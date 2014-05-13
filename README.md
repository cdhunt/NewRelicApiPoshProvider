NewRelicApiPoshProvider
=======================

A PowerShell Provider for the New Relic Web API

```powershell
Import-Module NewRelicAPIPoshProvider.dll
# Provide the API key as the UserName. Password doesn't matter.
New-PSDrive -Name nr -PSProvider NewRelicApi -Root '' -Credential (Get-Credential)
cd nr:/
```

```powershell
PS nr:\> gci applications | select Name, Health_Status, Last_Reported_At
```

```powershell
PS nr:\> gci servers | select Name, @{n="MemoryUsedGB";e={$_.Summary.Memory_Used/1GB}}, @{n="FullestDiskGB";e={$_.Summary.Fullest_Disk_Free/1gb}}, Last_Reported_At
```

```powershell
PS nr:\applications\ApplicationName\agent_metrics_count> gci


SSItemMode    :    <
PSPath        : NewRelicAPIPoshProvider\NewRelicApi::applications\Private
                API\agent_metrics_count\Data
PSParentPath  : NewRelicAPIPoshProvider\NewRelicApi::applications\ApplicationName\agent_metrics_count
PSChildName   : Data
PSDrive       : nr
PSProvider    : NewRelicAPIPoshProvider\NewRelicApi
PSIsContainer : False
From          : 1/1/0001 12:00:00 AM
To            : 1/1/0001 12:00:00 AM
```

This project uses:

 * [P2F](https://github.com/beefarino/p2f "PowerShell Provider Framework") by Jim Christopher
 * [RestSharp](https://github.com/restsharp/RestSharp "RestSharp")
 * https://rpm.newrelic.com/api/explore/