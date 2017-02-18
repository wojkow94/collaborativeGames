/// <reference path="../../_references.js" />

ProjektGrupowy.App.Mobile.viewControllers.NewGamePage = (function () {

    var c = new ProjektGrupowy.Framework.classes.Controller('Mobile', 'NewGamePage');


    return {
    
        actions: {

            index: function () {
                return c.action('', {});
            },

            newGameForm: function () {
                return c.action('NewGameForm', {});
            }
        }
    };

}());