/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.Toolbox = (function () {

    var GamePageController = ProjektGrupowy.App.Site.viewControllers.GamePage
    var PopupForm = ProjektGrupowy.Framework.classes.PopupForm;

    var models = {
        Game:       ProjektGrupowy.App.Common.Models.Game,
    }

    var config = {
        addElement: ".addElement",
        addElementForm: "#addElementForm"
    };

    var addElementForm = new PopupForm({
        formId: config.addElementForm
    });

    function bindUIEvents()
    {
        $(config.addElement).popup();

        $(config.addElement).click(function () {
            var elementDefId = this.id;
            addElementForm.options.view = GamePageController.actions.addElementForm(elementDefId);
            addElementForm.options.onExecute = function(data, form) {
                models.Game.addElement(elementDefId, data, function (data) {
                    form.afterPost(data);
                });
            }
            addElementForm.show();
        });
    }

    return {
        load: function() {
            bindUIEvents();
        }
    }
}());
