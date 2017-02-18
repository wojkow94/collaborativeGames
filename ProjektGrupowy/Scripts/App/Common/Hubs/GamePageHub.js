/// <reference path="../../_references.js" />

ProjektGrupowy.App.Common.Hubs.Game = (function () {

    var gameHub;
    var game = ProjektGrupowy.App.Common.Models.Game;

    var dispatchedEvents = [
        game.enums.events.onAddElement,
        game.enums.events.onRejectElement,
        game.enums.events.onAcceptElement,
        game.enums.events.onMoveElement,
        game.enums.events.onSetTokens,
        game.enums.events.onAddTokens,
        game.enums.events.onJoinPlayer,
        game.enums.events.onLeavePlayer,
        game.enums.events.onEditElement,
        game.enums.events.onConfigTokens
    ];

    var subscriptions = {};

    function init() {
        gameHub = $.connection.gameHub;

        gameHub.client.onEvent = function (eventId, eventObj) {
            //console.log('fire event ' + eventId + ' ' + eventObj);
            game.events.fireEventExcept(subscriptions[eventId], eventId, eventObj);
        }
    }

    return {
        run: function (playerId) {
            init();

            $.connection.hub.start().done(function () {

                dispatchedEvents.forEach(function(eventId) {
                    subscriptions[eventId] = game.events.addEventListener(eventId, function (eventObj) {
                        gameHub.server.onEvent(game.getId(), eventId, eventObj);
                    });
                });

                console.log('Now player '+playerId+'connected, connection ID=' + $.connection.hub.id);
                gameHub.server.addConnection($.connection.hub.id, game.getId(), playerId);
            });
        }
    }
}());
