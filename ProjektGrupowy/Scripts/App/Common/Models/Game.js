/// <reference path="../../_references.js" />

ProjektGrupowy.App.Common.Models.Game = (function () {

    var GameController = ProjektGrupowy.App.Api.Game;
    var EventEmitter = ProjektGrupowy.Framework.classes.EventEmitter;

    var enums = {
        view: {
            board: 1,
            sheet: 2
        },

        events: {
            onAddElement: 1,
            onRejectElement: 2,
            onAcceptElement: 3,
            onSelectElement: 4,
            onMoveElement: 5,
            onAddTokens: 6,
            onSetTokens: 7,
            onSetView: 8,
            onSort: 9,
            onJoinPlayer: 10,
            onLeavePlayer: 11,
            onEditElement: 12,
            onConfigTokens: 13,
            count: 14,
        },
    };

    var events = new EventEmitter(enums.events.count);

    return {

        enums: enums,
        events: events,

        getId: function() {
            return GameController.getGameId();
        },

        setView: function(view) {
            events.fireEvent(enums.events.onSetView, { view: view });
        },

        save: function (after) {
            $.post(GameController.actions.saveGame(), function (data) {
                after();
            });
        },

        selectElement: function (elementId) {
            events.fireEvent(enums.events.onSelectElement, { elementId: elementId });
        },

        addTokens: function (tokenId, elementId, tokensCount, after) {
            $.post(GameController.actions.addTokens(tokenId, elementId, tokensCount), function (data) {
                events.fireEvent(enums.events.onAddTokens, { tokenId: tokenId, elementId: elementId, tokensCount: tokensCount });
                after();
            });
        },

        setTokens: function (tokenId, elementId, tokensCount, after) {
            $.post(GameController.actions.setTokens(tokenId, elementId, tokensCount), function (data) {
                events.fireEvent(enums.events.onSetTokens, { tokenId: tokenId, elementId: elementId, tokensCount: tokensCount });
                after();
            });
        },

        setTokensConfig: function(configData, callback) {
            $.post(GameController.actions.setTokensConfig(), configData, function (result) {
                events.fireEvent(enums.events.onConfigTokens);
                callback(result);
            });
        },

        addElement: function (elDefId, elementData, after) {
            $.post(GameController.actions.addElement(elDefId), elementData, function (result) {
                if (result.Succeed === true) {
                    events.fireEvent(enums.events.onAddElement, { elDefId: elDefId, elementId: result.Data.elementId });
                }
                after(result);
            });
        },

        editElement: function (elementId, elementData, after) {
            $.post(GameController.actions.editElement(elementId), elementData, function (result) {
                if (result.Succeed === true) {
                    events.fireEvent(enums.events.onEditElement, { elementId: elementId });
                }
                after(result);
            });
        },

        //IMPORTANT!! accept element

        acceptElement: function (elDefId, elementId, containerId, regionId, eldefId) {
            $.post(GameController.actions.isElementAccepted(elementId), function (result) {
                $.post(GameController.actions.acceptElement(elementId, containerId, regionId), function (result2) {
                    if (result.Data.accepted == true) {
                        events.fireEvent(enums.events.onMoveElement, { elDefId: elDefId, elementId: elementId, containerId: containerId, regionId: regionId });
                    }
                    else {
                        events.fireEvent(enums.events.onAcceptElement, { elDefId: elDefId, elementId: elementId, containerId: containerId, regionId: regionId });
                    }
                });
            });
        },

        sortElements: function (elDefId, type, id) {
            $.post(GameController.actions.setSorting(elDefId, type, id), function (data) {
                events.fireEvent(enums.events.onSort, { elDefId: elDefId });
            });
        },

        rejectElement: function (elDefId, elementId) {
            $.post(GameController.actions.rejectElement(elementId), function (data) {
                events.fireEvent(enums.events.onRejectElement, { elDefId: elDefId, elementId: elementId });
            });
        },

        getPlayerTokensAmount: function (tokenDefId, elementId, callback) {
            $.post(GameController.actions.getPlayerTokensAmount(tokenDefId, elementId), function (data) {
                if (data.Succeed === true) {
                    callback(data.Data.tokensAmount);
                }
            });
        },

        canAccept: function (elementDefId, callback) {
            $.post(GameController.actions.canAccept(elementDefId), function (data) {
                callback(data);
            });
        },

        canAcceptElement: function (elementId, callback) {
            $.post(GameController.actions.canAcceptElement(elementId), function (data) {
                callback(data);
            });
        },
    }

}());
