/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.Tokens = (function () {

    var components = {
        TokensConfig: ProjektGrupowy.App.Site.components.GamePage.TokensConfig
    }

    var GamePageController =    ProjektGrupowy.App.Site.viewControllers.GamePage
    var GameModel =             ProjektGrupowy.App.Common.Models.Game;

    var config = {
        tokenLabel: ".tokenLabel",
        btnConfig: "button.config" 
    };
    var container;

    function load(cont) {
        container = cont;
        refresh();

        GameModel.events.addEventListener(GameModel.enums.events.onSetTokens, refresh);
        GameModel.events.addEventListener(GameModel.enums.events.onAddTokens, refresh);
        GameModel.events.addEventListener(GameModel.enums.events.onRejectElement, refresh);
        GameModel.events.addEventListener(GameModel.enums.events.onConfigTokens, refresh);
    }
    
    function refresh() {
        $.get(GamePageController.actions.tokensView(), function (data) {
            $(container).html(data);

            $(container).find(config.btnConfig).click(function () {
                components.TokensConfig.load();
            });

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
