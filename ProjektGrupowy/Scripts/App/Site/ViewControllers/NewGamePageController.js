/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.viewControllers.NewGamePage = (function () {

    var c = new ProjektGrupowy.Framework.classes.Controller('Site', 'NewGamePage');

    return {
        actions: {

            index: function () {
                return c.action('', {});
            },

            newGameForm: function () {
                return c.action('NewGameForm', {});
            },

            gameRules: function (gameId) {
                return c.action('GameRules', {gameId: gameId});
            }
        }
    }
}());