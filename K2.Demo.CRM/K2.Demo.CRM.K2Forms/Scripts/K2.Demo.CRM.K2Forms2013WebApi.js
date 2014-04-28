// default configuration values - will be read from K2Settings entity or configured via configureK2URLs()
var K2VIEWFLOWURL = 'http://dlx:81/workspace/Tasklistcontrol/ViewFlowMain.aspx?ProcessId=';
var K2WFSERVICESBASEURI = 'http://dlx.denallix.com:81/K2Services/';
var K2CUSTOMRESTSERVICESBASEURI = 'http://dlx.denallix.com/K2.Demo.CustomREST/';
var K2SMARTOBJECTSERVICESBASEURI = 'http://dlx.denallix.com:8888/smo/';

var K2_CONFIG_DISPLAYTYPE = "800000002";

// Tab display settings
var K2_CONFIG_TABPOSITION = 99;
var K2_CONFIG_ACTIONS = false;
var K2_CONFIG_VIEWFLOW = false;
var K2_CONFIG_DATAFIELDS = false;
var K2_CONFIG_PROCESSINFORMATION = false;
var K2_CONFIG_ACTIVITYINFORMATION = false;
var K2_CONFIG_ENTITYDATAFIELDNAME = "";
//

// Section display settings
var K2_CONFIG_SECTION_TAB = -1;
var K2_CONFIG_SECTION_LOCATION = -1;
var K2_CONFIG_SECTION_ACTIONS = false;
var K2_CONFIG_SECTION_VIEWFLOW = false;
var K2_CONFIG_SECTION_PROCESSINFORMATION = false;
var K2_CONFIG_SECTION_ACTIVITYINFORMATION = false;
//

var K2_CONFIG_QUERYK2SETTINGS = true;
var K2_CONFIG_QUERYK2SETTINGS_COMPLETE = false;
var K2_CONFIG_CHECKFORTASKS = true;
var K2_CONFIG_CHECKFORTASKS_COMPLETE = false;
var K2_CONFIG_USEFORMSETTINGS = true;
var K2_CONFIG_USEFORMSETTINGS_COMPLETE = false;

// not currently used
var K2_CONFIG_FORMNAME = "Information";
var K2_CONFIG_OVERRIDEFORM = false;
//

var K2SN = "";
var K2SELECTEDACTION = "";
var K2ACTIONSRIBBON = false;
var K2ACTIONS;

var SIMPLEOUTPUT = '';
var FULLOUTPUT = '';
var K2TIMER;


