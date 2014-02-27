// default configuration values - will be read from K2Settings entity or configured via configureK2URLs()
var K2VIEWFLOWURL = 'http://dlx:81/workspace/Tasklistcontrol/ViewFlowMain.aspx?ProcessId=';
var K2WFSERVICESBASEURI = 'http://dlx.denallix.com:81/K2Services/';
var K2CUSTOMRESTSERVICESBASEURI = 'http://dlx.denallix.com/K2.Demo.CustomREST/';
var K2SMARTOBJECTSERVICESBASEURI = 'http://dlx.denallix.com:8888/smo/';

var K2_CONFIG_TABPOSITION = 99;
var K2_CONFIG_ACTIONS = false;
var K2_CONFIG_VIEWFLOW = false;
var K2_CONFIG_DATAFIELDS = false;
var K2_CONFIG_PROCESSINFORMATION = false;
var K2_CONFIG_ENTITYDATAFIELDNAME = "";
var K2_CONFIG_QUERYK2SETTINGS = true;
var K2_CONFIG_QUERYK2SETTINGS_COMPLETE = false;
var K2_CONFIG_CHECKFORTASKS = true;
var K2_CONFIG_CHECKFORTASKS_COMPLETE = false;
var K2_CONFIG_USEFORMSETTINGS = true;
var K2_CONFIG_USEFORMSETTINGS_COMPLETE = false;
var K2_CONFIG_FORMNAME = "Information";
var K2_CONFIG_OVERRIDEFORM = false;

var K2SN = "";
var K2ACTIONSRIBBON = false;
var K2ACTIONS;


function setConfigCheckForTasks(checktasks) {
    K2_CONFIG_CHECKFORTASKS = checktasks;
}

function setConfigUseFormSettings(settings) {
    K2_CONFIG_USEFORMSETTINGS = settings;
}

function setConfigTabPostion(tabpos) {
    K2_CONFIG_TABPOSITION = tabpos;
}

function setConfigActions(actions) {
    K2_CONFIG_ACTIONS = actions;
}

function setConfigVeiwFlow(viewflow) {
    K2_CONFIG_VIEWFLOW = viewflow;
}

function setConfigDataFields(datafields) {
    K2_CONFIG_DATAFIELDS = datafields;
}

function setConfigProcessInformation(procinfo) {
    K2_CONFIG_PROCESSINFORMATION = procinfo;
}

function setConfigProcessInformation(dfname) {
    K2_CONFIG_ENTITYDATAFIELDNAME = dfname;
}

function setConfigQueryK2Settings(k2settings) {
    K2_CONFIG_QUERYK2SETTINGS = k2settings;
}

function setConfigFormName(form) {
    K2_CONFIG_FORMNAME = form;
}


function getFormConfigurationFromEntity(callAsync) {

    var restRequestURL = Xrm.Page.context.getServerUrl() + "/xrmservices/2011/OrganizationData.svc/k2_k2formassociationsSet?$select=k2_actionssection,k2_checktasks,k2_datafieldssection,k2_entityiddatefieldname,k2_entitylogicalname,k2_formname,k2_k2formassociationsId,k2_name,k2_processinformationsection,k2_tablocation,k2_viewflowsection&$filter=k2_entitylogicalname eq '" + Xrm.Page.data.entity.getEntityName() + "'";
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

    configureK2Forms(settings['k2_tablocation'], settings['k2_actionssection'], settings['k2_datafieldssection'], settings['k2_viewflowsection'], settings['k2_processinformationsection'], settings['k2_entityiddatefieldname'], settings['k2_checktasks'], settings['k2_formname']);

    K2_CONFIG_USEFORMSETTINGS_COMPLETE = true;
}

function errorGetFormConfigurationFromEntity(data) {
    alert("Error retrieving form settings from entity: " + data.status + " - " + data.statusText);
}


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

    var task;

    if (data.d.results != null && data.d.results != undefined) {
        // take the first task if multiple are returned
        task = data.d.results[0];
    } else {
        task = data.d;
    }

    if (task.k2_k2serialnumber != null && task.k2_k2serialnumber) {
        if (task.k2_k2serialnumber != "") {
            K2SN = task.k2_k2serialnumber;
        }
    }
    K2_CONFIG_CHECKFORTASKS_COMPLETE = true;
}

