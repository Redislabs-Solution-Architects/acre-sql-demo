

@description('Required. Main location')
param location string

@description('Optional. The tags to be assigned to the created resources.')
param tags object = {}

@description('Optional. Administrator username')
param adminUserName string = 'sql-admin'

@description('Required. Application name')
param applicationName string

@description('Required. Key Vault Name')
param keyVaultName string

var resourceNames = {
  sqlServerName: 'sql-${applicationName}-${location}'
  sqlServerDbName: 'sqldb-${applicationName}-${location}'
}


resource sqlServer 'Microsoft.Sql/servers@2022-02-01-preview' = {
  name: resourceNames.sqlServerName
  location: location
  tags: tags
  properties: {
    administratorLogin: adminUserName
    version: '12.0'
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    restrictOutboundNetworkAccess: 'Disabled'
  }
}

resource sqlServerDb 'Microsoft.Sql/servers/databases@2022-02-01-preview' = {
  parent: sqlServer
  name: resourceNames.sqlServerDbName
  location: location
  tags: tags
  sku: {
    name: 'Standard'
    tier: 'Standard'
    capacity: 10
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 268435456000
    catalogCollation: 'SQL_Latin1_General_CP1_CI_AS'
    zoneRedundant: false
    readScale: 'Disabled'
    requestedBackupStorageRedundancy: 'Local'
    isLedgerOn: false
  }
}

resource sqlConnectionString 'Microsoft.KeyVault/vaults/secrets@2019-09-01' = {
  name: '${keyVaultName}/azureSqlConnectionString'
  properties: {
   value: 'Server=tcp:${reference(sqlServer.name).fullyQualifiedDomainName},1433;Initial Catalog=${sqlServerDb.name};Persist Security Info=False;User ID=${reference(sqlServer.name).administratorLogin};Password=${reference(sqlServer.name).administratorLoginPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
  }
 }

output sqlServerName string = sqlServer.name
output sqlServerDbName string = sqlServerDb.name