// ## END Get K2 Config ## //
function getK2Settings(callAsync) {
    var restRequestURL = Xrm.Page.context.getServerUrl() + "/XRMServices/2011/OrganizationData.svc/k2_k2settingsSet?$select=k2_name,k2_value";

    $.ajax({
        url: restRequestURL,
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        async: callAsync,
        beforeSend: function (XMLHttpRequest) {
            //Specifying this header ensures that the results will be returned as JSON.
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorGetK2Settings,
        success: handleGetK2Settings
    });
}

function handleGetK2Settings(data) {
    for (var i = 0; i < data.d.results.length; i++) {

        switch (data.d.results[i].k2_name) {
            case "K2RESTServicesBaseURL":
                K2WFSERVICESBASEURI = checkTrailingSlash(data.d.results[i].k2_value);
                break;
            case "ViewFlowURL":
                K2VIEWFLOWURL = data.d.results[i].k2_value;
                break;
            case "K2SmartObjectServicesBaseURL":
                K2SMARTOBJECTSERVICESBASEURI = checkTrailingSlash(data.d.results[i].k2_value);
                break;
            case "K2CustomRESTServicesBaseURL":
                K2CUSTOMRESTSERVICESBASEURI = checkTrailingSlash(data.d.results[i].k2_value);
                break;
            case "K2CustomRESTServicesBaseURL":
                K2CUSTOMRESTSERVICESBASEURI = checkTrailingSlash(data.d.results[i].k2_value);
                break;
        }
    }
    K2_CONFIG_QUERYK2SETTINGS_COMPLETE = true;
}

function errorGetK2Settings(data) {
    alert("Error retrieving K2 Settings: " + data.status + " - " + data.statusText);
}

function configureK2URLs(k2ViewFlowURL, k2WFServicesBaseURI, k2CustomRESTServicesBaseURI, k2SmartObjectBaseURI) {
    K2VIEWFLOWURL = ($.trim(k2ViewFlowURL) != '') ? k2ViewFlowURL : K2VIEWFLOWURL;
    K2WFSERVICESBASEURI = checkTrailingSlash(k2WFServicesBaseURI);
    K2CUSTOMRESTSERVICESBASEURI = checkTrailingSlash(k2CustomRESTServicesBaseURI);
    K2SMARTOBJECTSERVICESBASEURI = checkTrailingSlash(k2SmartObjectBaseURI);
}
// ## END Get K2 Config ## //


// ## Start Get Form Config ## //
// GET CONFIG FORM K2 Form Association Entity
function getFormConfigurationFromEntity(callAsync) {

    var restRequestURL = Xrm.Page.context.getServerUrl() + "/xrmservices/2011/OrganizationData.svc/k2_k2formassociationsSet?$select=k2_displaytype,k2_actionssection,k2_checktasks,k2_datafieldssection,k2_entityiddatefieldname,k2_entitylogicalname,k2_formname,k2_k2formassociationsId,k2_name,k2_processinformationsection,k2_tablocation,k2_viewflowsection,k2_activityinformationsection,k2_sectiontab,k2_sectionlocation,k2_sectionactions,k2_sectionviewflow,k2_sectionprocessinformation,k2_sectionactivityinformation&$filter=k2_entitylogicalname eq '" + Xrm.Page.data.entity.getEntityName() + "'";
    $.ajax({
        url: restRequestURL,
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        async: callAsync,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorGetFormConfigurationFromEntity,
        success: handleGetFormConfigurationFromEntity
    });
}

function handleGetFormConfigurationFromEntity(data) {

    var settings;

    if (data.d.results != null && data.d.results != undefined) {
        // take the first task if multiple are returned
        settings = data.d.results[0];
    } else {
        settings = data.d;
    }

    K2_CONFIG_DISPLAYTYPE = settings['k2_displaytype'].Value;

    // Tab display settings
    K2_CONFIG_TABPOSITION = settings['k2_tablocation'];
    K2_CONFIG_ACTIONS = settings['k2_actionssection'];
    K2_CONFIG_VIEWFLOW = settings['k2_viewflowsection'];
    K2_CONFIG_DATAFIELDS = settings['k2_datafieldssection'];
    K2_CONFIG_PROCESSINFORMATION = settings['k2_processinformationsection'];
    K2_CONFIG_ACTIVITYINFORMATION = settings['k2_activityinformationsection'];
    //K2_CONFIG_ENTITYDATAFIELDNAME = ""; // not implemented in 2013
    //

    // Section display settings
    K2_CONFIG_SECTION_TAB = settings['k2_sectiontab'];
    K2_CONFIG_SECTION_LOCATION = settings['k2_sectionlocation'];
    K2_CONFIG_SECTION_ACTIONS = settings['k2_sectionactions'];
    K2_CONFIG_SECTION_VIEWFLOW = settings['k2_sectionviewflow'];
    K2_CONFIG_SECTION_PROCESSINFORMATION = settings['k2_sectionprocessinformation'];
    K2_CONFIG_SECTION_ACTIVITYINFORMATION = settings['k2_sectionactivityinformation'];
    //

    K2_CONFIG_CHECKFORTASKS = settings['k2_checktasks'];

    K2_CONFIG_ENTITYDATAFIELDNAME = settings['k2_entityiddatefieldname'];

    K2_CONFIG_USEFORMSETTINGS_COMPLETE = true;
}

function errorGetFormConfigurationFromEntity(data) {
    alert("Error retrieving form settings from entity: " + data.status + " - " + data.statusText);
}
// ## END Get Form Config ## //

// ## START Check Tasks ## //
// Check tasks associated with current entity for the current user for a K2 customized task with a Serial Number
function checkForTasks(callAsync) {
    var restRequestURL = Xrm.Page.context.getServerUrl() + "/xrmservices/2011/organizationdata.svc/TaskSet?$filter=RegardingObjectId/Id eq (guid'" + Xrm.Page.data.entity.getId() + "') and OwnerId/Id eq (guid'" + Xrm.Page.context.getUserId() + "') and StateCode/Value eq 0&$select=*&$orderby=CreatedOn asc";

    $.ajax({
        url: restRequestURL,
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        async: callAsync,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorCheckForTasks,
        success: handleCheckForTasks
    });
}

function handleCheckForTasks(data) {
    K2SN = '';

    var task;
    var isMultipleTasks = false;
    if (data.d.results != null && data.d.results != undefined) {
        // take the first task if multiple are returned
        task = data.d.results[0];
        isMultipleTasks = true;
    } else {
        task = data.d;
    }

    if (isMultipleTasks) {
        for (var i = 0; i < data.d.results.length; i++) {
            if (getK2SerialNumberFromTask(data.d.results[i]) != '') {
                break;
            }
        }
    } else {
        // just one task returned
        getK2SerialNumberFromTask(task);
    }

    K2_CONFIG_CHECKFORTASKS_COMPLETE = true;
}

function getK2SerialNumberFromTask(task) {
    if (task != null && task && task.k2_k2serialnumber != null && task.k2_k2serialnumber) {
        if (task.k2_k2serialnumber != "") {
            K2SN = task.k2_k2serialnumber;
        }
    }
    return K2SN;
}

function errorCheckForTasks(data) {
    alert("Error retrieving associated Tasks: " + data.status + " - " + data.statusText);
}
// ## END Get Tasks ## //


function k2OnFormLoad() {
    if (Xrm.Page.ui.getFormType() == 2 || Xrm.Page.ui.getFormType() == 3) {

        if (K2_CONFIG_QUERYK2SETTINGS && !K2_CONFIG_QUERYK2SETTINGS_COMPLETE) {
            getK2Settings(false);
        }

        if (K2_CONFIG_USEFORMSETTINGS && !K2_CONFIG_USEFORMSETTINGS_COMPLETE) {
            getFormConfigurationFromEntity(false);
        }

        var qs = new Querystring(getQueryString());
        var extra = new Querystring(getExtraQS());
        var sn = "";
        sn = extra.get("k2_sn", "");
        if (sn != "") {
            K2SN = sn;
        }

        if (K2SN == "") {
            if (K2_CONFIG_CHECKFORTASKS && !K2_CONFIG_CHECKFORTASKS_COMPLETE) {
                checkForTasks(false);
            }
        }

        var entityId = "";

        // get entity id
        entityId = removeGUIDBrackets(Xrm.Page.data.entity.getId());

        if (K2SN != "") {
            getK2WorklistBySN(K2SN);
        } else if (entityId != "" && !K2_CONFIG_CHECKFORTASKS) {
            getK2WorklistByEntityId(K2_CONFIG_ENTITYDATAFIELDNAME, entityId);
        }
    }
}

function actionK2WorklistItem(SN, k2Action) {

    var action = {
        'SerialNumber': SN,
        'SelectedCustomAction': k2Action
    };

    return $.ajax({
        url: K2WFSERVICESBASEURI + 'api/worklistitem/',
        type: 'PUT',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        data: JSON.stringify(action),
        async: true,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorWorklist
    });

}

function getK2WorklistBySN(SN) {
    $.ajax({        
        url: K2WFSERVICESBASEURI + 'api/worklistitem/' + SN,
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        async: true,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorWorklist,
        success: handlegetK2WorklistBySN
    });
}

function handlegetK2WorklistBySN(data) {

    if (data.Code == "-1") {
        alert("The assign K2 task is currently being worked on by another user");
        return;
    }

    // assumes a single worklist item is being returned
    if (data.CustomActions.length > 0) {
        //updateK2CRMForm(data);
        updateK2CRM2013Form(data);
    }
}

function getK2WorklistByEntityId(dataFieldName, dataFieldValue) {

    //    var k2serviceurl = 'http://dlx/K2.Demo.CustomREST/K2DemoREST.svc/Custom/WorklistDataFieldSearch?sDataFieldName=' + dataFieldName + '&sDataFieldValue=' + dataFieldValue + '&actDataField=false&actXmlField=false&piDataField=true&piXmlField=true&$format=json';
    var k2serviceurl = K2CUSTOMRESTSERVICESBASEURI + 'K2DemoREST.svc/Custom/WorklistDataFieldSearch?sDataFieldName=' + dataFieldName + '&sDataFieldValue=' + dataFieldValue + '&actDataField=false&actXmlField=false&piDataField=true&piXmlField=true&$format=json';

    $.ajax({
        url: k2serviceurl,
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        async: true,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorWorklist,
        success: handlegetK2WorklistByEntityId
    });
}

function handlegetK2WorklistByEntityId(data) {
    // assumes a collection of worklist items is being returned
    // takes first worklist item returned - not handling multiple items at present
    if (data[0] != null && data[0].Actions.length) {
        //updateK2CRMForm(data[0])
        updateK2CRM2013Form(data[0]);
    }
}


function errorWorklist(data) {
    alert(data.status + " - " + data.statusText);
}


// handles K2 Task Action ribbon extension if it exists - used in CRM 2011/2013
function populateK2ActionsArray(data) {
    var len = data.CustomActions.length;
    if (len > 0) {
        K2ACTIONS = new Array();
        for (var i = 0; i < len; i++) {
            K2ACTIONS[i] = data.CustomActions[i].Name;
        }
        K2ACTIONSRIBBON = true;
        Xrm.Page.ui.refreshRibbon();
    }
}


// Dynamics CRM 2013
function showSimpleK2ProcessInformation(data) {

    var k2sectionTableStart = '<div class="ms-crm-FormSection-Container" aria-k2="customsection" style="display:none;"><table class="ms-crm-FormSection" id="Table1" style="table-layout: fixed;" cellspacing="0" cellpadding="0" name="Details" valign="top"><colgroup><col width="102"><col><tbody><tr><td class="ms-crm-Form-Section&#9;ms-crm-Form-Section-Print" colspan="2"><h3 class="ms-crm-Form">K2 WORKLIST ITEM</h3></td></tr><tr height="9"><td></td></tr>';

    var k2sectionTableFinish = '<tr class="ms-crm-Form-SectionGapRow"><td></td><td></td></tr></tbody></table></div>';

    var k2valueRowTemplate = '<tr height="24"><td title="[[k2valuelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[k2valueid]]"><span class="ms-crm-InlineEditLabel"><span style="max-width: 102px;">[[k2valuelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="Td4" data-height="24"><div tabindex="1020" title="[[k2value]]" class="ms-crm-Inline-Chrome nvarchar" id="Div2" aria-describedby="[[k2valuelabel]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="4a63c8d1-6c1e-48ec-9db4-3e6c7155334c" data-attributename="[[k2valuelabel]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[k2value]]<div class="ms-crm-Inline-GradientMask"></div></span></div><span class="ms-crm-Inline-LockIcon"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span><div class="ms-crm-Inline-Edit" style="display: none;"><input title="" class="ms-crm-InlineInput" id="Text2" aria-labelledby="[[k2valuelabel]]_c [[k2valuelabel]]_w" type="text" maxlength="100" attrName="[[k2valuelabel]]" attrPriv="3" controlmode="locked" defaultValue="[[k2value]]"></div></div></td></tr>';

    var k2valueRowTemplateNoIcon = '<tr height="24"><td title="[[k2valuelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[k2valueid]]"><span class="ms-crm-InlineEditLabel"><span style="max-width: 102px;">[[k2valuelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="Td5" data-height="24"><div tabindex="1020" title="[[k2value]]" class="ms-crm-Inline-Chrome nvarchar" id="Div3" aria-describedby="[[k2valuelabel]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="4a63c8d1-6c1e-48ec-9db4-3e6c7155334c" data-attributename="[[k2valuelabel]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[k2value]]<div class="ms-crm-Inline-GradientMask"></div></span></div><div class="ms-crm-Inline-Edit" style="display: none;"><input title="" class="ms-crm-InlineInput" id="Text3" aria-labelledby="[[k2valuelabel]]_c [[k2valuelabel]]_w" type="text" maxlength="100" attrName="[[k2valuelabel]]" attrPriv="3" controlmode="locked" defaultValue="[[k2value]]"></div></div></td></tr>';

    var k2valueRowTemplateHyperlink = '<tr height="24"><td title="[[k2valuelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="Td3"><span class="ms-crm-InlineEditLabel"><span style="max-width: 115px;">[[k2valuelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="Td6" data-height="24"><div tabindex="1040" title="[[k2value]]" class="ms-crm-ltrcontrol ms-crm-Inline-Chrome nvarchar" id="Div4" aria-describedby="[[k2valuelabel]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="8448b78f-8f42-454e-8e2a-f8196b0419af" data-attributename="[[k2valuelabel]]" data-initialized="true"><div class="ms-crm-Inline-Value"><span><a class="ms-crm-gridurl" href="[[k2value]]", target="_blank">[[k2valuemask]]</a><div class="ms-crm-Inline-GradientMask"></div></span></div><span class="ms-crm-Inline-LockIcon" style="display: none;"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span><div class="ms-crm-Inline-Edit" style="display: none;"><input title="" class="ms-crm-Email-Address ms-crm-InlineInput" id="Text4" aria-labelledby="[[k2valuelabel]]_c [[k2valuelabel]]_w" maxlength="200" attrName="[[k2valuelabel]]" attrPriv="7" controlmode="normal" defaultValue="[[k2value]]"></div></div></td></tr>';


    var folioRow = "";
    var processInstanceRow = "";
    var processInstanceDateRow = "";
    var activityRow = "";
    var activityDateRow = "";
    var viewflowRow = "";
    var actionOutput = "";

    if (K2_CONFIG_SECTION_PROCESSINFORMATION) {
        folioRow = k2valueRowTemplateNoIcon.replace(/\[\[k2value\]\]/g, data.ProcessFolio).replace(/\[\[k2valuelabel\]\]/g, "Folio").replace(/\[\[k2valueid\]\]/g, "k2folio");
        processInstanceRow = k2valueRowTemplateNoIcon.replace(/\[\[k2value\]\]/g, data.ProcessFullName).replace(/\[\[k2valuelabel\]\]/g, "Process Name").replace(/\[\[k2valueid\]\]/g, "k2processname");
        processInstanceDateRow = k2valueRowTemplateNoIcon.replace(/\[\[k2value\]\]/g, data.StartDate).replace(/\[\[k2valuelabel\]\]/g, "Process Start Date").replace(/\[\[k2valueid\]\]/g, "k2processstartdate");
    }

    if (K2_CONFIG_SECTION_ACTIVITYINFORMATION) {
        activityRow = k2valueRowTemplateNoIcon.replace(/\[\[k2value\]\]/g, data.ActivityName).replace(/\[\[k2valuelabel\]\]/g, "Activity Name").replace(/\[\[k2valueid\]\]/g, "k2activity");
        activityDateRow = k2valueRowTemplateNoIcon.replace(/\[\[k2value\]\]/g, data.StartDate).replace(/\[\[k2valuelabel\]\]/g, "Activity Start Date").replace(/\[\[k2valueid\]\]/g, "k2activitystartdate");
    }

    if (K2_CONFIG_SECTION_VIEWFLOW) {
        viewflowRow = k2valueRowTemplateHyperlink.replace(/\[\[k2value\]\]/g, data.ViewFlowUrl).replace(/\[\[k2valuemask\]\]/g, "K2 View Flow").replace(/\[\[k2valuelabel\]\]/g, "View Flow").replace(/\[\[k2valueid\]\]/g, "k2viewflow");
    }

    if (K2_CONFIG_SECTION_ACTIONS) {
        actionOutput = createActionsRow(data, "k2actionssimple", "K2 Actions");
    }

    var outputHtml = k2sectionTableStart + folioRow + processInstanceRow + processInstanceDateRow + activityRow + activityDateRow + viewflowRow + actionOutput + k2sectionTableFinish;

    return outputHtml;

}

// generate actions dropdown and button - used by both section and tab display
function createActionsRow(data, id, label) {
    // create actions row
    var actionsStart = '<tr height="24"><td title="[[k2actionslabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[k2actionsid]]_c"><span class="ms-crm-InlineEditLabel"><span style="max-width: 102px;">[[k2actionslabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="[[k2actionsid]]_d" data-height="24"><div tabindex="1750" title="Select an action" class="ms-crm-Inline-Chrome picklist k2actions" id="[[k2actionsid]]" aria-describedby="[[k2actionsid]]_c" data-picklisttype="1" data-raw="" data-layout="0" data-fdeid="PrimaryEntity" data-formid="8448b78f-8f42-454e-8e2a-f8196b0419af" data-attributename="[[k2actionsid]]" data-initialized="true" haserror="false"><div class="ms-crm-Inline-Value ms-crm-Inline-EmptyValue" style="display: block;"><span>--<div class="ms-crm-Inline-GradientMask"></div></span></div><!-- start --> <span class="ms-crm-Inline-LockIcon" title="Click to action worklist item"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_save k2actionsbutton" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span> <!-- end --> <div class="ms-crm-Inline-Edit ms-crm-Inline-OptionSet noScroll ms-crm-Inline-HideByZeroHeight"><select tabindex="-1" title="[[k2actionslabel]]" class="ms-crm-SelectBox ms-crm-Inline-OptionSet-AutoOpen ms-crm-Inline-HideByZeroHeight-Ie7" id="[[k2actionsid]]_i" aria-labelledby="[[k2actionsid]]_c [[k2actionsid]]_w" size="5" defaultselected="" attrname="[[k2actionsid]]" attrpriv="7" controlmode="normal" defaultvalue="">';

    var actionshtml = '';
    var len = data.CustomActions.length;
    for (var i = 0; i < len; i++) {
        actionshtml += '<option value="' + data.CustomActions[i].Name + '">' + data.CustomActions[i].Name + '</option>';
    }

    var actionsEnd = '</select></div><span class="ms-crm-Inline-LockIcon" style="display: none;"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span><span title="" class="ms-crm-Inline-WarningIcon" style="display: none;"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_warning" id="[[k2actionsid]]_warn" alt="Error" src="/_imgs/imagestrips/transparent_spacer.gif"><div class="ms-crm-Hidden-NoBehavior" id="[[k2actionsid]]_w"></div></span></div></td></tr>';

    var actionsOutput = actionsStart + actionshtml + actionsEnd;


    return actionsOutput.replace(/\[\[k2actionsid\]\]/g, id).replace(/\[\[k2actionslabel\]\]/g, label);
}


function showFullK2ProcessInforation(data) {

    // template code
    var tabHeaderTemplate = '<div class="ms-crm-InlineTab-Read" id="K2DetailsTabDynamic" name="K2DetailsTabDynamic" aria-k2="customtab" style="display:none;"><div class="ms-crm-InlineTabHeader ms-crm-InlineTabBorder" id="{21712fd1-92e7-49a4-83db-58209bcba531}_Header"><div class="ms-crm-InlineTabHeaderExpander" style="display: inline-block;"><a tabindex="0" href="javascript:void(0);"><img title="Collapse this tab" class="ms-crm-InlineTabExpander ms-crm-ImageStrip-tab_down" id="{21712fd1-92e7-49a4-83db-58209bcba531}_Expander" alt="Collapse this tab" src="/_imgs/imagestrips/transparent_spacer.gif"></a></div><div class="ms-crm-InlineTabHeaderText" style="display: inline-block;"><a name="{21712fd1-92e7-49a4-83db-58209bcba531}" tabindex="1680" class="ms-crm-InlineTabHeaderText" href="javascript:void(0);"><h2 class="ms-crm-Form">K2</h2></a></div></div><div class="W ms-crm-InlineTabBody-Read" id="{21712fd1-92e7-49a4-83db-58209bcba531}_content"><div class="ms-crm-tabcolumn0" style="width: 100%; float: left;">';
    var tabViewFlowTemplate = '<div class="ms-crm-FormSection-Container"><table class="ms-crm-FormSection" id="{bb7a9a14-a1ad-429c-b282-8a06365c025c}" style="table-layout: fixed;" cellspacing="0" cellpadding="0" name="tab_2_section_1" valign="top"><colgroup><col width="115"><col><tbody><tr style="display: none;"><td class="ms-crm-Form-Section ;ms-crm-Form-Section-Print" colspan="2"><h3 class="ms-crm-Form">View Flow</h3></td></tr><tr height="24"><td class="ms-crm-Field-Data-Print" rowspan="16" colspan="2"><div class="ms-crm-Field-Data-Print" id="IFRAME_autoviewflow_d" style="height: 384px; padding-bottom: 0px;" rowspan="16" colspan="2" data-height="384"><iframe tabindex="1690" class="ms-crm-Custom-Read" id="IFRAME_autoviewflow" src="[[viewflowurl]]" frameborder="0" scrolling="auto" style="border: 0px currentColor;" onNOTload=\'if(!IsNull(Mscrm.InlineIFrameControlView)) Mscrm.InlineIFrameControlView.loadHandler("IFRAME_autoviewflow")\' url="[[viewflowurl]]"></iframe></div></td></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr height="24"></tr><tr class="ms-crm-Form-SectionGapRow"><td></td><td></td></tr></tbody></table></div>';
    var tabSectionHeaderTemplate = '<div class="ms-crm-FormSection-Container"><table class="ms-crm-FormSection" id="{e58ad111-8218-4a53-a758-7212886cd4ff}" style="table-layout: fixed;" cellspacing="0" cellpadding="0" name="{e58ad111-8218-4a53-a758-7212886cd4ff}" valign="top"><colgroup><col width="115"><col><col width="115"><col><tbody><tr style="display: none;"><td class="ms-crm-Form-Section&#9;ms-crm-Form-Section-Print" colspan="4"><h3 class="ms-crm-Form">Section</h3></td></tr>';
    var tabProcessInfoTemplate = '<tr height="24"><td title="[[processnamelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[processnameid]]_c"><span class="ms-crm-InlineEditLabel"><span style="max-width: 115px;">[[processnamelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="[[processnameid]]_d" data-height="24"><div tabindex="1700" title="[[processname]]" class="ms-crm-Inline-Chrome nvarchar" id="[[processnameid]" aria-describedby="[[processnameid]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="ccfa0c7b-552e-4370-95c1-faa0293ddfee" data-attributename="[[processnameid]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[processname]]<div class="ms-crm-Inline-GradientMask"></div></span></div><!--<span class="ms-crm-Inline-LockIcon"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span>--><div class="ms-crm-Inline-Edit" style="display: none;"><input title="[[processname]]" class="ms-crm-InlineInput" id="[[processnameid]]_i" aria-labelledby="[[processnameid]]_c [[processnameid]]_w" type="text" maxlength="100" attrname="[[processnameid]]" attrpriv="3" controlmode="locked" defaultvalue="[[processname]]"></div></div></td><td title="[[processstartdatelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[processstartdateid]]_c"><span class="ms-crm-InlineEditLabel"><span style="max-width: 115px;">[[processstartdatelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="[[processstartdateid]]_d" data-height="24"><div tabindex="1700" title="[[processstartdate]]" class="ms-crm-Inline-Chrome nvarchar" id="[[processstartdateid]]" aria-describedby="[[processstartdateid]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="ccfa0c7b-552e-4370-95c1-faa0293ddfee" data-attributename="[[processstartdateid]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[processstartdate]]<div class="ms-crm-Inline-GradientMask"></div></span></div><!--<span class="ms-crm-Inline-LockIcon"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span>--><div class="ms-crm-Inline-Edit" style="display: none;"><input title="[[processstartdate]]" class="ms-crm-InlineInput" id="[[processstartdateid]]_i" aria-labelledby="[[processstartdateid]]_c [[processstartdateid]]_w" type="text" maxlength="100" attrname="[[processstartdateid]]" attrpriv="3" controlmode="locked" defaultvalue="[[processstartdate]]"></div></div></td></tr>';
    var tabFolioTemplate = '<tr height="24"><td title="[[foliolabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[folioid]_c"><span class="ms-crm-InlineEditLabel"><span style="max-width: 115px;">[[foliolabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="[[folioid]]_d" data-height="24"><div tabindex="1700" title="[[folio]]" class="ms-crm-Inline-Chrome nvarchar" id="[[folioid]]" aria-describedby="[[folioid]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="ccfa0c7b-552e-4370-95c1-faa0293ddfee" data-attributename="[[folioid]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[folio]]<div class="ms-crm-Inline-GradientMask"></div></span></div><!-- <span class="ms-crm-Inline-LockIcon"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span> --><div class="ms-crm-Inline-Edit" style="display: none;"><input title="[[folio]]" class="ms-crm-InlineInput" id="[[folioid]]_i" aria-labelledby="[[folioid]]_c [[folioid]]_w" type="text" maxlength="100" attrname="[[folioid]]" attrpriv="3" controlmode="locked" defaultvalue="[[folio]]"></div></div></td><td class="ms-crm-Field-Data-Print" colspan="2"><div class="ms-crm-Field-Data-Print" colspan="2" data-height="24"></div></td></tr>';
    var tabActivityInfoTemplate = '<tr height="24"><td title="[[activitynamelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[activitynameid]]_c"><span class="ms-crm-InlineEditLabel"><span style="max-width: 115px;">[[activitynamelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="[[activitynameid]]_d" data-height="24"><div tabindex="1700" title="[[activityname]]" class="ms-crm-Inline-Chrome nvarchar" id="[[activitynameid]" aria-describedby="[[activitynameid]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="ccfa0c7b-552e-4370-95c1-faa0293ddfee" data-attributename="[[activitynameid]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[activityname]]<div class="ms-crm-Inline-GradientMask"></div></span></div><!-- <span class="ms-crm-Inline-LockIcon"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span> --><div class="ms-crm-Inline-Edit" style="display: none;"><input title="[[activityname]]" class="ms-crm-InlineInput" id="[[activitynameid]]_i" aria-labelledby="[[activitynameid]]_c [[activitynameid]]_w" type="text" maxlength="100" attrname="[[activitynameid]]" attrpriv="3" controlmode="locked" defaultvalue="[[activityname]]"></div></div></td><td title="[[activitystartdatelabel]]" class="ms-crm-ReadField-Normal ms-crm-FieldLabel-LeftAlign" id="[[activitystartdateid]]_c"><span class="ms-crm-InlineEditLabel"><span style="max-width: 115px;">[[activitystartdatelabel]]</span><div class="ms-crm-Inline-GradientMask" style="display: none;"></div></span></td><td class="ms-crm-Field-Data-Print" id="[[activitystartdateid]]_d" data-height="24"><div tabindex="1700" title="[[activitystartdate]]" class="ms-crm-Inline-Chrome nvarchar" id="[[activitystartdateid]]" aria-describedby="[[activitystartdateid]]_c" data-layout="0" data-fdeid="PrimaryEntity" data-formid="ccfa0c7b-552e-4370-95c1-faa0293ddfee" data-attributename="[[activitystartdateid]]" data-controlmode="locked" data-initialized="true"><div class="ms-crm-Inline-Value  ms-crm-Inline-Locked"><span>[[activitystartdate]]<div class="ms-crm-Inline-GradientMask"></div></span></div><!-- <span class="ms-crm-Inline-LockIcon"><img width="1" height="1" class="ms-crm-ImageStrip-inlineedit_locked" alt="" src="/_imgs/imagestrips/transparent_spacer.gif"></span> --><div class="ms-crm-Inline-Edit" style="display: none;"><input title="[[activitystartdate]]" class="ms-crm-InlineInput" id="[[activitystartdateid]]_i" aria-labelledby="[[activitystartdateid]]_c [[activitystartdateid]]_w" type="text" maxlength="100" attrname="[[activitystartdateid]]" attrpriv="3" controlmode="locked" defaultvalue="[[activitystartdate]]"></div></div></td></tr>';
    var tabSectionFooterTemplate = '<tr class="ms-crm-Form-SectionGapRow"><td></td><td></td><td></td><td></td></tr></tbody></table></div>';
    var tabFooterTemplate = '</div></div></div>';
    var tabSpacerTemplate = '<td class="ms-crm-Field-Data-Print" colspan="2"><div class="ms-crm-Field-Data-Print" colspan="2" data-height="24"></div></td>';


    var tabViewFlow = "";
    var tabDetails = "";
    var tabDetailsProcess = "";
    var tabDetailsActivity = "";
    var tabDetailsActions = "";

    if (K2_CONFIG_VIEWFLOW) {
        tabViewFlow = tabViewFlowTemplate.replace(/\[\[viewflowurl\]\]/g, data.ViewFlowUrl);
    }

    if (K2_CONFIG_ACTIONS || K2_CONFIG_ACTIVITYINFORMATION || K2_CONFIG_PROCESSINFORMATION) {

        if (K2_CONFIG_ACTIONS) {
            tabDetailsActions = createActionsRow(data, "k2actionscomplex", "Actions");
            tabDetailsActions = tabDetailsActions.substr(0, tabDetailsActions.length - 5) + tabSpacerTemplate + "</tr>";
        }

        if (K2_CONFIG_PROCESSINFORMATION) {
            tabDetailsProcess = tabProcessInfoTemplate.replace(/\[\[processname\]\]/g, data.ProcessName).replace(/\[\[processnamelabel\]\]/g, "Process Name").replace(/\[\[processnameid\]\]/g, "processinstancename").replace(/\[\[processstartdate\]\]/g, data.StartDate).replace(/\[\[processstartdatelabel\]\]/g, "Process Start Date").replace(/\[\[processstartdateid\]\]/g, "processinstancestartdate");
            tabDetailsProcess += tabFolioTemplate.replace(/\[\[folio\]\]/g, data.ProcessFolio).replace(/\[\[foliolabel\]\]/g, "Folio").replace(/\[\[folioid\]\]/g, "processinstancefolio");
        }

        if (K2_CONFIG_ACTIVITYINFORMATION) {
            tabDetailsActivity = tabActivityInfoTemplate.replace(/\[\[activityname\]\]/g, data.ActivityName).replace(/\[\[activitynamelabel\]\]/g, "Activity").replace(/\[\[activitynameid\]\]/g, "activityinstancename").replace(/\[\[activitystartdate\]\]/g, data.StartDate).replace(/\[\[activitystartdatelabel\]\]/g, "Activity Start Date").replace(/\[\[activitydatestartid\]\]/g, "activityinstancestartdate");
        }

        tabDetails = tabSectionHeaderTemplate + tabDetailsProcess + tabDetailsActivity + tabDetailsActions + tabSectionFooterTemplate;

    }

    var tabOutput = tabHeaderTemplate + tabViewFlow + tabDetails + tabFooterTemplate;

    return tabOutput;
}


function updateK2CRM2013Form(data) {
    if (data == null || data == undefined || data == "") {
        return;
    }

    // set K2SN - currently a global var
    // used for k2OnFormSave
    K2SN = data.SerialNumber;

    var isLastTab = false;

    populateK2ActionsArray(data);

    var outputHtml = "";
    var fulloutputHtml = "";

    switch (String(K2_CONFIG_DISPLAYTYPE)) {
        case "800000000": // Section

            outputHtml = showSimpleK2ProcessInformation(data);
            SIMPLEOUTPUT = outputHtml;
            injectK2SectionIntoForm(outputHtml);

            break;
        case "800000001": // Tab

            fulloutputHtml = showFullK2ProcessInforation(data);
            FULLOUTPUT = fulloutputHtml;
            injectK2TabIntoForm(fulloutputHtml);

            break;
        case "800000002": // Both

            outputHtml = showSimpleK2ProcessInformation(data);
            SIMPLEOUTPUT = outputHtml;
            injectK2SectionIntoForm(outputHtml);

            fulloutputHtml = showFullK2ProcessInforation(data);
            FULLOUTPUT = fulloutputHtml;
            injectK2TabIntoForm(fulloutputHtml);

            break;
    }

    // add events here so that elements have been created in the DOM 
    // not perfect but will do
    $("div.k2actions").find("div.ms-crm-Inline-EmptyValue").click(function () {
        var el = $(this).parents().get(1);
        $(this).attr("style", "display:none;");
        $(this).parent().find("div.ms-crm-Inline-HideByZeroHeight").removeClass("ms-crm-Inline-HideByZeroHeight");
        $(this).parent().find("div.ms-crm-Inline-HideByZeroHeight-Ie7").removeClass("ms-crm-Inline-HideByZeroHeight-Ie7");
    });

    $("div.k2actions").find("select").mouseout(function () {
        var el = $(this).parents().get(1);
        $(this).addClass("ms-crm-Inline-HideByZeroHeight-Ie7");
        $(el).find("div.ms-crm-Inline-EmptyValue").attr("style", "display:block;");
        $(el).find("div.ms-crm-Inline-OptionSet").addClass("ms-crm-Inline-HideByZeroHeight");
    });

    $("div.k2actions").find("select").change(function () {
        var el = $(this).parents().get(1);
        $(el).find("div.ms-crm-Inline-EmptyValue").attr("style", "display:block;");
        $(el).find("div.ms-crm-Inline-EmptyValue:first-child").html('<span>' + $(this).val() + '<div class="ms-crm-Inline-GradientMask"></div></span>');
        $(el).find("div.ms-crm-Inline-OptionSet").addClass("ms-crm-Inline-HideByZeroHeight");
        $(this).addClass("ms-crm-Inline-HideByZeroHeight-Ie7");
    });

    $("img.k2actionsbutton").mouseover(function () {
        if ($(this).attr("src") == "/_imgs/imagestrips/transparent_spacer.gif") {
            $(this).removeClass("ms-crm-ImageStrip-inlineedit_save");
            $(this).addClass("ms-crm-ImageStrip-inlineedit_save_hover");
        }
    });

    $("img.k2actionsbutton").mouseout(function () {
        if ($(this).attr("src") == "/_imgs/imagestrips/transparent_spacer.gif") {
            $(this).removeClass("ms-crm-ImageStrip-inlineedit_save_hover");
            $(this).addClass("ms-crm-ImageStrip-inlineedit_save");
        }
    });

    $("img.k2actionsbutton").click(function () {
        // get selected action value
        var el = $(this).parents().get(1);
        var action = $(el).find("select").val();

        if (action != null && action && action.length > 0) {
            K2SELECTEDACTION = action;
            Xrm.Utility.confirmDialog("Action the K2 worklist item with the following decision: " + action, action2013WorklistItem2013, null)

        } else {
            Xrm.Utility.alertDialog("Please select an action for the worklist item");
        }
    });




    // reveal added sections
    $("div[aria-k2='customsection']").slideDown("slow", function () {
    });

    $("div[aria-k2='customtab']").slideDown("slow", function () {
    });

}


function injectK2TabIntoForm(tabHtml) {

    var tabsLength = Xrm.Page.ui.tabs.getLength();
    var tabLocation = K2_CONFIG_TABPOSITION;

    if (K2_CONFIG_TABPOSITION < 1) {
        tabLocation = 0;

        c = $('div#tab' + String(tabLocation));
        if (c && c.length > 0) {
            $(tabHtml).insertBefore(c);
        }

    } else if (K2_CONFIG_TABPOSITION >= tabsLength) {
        tabLocation = parseInt(tabsLength) - 1;

        c = $('div#tab' + String(tabLocation));
        if (c && c.length > 0) {
            $(tabHtml).insertAfter(c);
        }

    } else {

        c = $('div#tab' + String(parseInt(tabLocation) - 1));
        if (c && c.length > 0) {
            $(tabHtml).insertAfter(c);
        }
    }
}


function injectK2SectionIntoForm(sectionHtml) {

    var tabsLength = Xrm.Page.ui.tabs.getLength();
    var tabLocation = K2_CONFIG_SECTION_TAB;

    if (K2_CONFIG_SECTION_TAB < 1) {
        tabLocation = 0;
    } else if (K2_CONFIG_SECTION_TAB > tabsLength) {
        tabLocation = tabsLength - 1;
    } else {
        //tabLocation = tabLocation ;
    }

    var sectionsLength = Xrm.Page.ui.tabs.get(parseInt([tabLocation])).sections.getLength();
    var sectionLocation = K2_CONFIG_SECTION_LOCATION;

    if (K2_CONFIG_SECTION_LOCATION < 1) {
        sectionLocation = 0;

        c = $($('div#tab' + String(tabLocation)).find("div.ms-crm-FormSection-Container").get(0));
        if (c && c.length > 0) {
            $(sectionHtml).insertBefore(c);
        }

    } else if (K2_CONFIG_SECTION_LOCATION >= sectionsLength) {
        sectionLocation = sectionsLength - 1;

        c = $($('div#tab' + String(tabLocation)).find("div.ms-crm-FormSection-Container").get(parseInt(sectionsLength) - 1));
        if (c && c.length > 0) {
            $(sectionHtml).insertAfter(c);
        }

    } else {

        c = $($('div#tab' + String(tabLocation)).find("div.ms-crm-FormSection-Container").get(parseInt(sectionLocation) - 1));
        if (c && c.length > 0) {
            $(sectionHtml).insertAfter(c);
        }
    }

}

function action2013WorklistItem2013() {

    if (K2SN != null && K2SN.length > 0) {
        var c = actionK2WorklistItem2013();

        // need a spinner to show a call is being made
        $("img.k2actionsbutton").removeClass("ms-crm-ImageStrip-inlineedit_save");
        $("img.k2actionsbutton").removeClass("ms-crm-ImageStrip-inlineedit_save_hover");
        //        $("img.k2actionsbutton").attr("src", "/_imgs/appportal/spin.gif");

        $("img.k2actionsbutton").removeClass("ms-crm-ImageStrip-inlineedit_save");
        $("img.k2actionsbutton").removeClass("ms-crm-ImageStrip-inlineedit_save_hover");
        $("img.k2actionsbutton").attr("src", "/_imgs/processcontrol/process_control_loading.gif");
        $("img.k2actionsbutton").attr("width", "16");
        $("img.k2actionsbutton").attr("height", "22");

        //.ms-crm-ImageStrip-refresh_commandbar_EmailLink{background:transparent url(/_imgs/imagestrips/refresh_commandbar_images.png?ver=1001177143) no-repeat scroll -73px -1px;width:16px;height:16px;overflow:hidden;}

        c.done(function (data) {

            // remove added sections
            $("div[aria-k2='customsection']").slideUp("slow", function () {
                $(this).remove();

                // show after animation
                Xrm.Utility.alertDialog("The worklist item was actioned successfully");

            });

            $("div[aria-k2='customtab']").slideUp("slow", function () {
                $(this).remove();
            });


            // refresh ribbon to remove actions
            K2ACTIONS = "";
            Xrm.Page.ui.refreshRibbon();

        })
        .fail(function (data) {
            $("img.k2actionsbutton").addClass("ms-crm-ImageStrip-inlineedit_save;");
            $("img.k2actionsbutton").attr("src", "/_imgs/imagestrips/transparent_spacer.gif");
            $("img.k2actionsbutton").attr("width", "16");
            $("img.k2actionsbutton").attr("height", "16");
            Xrm.Utility.alertDialog("An error occured while actioning the worklist item: " + data.status + " - " + data.statusText);
        })
        .always(function () {
            K2SELECTEDACTION = "";
            K2SN = "";
        });

    }
}

//
function actionK2WorklistItem2013() {

    var action = {
        'SerialNumber': K2SN,
        'SelectedCustomAction': K2SELECTEDACTION
    };

    return $.ajax({
        url: K2WFSERVICESBASEURI + 'api/worklistitem/',
        type: 'PUT',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        data: JSON.stringify(action),
        async: true,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        //error: errorWorklist
        //success: handleactionK2WorklistItem
    });
}

// isn't ncessary
function timedUpdateOfForm() {
    var tab0Column0 = $('#contentIFrame1').contents().find('div#tab0').find("div[class='ms-crm-tabcolumn0']");
    var column0 = $('div#tab0').find("div[class='ms-crm-tabcolumn0']");
    if (tab0Column0.length && tab0Column0.length > 0) {
        clearInterval(K2TIMER);
        tab0Column0.prepend(SIMPLEOUTPUT);
    } else if (column0.length && column0.length > 0) {
        clearInterval(K2TIMER);
        column0.prepend(SIMPLEOUTPUT);
    }
}

