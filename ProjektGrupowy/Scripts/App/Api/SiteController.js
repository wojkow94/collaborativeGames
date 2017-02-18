/// <reference path="../../_references.js" />

ProjektGrupowy.App.Api.Site = (function () {

    var c = new ProjektGrupowy.Framework.classes.Controller('Api', 'Site');

    return {
        actions: {

            login: function () {
                return c.action('Login', {});
            },

            register: function () {
                return c.action('Register', {});
            },

            logout: function () {
                return c.action('Logout', {});
            },

            setLanguage: function (cultureName) {
                return c.action('SetLanguage', {
                    cultureName: cultureName
                });
            },

            getLanguage: function () {
                return c.action('GetLanguage', {});
            }
        }
    }
}());
