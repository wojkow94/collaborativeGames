/// <reference path="../../_references.js" />

ProjektGrupowy.App.Mobile.viewControllers.GamePage = (function () {

    var c = new ProjektGrupowy.Framework.classes.Controller('Mobile', 'GamePage');

    var _gameId;
    var _gameDefId;

    return {

        init: function (gameId, gameDefId) {
            _gameId = gameId;
            _gameDefId = gameDefId;
        },

        actions: {

            proposedElements: function (elementDefId) {
                return c.action('ProposedElements', {
                    gameId: _gameId,
                    elementDefinitionId: elementDefId
                });
            },

            acceptedElements: function (elementDefId) {
                return c.action('AcceptedElements', {
                    gameId: _gameId,
                    elementDefinitionId: elementDefId
                });
            },

            elementListItem: function (elementId) {
                return c.action('ElementListItem', {
                    gameId: _gameId,
                    elementId: elementId
                });
            },

            addElementForm: function (elementDefId) {
                return c.action('AddElementForm', {
                    gameId: _gameId,
                    elementDefinitionId: elementDefId
                });
            },

            tokensView: function () {
                return c.action('TokensView', {
                    gameId: _gameId
                });
            },

            index: function (gameId) {
                return c.action('', {
                    gameId: gameId
                });
            }
        }
    }
}());


