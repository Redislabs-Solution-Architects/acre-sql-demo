targetScope = 'subscription'

@description('Optional. Which caching pattern you plan to use? -defaults to Write-Behind & Read Through')
@allowed([
  'Write-Behind & Read-Through'
  'Cache-Aside'
  'Real-Time Ingestion'
])
param cachingPattern string = 'Write-Behind & Read-Through'

@description('Optional. Azure main location to which the resources are to be deployed -defaults to the location of the current deployment')
param location string = deployment().location

@description('Optional. The tags to be assigned to the created resources.')
param tags object = {}

@description('Optional. Setup geo replication for app and ACRE? -defaults to false')
param isGeoReplicated bool = false

@description('Optional. Azure second location to which the resources are to be deployed -defaults to west')
param location2 string = (isGeoReplicated)? 'WestUS': ''

@description('Optional. Application Name')
param applicationName string = 'leaderboard'

var defaultTags = union({
  application: applicationName
}, tags)

var appResourceGroupName = 'rg-${applicationName}'
var sharedResourceGroupName = 'rg-shared-${applicationName}'
var aksAdminUsername = 'redisDemo'
var aksSSHKey = ''
var aksVmDiskSize = 0
var aksNodeCount = 4
var aksVmSize = 'Standard_D4s_v3'


// Create resource groups
resource appResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: appResourceGroupName
  location: location
  tags: defaultTags
}

resource sharedResourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: sharedResourceGroupName
  location: location
  tags: defaultTags
}

// Create shared resources
module shared './shared/shared.bicep' = {
  name: 'sharedresources-Deployment'
  scope: resourceGroup(sharedResourceGroup.name)
  params: {
    location: location
    applicationName: applicationName
    tags: defaultTags
  }
}

//Create SQL Resource
module sql 'sql.bicep' = {
  scope: resourceGroup(appResourceGroup.name)
  name: 'sql-Deployment'
  params: {
    location: location
    tags: tags
    applicationName: applicationName
    keyVaultName: kv.name
  }
}

resource kv 'Microsoft.KeyVault/vaults@2019-09-01' existing = {
  name: shared.outputs.keyVaultName
  scope: resourceGroup(subscription().id, sharedResourceGroupName )
}

//Create Redis resource
module redis 'redis.bicep' = {
  dependsOn: [
    kv
  ]
  scope: resourceGroup(appResourceGroup.name)
  name: 'redis-Deployment'
  params: {
    location: location
    location2: location2
    tags: defaultTags
    keyVaultName: shared.outputs.keyVaultName
    applicationName: applicationName
    isGeoReplicated: isGeoReplicated
  }
}

//Create App Service resources
module leaderboardApp 'app.bicep' = {
  scope: resourceGroup(appResourceGroup.name)
  name: 'appService-Deployment'
  params: {
    location: location
    location2: location2
    isGeoReplicated: isGeoReplicated
    tags: tags
    applicationName: applicationName
    instrumentationKey: shared.outputs.appInsightsInstrumentationKey
    redis1HostName: kv.getSecret('redis1HostName')
    redis1Password: kv.getSecret('redis1Password')
    redis2HostName: (isGeoReplicated) ? kv.getSecret('redis2HostName') : ''
    redis2Password: (isGeoReplicated) ? kv.getSecret('redis2Password') : ''
    cachingPattern: cachingPattern
    azureSQLConnectionString: kv.getSecret('azureSqlConnectionString')
  }
}

//Create Function Apps
module functionApps 'func.bicep' = if(cachingPattern == 'Write-Behind & Read-Through'){
  dependsOn: [
   sql
   redis 
  ]
  scope: resourceGroup(appResourceGroup.name)
  name: 'functionApps-Deployment'
  params: {
    location: location
    tags: tags
    applicationName: applicationName
  }
}

//Create Front Door
module frontDoor 'frontDoor.bicep' = if(cachingPattern == 'Write-Behind & Read-Through' && isGeoReplicated){
  dependsOn: [
    leaderboardApp
  ]
  scope: resourceGroup(appResourceGroup.name)
  name: 'frontDoor-Deployment'
  params: {
    application1Location: location
    application2Location: location2
    appHostName: leaderboardApp.outputs.appHostName
    app2HostName: leaderboardApp.outputs.app2HostName
    applicationName: applicationName
    tags: tags
  }
}

// Create AKS
module aks 'aks.bicep' = if(cachingPattern == 'Real-Time Ingestion'){
  dependsOn: [
    leaderboardApp
  ]
  scope: resourceGroup(appResourceGroup.name)
  name: 'aks-Deployment'
  params: {
     location: location
     applicationName: applicationName
     tags: tags
     linuxAdminUsername: aksAdminUsername
     sshRSAPublicKey: aksSSHKey
     osDiskSizeGB: aksVmDiskSize
     agentCount: aksNodeCount
     agentVMSize: aksVmSize
  }
}

output appResourceGroupName string = appResourceGroup.name
output sharedResourceGroupName string = sharedResourceGroup.name
