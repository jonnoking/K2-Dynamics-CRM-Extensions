$crmapppool = "CRMAppPool"
$crmdeploymentserviceapppool = "CrmDeploymentServiceAppPool"

$appPool1 = get-wmiobject -namespace "root\MicrosoftIISv2" -class "IIsApplicationPool" | where-object {$_.Name -eq "W3SVC/AppPools/$crmapppool"}
$appPool2 = get-wmiobject -namespace "root\MicrosoftIISv2" -class "IIsApplicationPool" | where-object {$_.Name -eq "W3SVC/AppPools/$crmdeploymentserviceapppool"}

if($appPool1)
{
      $appPool1.Stop()
}
if($appPool2)
{
      $appPool2.Stop()
}
