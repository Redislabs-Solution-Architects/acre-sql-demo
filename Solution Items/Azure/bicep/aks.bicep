@description('Required. Main location')
param location string

@description('Optional. The tags to be assigned to the created resources.')
param tags object = {}

@description('Required. Application name')
param applicationName string

@description('Optional. Disk size (in GB) to provision for each of the agent pool nodes. This value ranges from 0 to 1023. Specifying 0 will apply the default disk size for that agentVMSize.')
@minValue(0)
@maxValue(1023)
param osDiskSizeGB int = 0

@description('Optional. The number of nodes for the cluster. defaults to 3')
@minValue(1)
@maxValue(50)
param agentCount int = 3

@description('Optional. The size of the Virtual Machine.')
param agentVMSize string = 'standard_d2s_v3'

@description('Required. User name for the Linux Virtual Machines.')
param linuxAdminUsername string

@description('Required. Configure all linux machines with the SSH RSA public key string. Your key should include three parts, for example \'ssh-rsa AAAAB...snip...UcyupgH azureuser@linuxvm\'')
param sshRSAPublicKey string


var resourceNames = {
  aksClusterName: 'aks-${applicationName}-${location}'
  aksDNSPrefix: 'aks-${toLower(applicationName)}-dns'
}

resource aks 'Microsoft.ContainerService/managedClusters@2022-05-02-preview' = {
  name: resourceNames.aksClusterName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    dnsPrefix: resourceNames.aksDNSPrefix
    agentPoolProfiles: [
      {
        name: 'agentpool'
        osDiskSizeGB: osDiskSizeGB
        count: agentCount
        vmSize: agentVMSize
        osType: 'Linux'
        mode: 'System'
      }
    ]
    linuxProfile: {
      adminUsername: linuxAdminUsername
      ssh: {
        publicKeys: [
          {
            keyData: sshRSAPublicKey
          }
        ]
      }
    }
  }
  tags: tags
}

output controlPlaneFQDN string = aks.properties.fqdn
