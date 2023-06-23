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
param location2 string = 'WestUS'

@description('Optional. Application Name')
param applicationName string = 'leaderboard'

@description('Optional. SSH Key for the AKS Node Pool. Configure all linux machines with the SSH RSA public key string. Your key should include three parts, for example \'ssh-rsa AAAAUcyupgH azureuser@linuxvm\' Needed if using Real-Time Ingestion')
@secure()
param aksSSHKey string = ''

@description('Optional. Admin username for AKS nodes. Needed if using Real-Time Ingestion')
@secure()
param aksAdminUsername string = ''

@description('Optional. AKS Node Pool VM Disk Size (in GB). This value ranges from 0 to 1023. Specifying 0 will apply the default disk size for that agentVMSize.')
@minValue(0)
@maxValue(1023)
param aksVmDiskSize int = 0

@description('Optional. AKS Node Pool Count')
@minValue(1)
@maxValue(50)
param aksNodeCount int = 4

@description('Optional. AKS Node Pool VM Size')
param aksVmSize string = 'Standard_D4s_v3'


var defaultTags = union({
  application: applicationName
}, tags)

var appResourceGroupName = 'rg-${applicationName}'
var sharedResourceGroupName = 'rg-shared-${applicationName}'

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

//Create SQL Resource
module sql 'sql.bicep' = {
  dependsOn:[
    kv
  ]
  scope: resourceGroup(appResourceGroup.name)
  name: 'sql-Deployment'
  params: {
    location: location
    tags: tags
    applicationName: applicationName
    keyVaultName: kv.name
  }
}

//Create App Service resources
module leaderboardApp 'app.bicep' = {
  dependsOn:[
    kv
    redis
    sql
    shared
  ]
  scope: resourceGroup(appResourceGroup.name)
  name: 'appService-Deployment'
  params: {
    location: location
    location2: location2
    isGeoReplicated: isGeoReplicated
    tags: tags
    applicationName: applicationName
    appiConnectionString: shared.outputs.appInsightsConnectionString
    redis1HostName: kv.getSecret('redis1HostName')
    redis1Password: kv.getSecret('redis1Password')
    redis2HostName: kv.getSecret('redis2HostName')
    redis2Password: kv.getSecret('redis2Password')
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
output app string = leaderboardApp.name
