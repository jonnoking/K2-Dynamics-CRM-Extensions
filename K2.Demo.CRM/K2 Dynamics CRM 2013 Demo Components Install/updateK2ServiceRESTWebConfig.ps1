$doc = New-Object System.Xml.XmlDocument
$doc.Load("C:\Program Files (x86)\K2 blackpearl\WebServices\K2Services\web.config")

#update REST.svc HTTPS security
$transHttpsNode = $doc.SelectSingleNode("//binding[@name='SourceCode.Services.RestBinding+HTTPS']/security/transport")
if ($transHttpsNode -ne $null) 
{
    #update clientCredentialType to Ntlm
    $transHttpsNode.clientCredentialType = "Ntlm"
}

#update REST.svc HTTP security
$transHttpNode = $doc.SelectSingleNode("//binding[@name='SourceCode.Services.RestBinding+HTTP']/security/transport")
if ($transHttpNode -ne $null) 
{
    #update clientCredentialType to Ntlm
    $transHttpNode.clientCredentialType = "Ntlm"
}

$doc.Save("C:\Program Files (x86)\K2 blackpearl\WebServices\K2Services\web.config")