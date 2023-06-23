using './main.bicep'

param cachingPattern = 'Write-Behind & Read-Through'
param location = 'EastUS'
param tags = {}
param isGeoReplicated = false
param location2 = 'WestUS'
param applicationName = 'leaderboard'
param aksSSHKey = ''
param aksAdminUsername = ''
param aksVmDiskSize = 0
param aksNodeCount = 4
param aksVmSize = 'Standard_D4s_v3'

