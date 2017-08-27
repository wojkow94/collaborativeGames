/// <reference path="../../_references.js" />

ProjektGrupowy.App.Api.Game = (function () {

    var controller = new ProjektGrupowy.Framework.classes.Controller('Api', 'Game');

    var _gameId;
    var _gameDefId;

    return {

        init: function (gameId, gameDefId) {
            _gameId = gameId;
            _gameDefId = gameDefId;
        },

        getGameId: function () {
            return _gameId;
        },

        actions: {

            newGame: function (gameDefId) {
                return controller.action('NewGame', {
                    gameDefId: gameDefId
                });
            },

            setSorting: function (elementDefId, type, id) {
                return controller.action('SetSorting', {
                    gameId: _gameId,
                    elementDef: elementDefId,
                    type: type,
                    id: id
                });
            },

            saveGame: function () {
                return controller.action('SaveGame', {
                    gameId: _gameId
                });
            },

            joinGame: function () {
                return controller.action('JoinGame', {});
            },

            addElement: function (elementDefId) {
                return controller.action('AddElement', {
                    gameId: _gameId,
                    elementDefinitionId: elementDefId
                });
            },

            editElement: function (elementId) {
                return controller.action('EditElement', {
                    gameId: _gameId,
                    elementId: elementId
                });
            },

            acceptElement: function (elementId, containerId, regionId) {
                return controller.action('AcceptElement', {
                    gameId: _gameId,
                    containerId: containerId,
                    regionId: regionId,
                    elementId: elementId
                });
            },

            isElementAccepted: function (elementId) {
                return controller.action('IsElementAccepted', {
                    gameId: _gameId,
                    elementId: elementId
                });
            },

            rejectElement: function (elementId) {
                return controller.action('RejectElement', {
                    gameId: _gameId,
                    elementId: elementId
                });
            },

            addTokens: function (tokenDefId, elementId, amount) {
                return controller.action('AddTokens', {
                    gameId: _gameId,
                    tokenDefinitionId: tokenDefId,
                    elementId: elementId,
                    amount: amount
                });
            },

            setTokens: function (tokenDefId, elementId, amount) {
                return controller.action('SetTokens', {
                    gameId: _gameId,
                    tokenDefinitionId: tokenDefId,
                    elementId: elementId,
                    amount: amount
                });
            },

            getPlayerTokensAmount: function (tokenDefId, elementId) {
                return controller.action('GetPlayerTokensAmount', {
                    gameId: _gameId,
                    tokenDefinitionId: tokenDefId,
                    elementId: elementId
                });
            },

            canAccept: function (elementDefId) {
                return controller.action('CanAccept', {
                    gameId: _gameId,
                    elementDefId: elementDefId
                });
            },

            setTokensConfig: function(){
                return controller.action('SetTokensConfig', {
                    gameId: _gameId,
                });
            },

            canAcceptElement: function (elementId) {
                return controller.action('CanAcceptElement', {
                    gameId: _gameId,
                    elementId: elementId
                });
            }
        }
    }
}());
