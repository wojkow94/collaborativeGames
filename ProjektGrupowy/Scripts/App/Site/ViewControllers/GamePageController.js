/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.viewControllers.GamePage = (function () {

    var controller = new ProjektGrupowy.Framework.classes.Controller('Site', 'GamePage');

    var _gameId;
    var _gameDefId;

    return {

        init: function (gameId, gameDefId) {
            _gameId = gameId;
            _gameDefId = gameDefId;
        },

        actions: {

            elementsSheet: function (elementDefId) {
                return controller.action('ElementsSheet', {
                    gameId: _gameId,
                    elementDefinitionId: elementDefId
                });
            },

            elementsSheetsView: function () {
                return controller.action('ElementsSheetsView', {
                    gameId: _gameId
                });
            },

            elementPropertiesView: function (elId) {
                return controller.action('ElementPropertiesView', {
                    gameId: _gameId,
                    elementId: elId
                });
            },

            boardElementLabel: function (elementId) {
                return controller.action('BoardElementLabel', {
                    gameId: _gameId,
                    elementId: elementId
                });
            },

            index: function (gameId) {
                return controller.action('', {
                    gameId: gameId
                });
            },

            proposedElementsView: function () {
                return controller.action('ProposedElementsView', {
                    gameId: _gameId
                });
            },

            gameBoardView: function () {
                return controller.action('GameBoardView', {
                    gameId: _gameId
                });
            },

            addElementForm: function (elementDefId) {
                return controller.action('AddElementForm', {
                    gameDefinitionId: _gameDefId,
                    elementDefinitionId: elementDefId
                });
            },

            editElementForm: function (elementId) {
                return controller.action('EditElementForm', {
                    gameId: _gameId,
                    elementId: elementId
                });
            },

            tokensView: function () {
                return controller.action('TokensView', {
                    gameId: _gameId
                });
            },

            playersView: function () {
                return controller.action('PlayersView', {
                    gameId: _gameId
                });
            },

            playerLabel: function (playerId) {
                return controller.action('PlayerLabel', {
                    gameId: _gameId,
                    playerId: playerId
                });
            },

            tokensConfig: function () {
                return controller.action('TokensConfig', {
                    gameId: _gameId,
                });
            }
        }
    }
}());