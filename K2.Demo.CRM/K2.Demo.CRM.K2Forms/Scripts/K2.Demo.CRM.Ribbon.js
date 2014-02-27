function K2GetActions(CommandProperties) {

    if (typeof K2ACTIONS != "undefined" && K2ACTIONS != null && K2ACTIONS.length > 0) {

        var RibbonActions = '<Menu Id="K2.Demo.ActionMenu"><MenuSection Id="K2.Demo.Action.MenuSection" Sequence="10"><Controls Id="K2.Demo.Action.Controls">';

        if (typeof K2ACTIONS != "undefined") {
            for (var i = 0; i < K2ACTIONS.length; i++) {
                RibbonActions += '<Button Id="K2.Demo.Action.Button' + i + '" Command="K2.form.ActionTask" Sequence="20" LabelText="' + K2ACTIONS[i] + '" Alt="' + K2ACTIONS[i] + '" TemplateAlias="o1" Image16by16="WebResources/k2_K2Action16x16.png" Image32by32="WebResources/k2_K2Action32x32.png"/>';
            }
        }
        else {
            RibbonActions += '<Button Id="K2.Demo.Action.ButtonNOTASKS" Command="K2.form.ActionTask" Sequence="20" LabelText="No K2 Actions" Alt="No K2 Actions" TemplateAlias="o1" Image16by16="WebResources/k2_K2Action16x16.png" Image32by32="WebResources/k2_K2Action32x32.png"/>';
        }
        RibbonActions += '</Controls></MenuSection></Menu>';

        CommandProperties.PopulationXML = RibbonActions;
    }
}


function K2ActionTaskFromRibbon(CommandProperties) {
    var controlId = CommandProperties.SourceControlId;
    var actionid = controlId.replace("K2.Demo.Action.Button", "");
    if (isNaN(actionid)) {
        //        alert("doing nothing");
    } else {
        actionK2WorklistItem(K2SN, K2ACTIONS[actionid]);
    }
    Xrm.Page.data.entity.save("saveandclose");
}

function K2SetRibbon() {

    if (typeof K2ACTIONS != "undefined") {
        return K2ACTIONS;
    } else {
        return false;
    }
}