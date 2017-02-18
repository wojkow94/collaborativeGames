/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.ProposedElements = (function () {

    var viewControllers = {
        game: ProjektGrupowy.App.Site.viewControllers.GamePage
    }
    
    var models = {
        Game:       ProjektGrupowy.App.Common.Models.Game,
        GameEvents: ProjektGrupowy.App.Common.Models.Game.enums.events
    }


    var config = {
        elementLabel: ".elementLabel",
    };

    var loaded = false,
        container = 0,
        modelSubscriptions = [];
    
    function bindUIEvents() {
        $(container).find(config.elementLabel).click(function () {
            models.Game.selectElement(this.id);
        });
    }
    
    function bindModelEvents() {

        modelSubscriptions.push(
            models.Game.events.addEventListener(models.GameEvents.onAcceptElement, refresh),
            models.Game.events.addEventListener(models.GameEvents.onAddElement, refresh),
            models.Game.events.addEventListener(models.GameEvents.onRejectElement, refresh)
        );
    }

    function enableDragNDrop(element)
    {
        $(element).draggable({
            helper: 'clone',
            revert: 'invalid',
            scroll: false,
            start: function () {
                $(this).css("opacity", 0.2);
            },
            stop: function () {
                $(this).css("opacity", 1);
            }
        });
    }
    
    function configureDragNDrop()
    {
        var canAccept = {};
        var labels = $(container).find(config.elementLabel);
        labels.each(function (index, label) {
            var value = canAccept[label.dataset.elementdefid];
            
            if (value == undefined) {
                models.Game.canAccept(label.dataset.elementdefid, function (result) {
                    if (result.Data.canAccept === true) {
                        enableDragNDrop(label);
                    }
                    canAccept[label.dataset.elementdefid] = result.Data.canAccept;
                });
            }
            else {
                if (value === true) {
                    enableDragNDrop(label);
                }
            }
        });
    }

    function refresh() {
        $(container).append('<div class="ui active inverted dimmer"><div class="ui loader"></div></div>');

        $.get(viewControllers.game.actions.proposedElementsView(), function (data) {
            $(container).html(data);

            configureDragNDrop();
            bindUIEvents();
        });
    }
    
    function load(cont) {
        container = cont;
        bindModelEvents();
        refresh();
        loaded: true;
    }
    
    return {
        load: load,
        refresh: refresh
    }
}());
