/// <reference path="../../_references.js" />

ProjektGrupowy.App.Mobile.viewControllers.HomePage = (function () {

    var c = new ProjektGrupowy.Framework.classes.Controller('Mobile', 'HomePage');

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
            }
        }
    }

}());