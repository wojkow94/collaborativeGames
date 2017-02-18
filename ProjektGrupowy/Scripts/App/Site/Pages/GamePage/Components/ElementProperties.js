/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.ElementProperties = (function () {

    //-------------------------------------------------

    var viewControllers = {
        game: ProjektGrupowy.App.Site.viewControllers.GamePage
    }

    var models = {
        Game:       ProjektGrupowy.App.Common.Models.Game,
        GameEvents: ProjektGrupowy.App.Common.Models.Game.enums.events
    }
    var PopupForm = ProjektGrupowy.Framework.classes.PopupForm;

    //-------------------------------------------------

    var elementId;
    var container = undefined;
    var modelSubscriptions = [];
    var loaded = false;

    var config = {
        formEditElement: "#formEditElement",
        btnEditElement: ".btnEditElement"
    }

    var editElementForm = new PopupForm({
        formId: config.formEditElement
    });

    //-------------------------------------------------

    function refresh(event)
    {
        elementId = event.elementId;
        $(container).append('<div class="ui active inverted dimmer"><div class="ui loader"></div></div>');
        $.get(viewControllers.game.actions.elementPropertiesView(event.elementId), function (data) {
            $(container).html(data);
        });
    }

    function bindUIEvents() {
        $(container).on('click', config.btnEditElement, function () {
            var elementId = this.id;
            editElementForm.options.view = viewControllers.game.actions.editElementForm(elementId);
            editElementForm.options.onExecute = function(data, form) {
                models.Game.editElement(elementId, data, function (data) {
                    form.afterPost(data);
                });
                return false;
            }
            editElementForm.show();
        });
    }

    function load(cont) {
        container = cont;
        bindModelEvents();
        bindUIEvents();
        loaded = true;
    }

    function unload() {
        unbindModelEvents();
        loaded = false;
    }

    function bindModelEvents() {
        modelSubscriptions.push(
            models.Game.events.addEventListener(models.GameEvents.onSelectElement, refresh),

            models.Game.events.addEventListener(models.GameEvents.onEditElement, function(event) {
                if (event.elementId == elementId) refresh(event);
            }),

            models.Game.events.addEventListener(models.GameEvents.onMoveElement, function(event) {
                refresh(event);
            })
        );
    }

    function unbindModelEvents() {
        modelSubscriptions.map(function(subscription) {
          models.Game.events.removeEventListener(subscription)
        });
    }

    //-------------------------------------------------

    return {
        load: load,
        unload: unload,
        refresh: refresh
    }
}());
