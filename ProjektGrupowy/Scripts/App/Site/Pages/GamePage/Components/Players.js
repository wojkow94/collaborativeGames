

ProjektGrupowy.App.Site.components.GamePage.Players = (function () {

    var GamePageController =    ProjektGrupowy.App.Site.viewControllers.GamePage,
        GameModel =             ProjektGrupowy.App.Common.Models.Game;

    var container;
    var _playerId;

    function load(cont, playerId) {
        container = cont;
        _playerId = playerId;

        bindModelEvents();
        refresh();
    }

    function bindModelEvents() {

        GameModel.events.addEventListener(GameModel.enums.events.onJoinPlayer, function(event) {
            $.get(GamePageController.actions.playerLabel(event.playerId), function (data) {
                var label = $(container).find('#' + event.playerId);
                if (label.length == 0) {
                    $(data).hide().appendTo($(container)).fadeIn(500);
                }
                else {
                    label.replaceWith(data);
                }
            });
        });

        GameModel.events.addEventListener(GameModel.enums.events.onLeavePlayer, function(event) {
            $.get(GamePageController.actions.playerLabel(event.playerId), function (data) {
                var label = $(container).find('#' + event.playerId);
                if (label.length == 0) {
                    $(data).hide().appendTo($(container)).fadeIn(500);
                }
                else {
                    label.replaceWith(data);
                }
            });
        });

    }

    function refresh() {
        $.get(GamePageController.actions.playersView(), function (data) {
            $(container).html(data);
        });
    }

    return {
        load: load
    }

}());
