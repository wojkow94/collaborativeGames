/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.pages.GamePage = (function () {

    var GamePageController =    ProjektGrupowy.App.Site.viewControllers.GamePage;
        GameController =        ProjektGrupowy.App.Api.Game;
        GameModel =             ProjektGrupowy.App.Common.Models.Game;
        GameHub =               ProjektGrupowy.App.Common.Hubs.Game;

    var components = {
        GameBoard:              ProjektGrupowy.App.Site.components.GamePage.GameBoard, 
        GameSheet:              ProjektGrupowy.App.Site.components.GamePage.GameSheet,
        ProposedElements:       ProjektGrupowy.App.Site.components.GamePage.ProposedElements,
        ElementProperties:      ProjektGrupowy.App.Site.components.GamePage.ElementProperties,
        Tokens:                 ProjektGrupowy.App.Site.components.GamePage.Tokens,
        TokensPopup:            ProjektGrupowy.App.Site.components.GamePage.TokensPopup,
        Toolbox:                ProjektGrupowy.App.Site.components.GamePage.Toolbox,
        MenuBar:                ProjektGrupowy.App.Site.components.GamePage.MenuBaar,
        Players:                ProjektGrupowy.App.Site.components.GamePage.Players,
    };

    var config = {
        lefColumnId: ".leftColumn",
        rightColumnId: ".rightColumn",
        centerColumnId: ".contentContainer",
        elementPropertiesId: ".elementProperties",
        proposedElementsId: ".proposedElementsContainer",
        tokensContainerId: ".tokensContainer",
        playersContainerId: ".playersContainer",
    };
    
    var gameId;

    var callbacks = {
        onChange: function () { }
    }

    function onChangeView(event) {
        if (event.view == GameModel.enums.view.board) {
            components.GameSheet.unload();
            components.GameBoard.load(config.centerColumnId);
        }
        else {
            components.GameBoard.unload();
            components.GameSheet.load(config.centerColumnId);
        }
    }
    
    
    return {
        getGameId: function() {
            return gameId;
        },
    
        init: function (gId, gameDefId, playerId) {
 
            gameId = gId;

            GamePageController.init(gameId, gameDefId);
            GameController.init(gameId, gameDefId);
            GameHub.run(playerId);

            GameModel.events.addEventListener(GameModel.enums.events.onSetView, onChangeView);
            
            components.Toolbox.load();
            components.MenuBar.load();
            components.ProposedElements.load(config.proposedElementsId);
            components.GameBoard.load(config.centerColumnId);
            components.ElementProperties.load(config.elementPropertiesId);
            components.Tokens.load(config.tokensContainerId);
            components.Players.load(config.playersContainerId, playerId);
        }
    }
}());