function errorCheckForTasks(data) {
    alert("Error retrieving associated Tasks: " + data.status + " - " + data.statusText);
}

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

function configureK2Forms(k2TabPosition, k2Actions, k2DataFields, k2ViewFlow, k2ProcessInformation, k2entitydatafield, k2checktasks, k2formname) {
    K2_CONFIG_TABPOSITION = k2TabPosition;
    K2_CONFIG_ACTIONS = k2Actions;
    K2_CONFIG_VIEWFLOW = k2ViewFlow;
    K2_CONFIG_DATAFIELDS = k2DataFields;
    K2_CONFIG_PROCESSINFORMATION = k2ProcessInformation;
    K2_CONFIG_ENTITYDATAFIELDNAME = k2entitydatafield;
    K2_CONFIG_CHECKFORTASKS = k2checktasks;
    K2_CONFIG_FORMNAME = k2formname;
}

function setTabPosition(afterTab) {
    K2TABPOSITION = afterTab;
}

function k2OnFormSave() {
    if (Xrm.Page.ui.getFormType() == 2) {
        var k2act = $('select#k2actions option:selected').val()

        var lcK2SN = K2SN;

        if ((lcK2SN != null && lcK2SN != "") && (k2act != null && k2act != "")) {
            // call service to action K2 worklist
            actionK2WorklistItem(lcK2SN, k2act);
        }
    }
}

function actionK2WorklistItem(SN, k2Action) {
    $.ajax({
        //url: 'http://dlx:81/K2Services/REST.svc/Worklist/Items(' + SN + ')/Actions(' + k2Action + ')/Execute?$format=json',
        url: K2WFSERVICESBASEURI + 'REST.svc/Worklist/Items(' + SN + ')/Actions(' + k2Action + ')/Execute?$format=json',
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "JSON",
        async: true,
        beforeSend: function (XMLHttpRequest) {
            XMLHttpRequest.setRequestHeader("Accept", "application/json");
        },
        crossDomain: false,
        error: errorWorklist
        //success: handleactionK2WorklistItem
    });
}

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

        //        if (K2SN == "") {
        //            // check query string for SN
        //            sn = extra.get("k2_sn", "");
        //            if (sn != "") {
        //                K2SN = sn;
        //            }
        //        }

        // get entity id
        entityId = removeGUIDBrackets(Xrm.Page.data.entity.getId());

        if (K2SN != "") {
            getK2WorklistBySN(K2SN);
        } else if (entityId != "" && !K2_CONFIG_CHECKFORTASKS) {
            getK2WorklistByEntityId(K2_CONFIG_ENTITYDATAFIELDNAME, entityId);
        }
    }
}

