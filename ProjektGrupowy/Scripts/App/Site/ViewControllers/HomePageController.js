/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.viewControllers.HomePage = (function () {

    var c = new ProjektGrupowy.Framework.classes.Controller('Site', 'HomePage');

    return {
        actions: {

            loginForm: function () {
                return c.action('LoginForm', {});
            },

            registerForm: function () {
                return c.action('RegisterForm', {});
            },

            joinGameForm: function () {
                return c.action('JoinGameForm', {});
            },

            newGameForm: function () {
                return c.action('NewGameForm', {});
            },

            gamesListView: function () {
                return c.action('GamesListView', {});
            },

            newGamePage: function () {
                return c.action('NewGamePage', {});
            }
        }
    }

}());