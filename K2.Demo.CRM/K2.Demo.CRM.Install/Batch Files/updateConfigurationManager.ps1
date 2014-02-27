## Update C:\ProgramData\SourceCode\configurationmanager.config with CRM Client Event nodes

$doc = New-Object System.Xml.XmlDocument
$doc.Load("C:\ProgramData\SourceCode\configurationmanager.config")

$updated = $false;

# check if node already exists
$propertyWizardCheck = $doc.SelectSingleNode("//propertyWizard[@name='WizardCRMClient Property Wizard']")

if ($propertyWizardCheck -eq $null)
{
	$propWizElm = $doc.CreateDocumentFragment()
	$propWizElm.InnerXml = '<propertyWizard text="CRM Client Event" description="CRM Client Event" makeAvailableOffline="false" minVersionRequired="1.0.0.0" type="WizardCRMClient.CRMClientPropertyWizard" assembly="WizardCRMClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dc7da7b8c944e2f2" name="WizardCRMClient Property Wizard" />'
	$propertyWizards = $doc.SelectSingleNode("configuration/propertyWizards")
	$propertyWizards.AppendChild($propWizElm);

    $updated = $true
}

# check if node already exists
$toolboxItemsCheck = $doc.SelectSingleNode("/configuration/toolboxCategories/toolboxCategory[@default='Default Server Event Wizard']/toolboxItems/add[@name='WizardCRMClient Event Wizard']")
if ($toolboxItemsCheck -eq $null)
{
	$toolboxElm = $doc.CreateDocumentFragment()
	$toolboxElm.InnerXml = '<add name="WizardCRMClient Event Wizard" />'
	$toolboxItems = $doc.SelectSingleNode("/configuration/toolboxCategories/toolboxCategory[@default='Default Server Event Wizard']/toolboxItems")
	$toolboxItems.AppendChild($toolboxElm);

    $updated = $true
}

# check if node already exists
$wizardDocCheck = $doc.SelectSingleNode("//wizard[@name='WizardCRMClient Event Wizard']")
if ($wizardDocCheck -eq $null)
{
	$crmWizardElm = $doc.CreateDocumentFragment()
	$crmWizardElm.InnerXml = '<wizard showInToolbox="true" text="CRM Client Event" description="CRM Client Event" makeAvailableOffline="false" minVersionRequired="1.0.0.0" type="WizardCRMClient.CRMClientDefaultWizard" assembly="WizardCRMClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=dc7da7b8c944e2f2" name="WizardCRMClient Event Wizard"><propertyWizards><add name="Event General Property Wizard" /><add name="WizardCRMClient Property Wizard" /><add name="Event Actions and Outcome Property Wizard" /><add name="Event Escalations" /><add name="Exception Rule" /></propertyWizards></wizard>'
	$wizardDoc = $doc.SelectSingleNode("/configuration/wizards")
	$wizardDoc.AppendChild($crmWizardElm);

    $updated = $true
}

if ($updated)
{
    $doc.Save("C:\ProgramData\SourceCode\configurationmanager.config")
}