function getK2WorklistBySN(SN) {
    $.ajax({
        //        url: 'http://dlx:81/K2Services/REST.svc/Worklist/Items(' + SN + ')?piDataField=true&actXmlField=true&$format=json',
        url: K2WFSERVICESBASEURI + 'REST.svc/Worklist/Items(' + SN + ')?piDataField=true&actXmlField=true&$format=json',
        method: 'GET',
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
        updateK2CRMForm(data[0])
    }
}

function handlegetK2WorklistBySN(data) {

    if (data.Code == "-1") {
        alert("The assign K2 task is currently being worked on by another user");
        return;
    }

    // assumes a single worklist item is being returned
    if (data.Actions.length > 0) {
        updateK2CRMForm(data);
    }
}

function errorWorklist(data) {
    alert(data.status + " - " + data.statusText);
}


function updateK2CRMForm(data) {

    if (data == null || data == undefined || data == "") {
        return;
    }

    // swap to role form if specified
    var currentItem = Xrm.Page.ui.formSelector.getCurrentItem();
    if (K2_CONFIG_FORMNAME != "" && !K2_CONFIG_OVERRIDEFORM) {
        var items = Xrm.Page.ui.formSelector.items.get();
        if (items.length > 1) {
            for (var i = 0; i < items.length; i++) {
                if ($.trim(currentItem.getLabel()) != $.trim(K2_CONFIG_FORMNAME)) {
                    if ($.trim(items[i].getLabel()) == $.trim(K2_CONFIG_FORMNAME)) {
                        items[i].navigate();
                    }
                }
            }
        }
    }


    var shortcutclass = "";
    var k2actions = "";
    var k2datafields = "";
    var k2crmviewflow = "";
    var k2crmprocessinfo = "";

    // set K2SN - currently a global var
    // used for k2OnFormSave
    K2SN = data.SerialNumber;

    var isLastTab = false;

    if (K2_CONFIG_TABPOSITION >= Xrm.Page.ui.tabs.getLength() - 1) {
        // intentionally setting to second last tab as generated html doesn't currently work with last tab
        isLastTab = false;
        K2_CONFIG_TABPOSITION = Xrm.Page.ui.tabs.getLength() - 2;
    }

    populateK2ActionsArray(data);

    if (isLastTab) {
        shortcutclass = 'id=formNavTreeLineBottom class="ms-crm-ImageStrip-formNavTreeLineBottom ms-crm-FormSelector-SubItem"'
    }
    else {
        shortcutclass = 'id=formNavTreeLine class="ms-crm-ImageStrip-formNavTreeLine ms-crm-FormSelector-SubItem"';
    }


    var k2crmtable_start = '<DIV id="tab1978" class="ms-crm-InlineTab" control="[object Object]" IsViewportTab="0"><TABLE id="{33387f28-0cf9-4a14-ae41-42517014ac2c}_Header" class="ms-crm-InlineTabHeader ms-crm-InlineTabBorder" cellSpacing="0" cellPadding="0"><TBODY><TR><TD class="ms-crm-InlineTabHeaderExpander"><A tabIndex="1270" href="#" _events="[object Object]"><IMG id="{33387f28-0cf9-4a14-ae41-42517014ac2c}_Expander" class="ms-crm-InlineTabExpander ms-crm-ImageStrip-tab_down" alt="Collapse this tab" src="/_imgs/imagestrips/transparent_spacer.gif"></A></TD><TD class="ms-crm-InlineTabHeaderText"><A class="ms-crm-InlineTabHeaderText" tabIndex="1271" _events="[object Object]" href="#" name="{33387f28-0cf9-4a14-ae41-42517014ac2c}"><H2 class="ms-crm-Form">K2</H2></A></TD></TR></TBODY></TABLE>'
    + '<TABLE class="stdTable" cellSpacing="0" cellPadding="0"><COLGROUP><COL width="100%"><TBODY><TR><TD vAlign="top">';

    if (K2_CONFIG_ACTIONS) {
        k2actions = showK2Actions(data);
    }

    if (K2_CONFIG_DATAFIELDS) {
        k2datafields = showK2DataFields(data);
    }

    if (K2_CONFIG_VIEWFLOW) {
        k2crmviewflow = showK2ViewFlow(data);
    }

    if (K2_CONFIG_PROCESSINFORMATION) {
        k2crmprocessinfo = showK2ProcessInformation(data);
    }

    var k2crmtable_end = '</TD></TR></TBODY></TABLE></DIV></body></html>';

    var html = k2crmtable_start + k2actions + k2datafields + k2crmviewflow + k2crmprocessinfo + k2crmtable_end;

    // add shortcut
    var k2crmshortcut = '<LI class="ms-crm-FormSelector"><A id="tab1978Tab" class="ms-crm-FormSelector-SubItem" tabIndex="0" onclick="loadArea(' + "'areaForm'" + ');crmForm.GetTab($get(' + "'tab1978'" + ', crmForm), true);" href="#" target=_self><IMG ' + shortcutclass + ' alt="" align="absMiddle" src="/_imgs/imagestrips/transparent_spacer.gif"><NOBR class="ms-crm-FormSelector-SubItem" title="K2">K2</NOBR>&nbsp; </A></LI>';

    if (K2_CONFIG_ACTIONS || K2_CONFIG_DATAFIELDS || K2_CONFIG_VIEWFLOW || K2_CONFIG_PROCESSINFORMATION) {
        if (isLastTab) {
            $('div#crmFormTabContainer').append(html);
            $('ul#ulTabLinks').append(k2crmshortcut);
        }
        else {
            $(html).insertAfter('div.ms-crm-InlineTab:eq(' + K2_CONFIG_TABPOSITION + ')');
            $(k2crmshortcut).insertAfter('li.ms-crm-FormSelector:eq(' + K2_CONFIG_TABPOSITION + ')');
        }
    }
}

// handles K2 Task Action ribbon extension if it exists
function populateK2ActionsArray(data) {
    var len = data.Actions.length;
    if (len > 0) {
        K2ACTIONS = new Array();
        for (var i = 0; i < len; i++) {
            K2ACTIONS[i] = data.Actions[i].Name;
        }
        K2ACTIONSRIBBON = true;
        Xrm.Page.ui.refreshRibbon();
    }
}

function showK2Actions(data) {
    // construct actions
    var actionshtml = "";
    var len = data.Actions.length;
    for (var i = 0; i < len; i++) {
        actionshtml += '<option value="' + data.Actions[i].Name + '">' + data.Actions[i].Name + '</option>';
    }

    var k2actions = '<TABLE style="TABLE-LAYOUT: fixed" id="{6ba7bae9-3e0c-4f6a-ba5e-0ae38c8a2ed6}" class="ms-crm-FormSection" cellSpacing="0" cellPadding="3" height="1%" valign="top" IsViewportSection="0"><COLGROUP><COL width="115"><COL><COL class="FormSection_WriteColumnHeaders_col" width="135"><COL><TBODY><TR><TD class="ms-crm-Form-Section ms-crm-Form-SectionBar" colSpan="4"><H3 class="ms-crm-Form">Decision</H3></TD></TR><TR height="5"><TD></TD></TR><TR vAlign="top"><TD id="k2actions_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="k2actions">Actions</LABEL></TD><TD id="k2actions_d"><SELECT style="IME-MODE: auto" id="k2actions" class="ms-crm-SelectBox " tabIndex=1370 name="k2actions" attrPriv="7" attrName="k2actions" req="0"><OPTION title="" value=""></OPTION>'
    + actionshtml + '</SELECT></TD></TR><TR><TD colSpan="4"><DIV style="DISPLAY: none" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal" sl="false"><LABEL></LABEL></DIV></TD></TR></TBODY></TABLE>';

    return k2actions;
}

function showK2DataFields(data) {

    // TODO: Change formating so that each datafield spans two columns
    // TODO: Update datafields in process on save

    var k2crmprocessinfo = '<TABLE style="TABLE-LAYOUT: fixed" id="{7fa5ea84-c0fa-433c-9b94-cef1518726bb}" class="ms-crm-FormSection" cellSpacing="0" cellPadding="3" height="1%" valign="top" IsViewportSection="0"><COLGROUP><COL width="115"><COL><COL class="FormSection_WriteColumnHeaders_col" width="135"><COL><TBODY><TR><TD class="ms-crm-Form-Section ms-crm-Form-SectionBar" colSpan="4"><H3 class="ms-crm-Form">Process Data Fields</H3></TD></TR><TR height="5"><TD></TD></TR>';

    k2crmprocessinfo += '<TR vAlign="top">';
    for (var i = 0; i < data.ProcessInstance.DataFields.length; i++) {

        if (K2_CONFIG_DATAFIELDS.toLowerCase() == "all" || K2_CONFIG_DATAFIELDS.toLowerCase() == "true" || checkArray(K2_CONFIG_DATAFIELDS.toLowerCase(), "|", data.ProcessInstance.DataFields[i].Name.toLowerCase())) {
            var lbl = "k2df_" + i;
            k2crmprocessinfo += '<TD id="' + lbl + '_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="' + lbl + '">' + data.ProcessInstance.DataFields[i].Name + '</LABEL></TD><TD id="' + lbl + '_d" colspan="3" isAutoExpanding="FALSE" formXmlColSpan="2"><INPUT style="IME-MODE: active" id="' + lbl + '" class="ms-crm-Text" contentEditable="true" tabIndex="1180" value="' + data.ProcessInstance.DataFields[i].Value + '" maxLength="250" attrFormat="text" attrPriv="7" attrName="' + lbl + '" req="0"></TD>';
            //            k2crmprocessinfo += '<TD colSpan="2"><DIV style="DISPLAY: none" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal" sl="false"><LABEL></LABEL></DIV></TD></TR>';
            k2crmprocessinfo += '</TR>';

        }
    }

    k2crmprocessinfo += '</TBODY></TABLE>';

    return k2crmprocessinfo;
}

function showK2ViewFlow(data) {
    return '<TABLE style="TABLE-LAYOUT: fixed; HEIGHT: 440px" id="{680207a4-aa3c-486c-bade-3838fb340e1d}" class="ms-crm-FormSection" cellSpacing="0" cellPadding="3" valign="top" label="K2 View Flow" IsViewportSection="1"><COLGROUP><COL width="115"><COL><COL class="FormSection_WriteColumnHeaders_col" width="135"><COL><TBODY><TR><TD class="ms-crm-Form-Section ms-crm-Form-SectionBar" colSpan="4"><H3 class="ms-crm-Form">K2 View Flow</H3></TD></TR><TR height="25" vAlign="top"><TD id="IFRAME_k2viewflow_d" rowSpan="16" colSpan="4"><DIV style="DISPLAY: none" id="IFRAME_k2viewflow_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal" sl="false"><LABEL for="IFRAME_k2viewflow"></LABEL></DIV><IFRAME id="IFRAME_k2viewflow" class="ms-crm-Custom" tabIndex="1310" src="' + K2VIEWFLOWURL + data.ProcessInstance.ID + '" frameBorder="0" preload="0" url="' + K2VIEWFLOWURL + data.ProcessInstance.ID + '"></IFRAME></TD></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="25" vAlign="top"></TR><TR height="100%" vAlign="top"></TR></TBODY></TABLE>';
}

function showK2ProcessInformation(data) {
    var k2crmprocessinfo = '<TABLE style="TABLE-LAYOUT: fixed" id="{f335cf1f-22a4-46a7-b2bf-d8063b0152d3}" class="ms-crm-FormSection" cellSpacing="0" cellPadding="3" height="1%" valign="top" IsViewportSection="0"><COLGROUP><COL width="115"><COL><COL class="FormSection_WriteColumnHeaders_col" width="135"><COL><TBODY><TR><TD class="ms-crm-Form-Section ms-crm-Form-SectionBar" colSpan="4"><H3 class="ms-crm-Form">Process Information</H3></TD></TR><TR height="5"><TD></TD></TR><TR vAlign="top"><TD id="processname_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="processname">Process Name</LABEL></TD><TD id="processname_d"><INPUT style="IME-MODE: active" id="processname" class="ms-crm-Text ms-crm-ReadOnly" disabled contentEditable="false" tabIndex="1180" value="'
    + data.ProcessInstance.FullName + '" maxLength="250" attrFormat="text" attrPriv="7" attrName="processname" req="0"></TD><TD id="processtartdate_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="processtartdate">Process Start</LABEL></TD><TD id="processtartdate_d"><INPUT style="IME-MODE: active" id="processtartdate" class="ms-crm-Text ms-crm-ReadOnly" disabled contentEditable="false" tabIndex="1190" maxLength="250" attrFormat="text" attrPriv="7" attrName="processtartdate" req="0" value="'
    + formatJSONDate(data.ProcessInstance.StartDate) + '"></TD></TR><TR vAlign="top"><TD id="folio_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="folio">Folio</LABEL></TD><TD id="folio_d"><INPUT style="IME-MODE: active" id="folio" class="ms-crm-Text ms-crm-ReadOnly" disabled contentEditable="false" tabIndex="1220" value="'
    + data.ProcessInstance.Folio + '" maxLength="50" attrFormat="text" attrPriv="7" attrName="folio" req="0"></TD><TD colSpan="2"><DIV style="DISPLAY: none" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal" sl="false"><LABEL></LABEL></DIV></TD></TR><TR vAlign="top"><TD id="activityname_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="activityname">Activity Name</LABEL></TD><TD id="activityname_d"><INPUT style="IME-MODE: active" id="activityname" class="ms-crm-Text ms-crm-ReadOnly" disabled contentEditable="false" tabIndex="1180" value="'
    + data.ActivityInstanceDestination.Name + '" maxLength="250" attrFormat="text" attrPriv="7" attrName="activityname" req="0"></TD><TD id="activitystartdate_c" class="ms-crm-FieldLabel-LeftAlign ms-crm-Field-Normal"><LABEL for="activitystartdate">Activity Start</LABEL></TD><TD id="activitystartdate_d"><INPUT style="IME-MODE: active" id="activitystartdate" class="ms-crm-Text ms-crm-ReadOnly" disabled contentEditable="false" tabIndex="1190" maxLength="250" attrFormat="text" attrPriv="7" attrName="activitystartdate" req="0" value="'
    + formatJSONDate(data.ActivityInstanceDestination.StartDate) + '"></TD></TR></TBODY></TABLE>';

    return k2crmprocessinfo;
}