/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.MenuBaar = (function () {


    var HomePage = ProjektGrupowy.App.Site.viewControllers.HomePage;
    var models = {
        Game:       ProjektGrupowy.App.Common.Models.Game
    }

    var config = {
        showElementsBtn: "#showElementsBtn",
        showBoardBtn: "#showBoardBtn",
        saveGameBtn: "#saveGame",
        exitBtn: "#exitBtn",
        bar: ".menuBar"
    };

    function bindUIEvents() {

        $(config.showBoardBtn).click(function () {
            models.Game.setView(models.Game.enums.view.board);
        });

        $(config.exitBtn).click(function () {
            $(config.bar).dimmer('show');
            models.Game.save(function () {
                $(config.bar).dimmer('hide');
                window.location.href = '/Site';
            })
        });

        $(config.saveGameBtn).click(function () {
            $(config.bar).dimmer('show');
            models.Game.save(function() {
                $(config.bar).dimmer('hide');
            })
        });

        $(config.showElementsBtn).click(function () {
            models.Game.setView(models.Game.enums.view.sheet);
        });

        $('.ui.dropdown').dropdown();
        $(config.saveGameBtn).popup();
        $(config.showBoardBtn).popup();
        $(config.showElementsBtn).popup();
        $(config.exitBtn).popup();
    }

    return {
        load: function () {
            bindUIEvents();
        }
    }
}());
