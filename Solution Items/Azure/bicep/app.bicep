@description('Required. Azure location to which the resources are to be deployed')
param location string

@description('Required. Azure secondary location to which the resources are to be deployed')
param location2 string

@description('Optional. The tags to be assigned to the created resources.')
param tags object = {}

@description('Required. Application name')
param applicationName string

@description('Optional. App Insights Instrumentation Key')
param instrumentationKey string = ''

@description('Optional. Setup geo-replication?')
param isGeoReplicated bool = false

@description('Required. Redis 1 Host Name')
@secure()
param redis1HostName string

@description('Required. Redis 1 Password')
@secure()
param redis1Password string

@description('Optional. Redis 2 Host Name')
@secure ()
param redis2HostName string

@description('Optional. Redis 2 Password')
@secure()
param redis2Password string

@description('Required. Caching Pattern')
param cachingPattern string

@description('Required. Azure SQL Connection String')
@secure()
param azureSQLConnectionString string



// Variables
var resourceNames = {
  appServicePlanName: 'asp-${applicationName}-${location}-001'
  appServicePlan2Name: 'asp-${applicationName}-${location2}-001'
  appName : 'aps-${applicationName}-${location}'
  app2Name: 'aps-${applicationName}-${location2}'
}

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: resourceNames.appServicePlanName
  location: location
  tags: tags
  sku: {
    name: 'B1'
    tier: 'Basic'
    size: 'B1'
    family: 'B'
    capacity: 1
  }
  kind: 'linux'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: true
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}

resource appServicePlan2 'Microsoft.Web/serverfarms@2022-03-01' = if(isGeoReplicated) {
  name: resourceNames.appServicePlan2Name
  location: location2
  tags: tags
  sku: {
    name: 'B1'
    tier: 'Basic'
    size: 'B1'
    family: 'B'
    capacity: 1
  }
  kind: 'linux'
  properties: {
    perSiteScaling: false
    elasticScaleEnabled: false
    maximumElasticWorkerCount: 1
    isSpot: false
    reserved: true
    isXenon: false
    hyperV: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}

resource app 'Microsoft.Web/sites@2022-03-01' = {
  name: resourceNames.appName
  location: location
  tags: tags
  kind: 'app,linux'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${resourceNames.appName}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${resourceNames.appName}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: appServicePlan.id
    reserved: true
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: false
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: 1
      linuxFxVersion: 'DOTNETCORE|6.0'
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: false
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '863D812A4F8321ABD7EE56AC999CCEA38C9856F34D6BB6D836065FB757627DF1'
    containerSize: 0
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    storageAccountRequired: false
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}


resource app1AppSettings 'Microsoft.Web/sites/config@2022-03-01' = if(!empty(instrumentationKey)) {
  name: 'web'
  parent: app
  properties: {
    appSettings: [
      {
        name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
        value: instrumentationKey
      }
      {
        name: 'LeaderboardSettings__RedisHost'
        value: redis1HostName
      }
      {
        name: 'LeaderboardSettings__RedisPassword'
        value: redis1Password
      }
      {
        name: 'LeaderboardSettings__IsACRE'
        value: true
      }
      {
        name: 'LeaderboardSettings__AllowAdmin'
        value: true
      }
      {
        name: 'LeaderboardSettings__LoadInitialData'
        value: true
      }
      {
        name: 'LeaderboardSettings__DeleteAllKeysOnLoad'
        value: true
      }
      {
        name: 'LeaderboardSettings__UseReadThrough'
        value: cachingPattern == 'Write-Behind & Read-Through' ? true : false
      }
      {
        name: 'LeaderboardSettings__UseWriteBehind'
        value: cachingPattern == 'Write-Behind & Read-Through' ? true : false
      }
      {
        name: 'LeaderboardSettings__UseCacheAside'
        value: cachingPattern == 'Cache-Aside' ? true : false
      }
      {
        name: 'LeaderboardSettings__UsePreFetch'
        value: cachingPattern == 'Real-Time Ingestion' ? true : false
      }
    ]
    connectionStrings: [
      {
        name: 'ConnectionString'
        connectionString: azureSQLConnectionString
      }
    ]
  }
}

resource app2 'Microsoft.Web/sites@2022-03-01' = if(isGeoReplicated) {
  name: resourceNames.app2Name
  location: location2
  tags: tags
  kind: 'app,linux'
  properties: {
    enabled: true
    hostNameSslStates: [
      {
        name: '${resourceNames.app2Name}.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Standard'
      }
      {
        name: '${resourceNames.app2Name}.scm.azurewebsites.net'
        sslState: 'Disabled'
        hostType: 'Repository'
      }
    ]
    serverFarmId: appServicePlan2.id
    reserved: true
    isXenon: false
    hyperV: false
    vnetRouteAllEnabled: false
    vnetImagePullEnabled: false
    vnetContentShareEnabled: false
    siteConfig: {
      numberOfWorkers: 1
      linuxFxVersion: 'DOTNETCORE|6.0'
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: false
      functionAppScaleLimit: 0
      minimumElasticInstanceCount: 0
    }
    scmSiteAlsoStopped: false
    clientAffinityEnabled: true
    clientCertEnabled: false
    clientCertMode: 'Required'
    hostNamesDisabled: false
    customDomainVerificationId: '863D812A4F8321ABD7EE56AC999CCEA38C9856F34D6BB6D836065FB757627DF1'
    containerSize: 0
    dailyMemoryTimeQuota: 0
    httpsOnly: true
    redundancyMode: 'None'
    storageAccountRequired: false
    keyVaultReferenceIdentity: 'SystemAssigned'
  }
}

resource app2AppSettings 'Microsoft.Web/sites/config@2022-03-01' = if(!empty(instrumentationKey) && isGeoReplicated) {
  name: 'web'
  parent: app2
  properties: {
    appSettings: [
      {
        name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
        value: instrumentationKey
      }
      {
        name: 'LeaderboardSettings__RedisHost'
        value: redis2HostName
      }
      {
        name: 'LeaderboardSettings__RedisPassword'
        value: redis2Password
      }
      {
        name: 'LeaderboardSettings__IsACRE'
        value: true
      }
      {
        name: 'LeaderboardSettings__AllowAdmin'
        value: true
      }
      {
        name: 'LeaderboardSettings__LoadInitialData'
        value: true
      }
      {
        name: 'LeaderboardSettings__DeleteAllKeysOnLoad'
        value: true
      }
      {
        name: 'LeaderboardSettings__UseReadThrough'
        value: cachingPattern == 'Write-Behind & Read-Through' ? true : false
      }
      {
        name: 'LeaderboardSettings__UseWriteBehind'
        value: cachingPattern == 'Write-Behind & Read-Through' ? true : false
      }
      {
        name: 'LeaderboardSettings__UseCacheAside'
        value: cachingPattern == 'Cache-Aside' ? true : false
      }
      {
        name: 'LeaderboardSettings__UsePreFetch'
        value: cachingPattern == 'Real-Time Ingestion' ? true : false
      }
    ]
    connectionStrings: [
      {
        name: 'ConnectionString'
        connectionString: azureSQLConnectionString
      }
    ]
  }
}

output appHostName string = app.properties.defaultHostName
output app2HostName string = app2.properties.defaultHostName 
