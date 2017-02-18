/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.GameSheet = (function () {

    var GamePageController = ProjektGrupowy.App.Site.viewControllers.GamePage

    var models = {
        Game:       ProjektGrupowy.App.Common.Models.Game,
        GameEvents: ProjektGrupowy.App.Common.Models.Game.enums.events
    }
    var PopupForm = ProjektGrupowy.Framework.classes.PopupForm;

    var config = {
        sheetContainer:     ".sheetContainer",
        btnTokenSort:       ".btnTokenSort",
        btnAttrSort:        ".btnAttrSort",
        btnPlayerSort:      ".btnPlayerSort",
        btnEditElement:     ".btnEditElement",
        formEditElement:    "#formEditElement"
    };

    var modelSubscriptions = [];
    var loaded = false;
    var container;

    var editElementForm = new PopupForm({
        formId: config.formEditElement
    });

    function bindUIEvents() {

        $(container).on('click', config.btnAttrSort, function () {
            models.Game.sortElements(this.dataset.elementdefid, 1, this.id);
        });

        $(container).on('click', config.btnTokenSort, function () {
            models.Game.sortElements(this.dataset.elementdefid, 2, this.id);
        });

        $(container).on('click', config.btnPlayerSort, function () {
            models.Game.sortElements(this.dataset.elementdefid, 3, this.id);
        });

        $(container).on('click', config.btnEditElement, function () {
            var elementId = this.id;
            editElementForm.options.view = GamePageController.actions.editElementForm(elementId);
            editElementForm.options.onExecute = function(data) {
                models.Game.editElement(elementId, data);
            }
            editElementForm.show();
        });
    }

    function unbindUIEvents() {
        $(container).off();
    }

    function bindModelEvents() {

        modelSubscriptions.push(
            models.Game.events.addEventListener(models.GameEvents.onAcceptElement, refresh),
            models.Game.events.addEventListener(models.GameEvents.onAddElement, refreshOneSheet),
            models.Game.events.addEventListener(models.GameEvents.onAddTokens, refresh),
            models.Game.events.addEventListener(models.GameEvents.onSetTokens, refresh),
            models.Game.events.addEventListener(models.GameEvents.onRejectElement, refresh),
            models.Game.events.addEventListener(models.GameEvents.onSort, refreshOneSheet)
        );
    }

    function unbindModelEvents() {
        modelSubscriptions.map(function(subscription) {
            models.Game.events.removeEventListener(subscription);
        });
    }

    function refresh() {
        $(container).siblings('.dimmer').dimmer('show');

        $.get(GamePageController.actions.elementsSheetsView(), function (data) {
            $(container).html(data);
            var sheets = $(config.sheetContainer);
            var count = 0;
            sheets.each(function (index, obj) {
                $.get(GamePageController.actions.elementsSheet(obj.id), function (data) {
                    $(config.sheetContainer).filter("#" + obj.id).html(data);
                    count++;
                    if (sheets.length == count) {
                        $(container).siblings('.dimmer').dimmer('hide');
                    }
                });
            });
        });
    };


    function refreshOneSheet(event) {
        var sheet = $(config.sheetContainer).filter("#" + event.elDefId);

        sheet.find('table').css('opacity', 0.5);
        $.get(GamePageController.actions.elementsSheet(event.elDefId), function (data) {
            sheet.html(data);
        });
    };

    function load(cont) {
        container = cont;
        bindModelEvents();
        bindUIEvents();
        refresh();
    }

    function unload() {
        $(container).empty();
        unbindModelEvents();
        unbindUIEvents();
        loaded = false;
    }

    return {
        load: load,
        unload: unload,
        refresh: refresh,
        refreshOneSheet: refreshOneSheet
    }
}());
