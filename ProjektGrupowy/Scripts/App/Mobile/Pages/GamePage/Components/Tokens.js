/// <reference path="../../_references.js" />

ProjektGrupowy.App.Mobile.components.GamePage.Tokens = (function () {

    var GamePageController =    ProjektGrupowy.App.Mobile.viewControllers.GamePage
    var GameModel =             ProjektGrupowy.App.Common.Models.Game;

    var config = {
        tokenLabel: ".tokenLabel"
    };
    var container;

    function load(cont) {
        container = cont;
        refresh();

        GameModel.events.addEventListener(GameModel.enums.events.onSetTokens, refresh);
        GameModel.events.addEventListener(GameModel.enums.events.onAddTokens, refresh);
        GameModel.events.addEventListener(GameModel.enums.events.onRejectElement, refresh);
    }
    
    function refresh() {
        $.get(GamePageController.actions.tokensView(), function (data) {
            $(container).html(data);

            $(container).find(config.tokenLabel).draggable({
                helper: 'clone',
                revert: 'invalid',
                scroll: false
            });
        });
    };

    return {
        load: load,
        refresh: refresh
    }
}());
