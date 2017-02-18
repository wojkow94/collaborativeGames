/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.TokensConfig = (function () {

    var models = {
        Game: ProjektGrupowy.App.Common.Models.Game,
    }
    var controllers = {
        GamePage: ProjektGrupowy.App.Site.viewControllers.GamePage
    }
    

    var config = {
        modal: '.ui.modal',
        form: '#tokensConfigForm',
        btnSubmit: '.submit',
    };

    function bindUIEvents() {
        $(config.modal).find(config.form).submit(function (e) {
            e.preventDefault();

            var data = $(config.form).serialize();
            models.Game.setTokensConfig(data, function (result) {
                $(config.modal).modal('hide');
            });
        });
    }

    function load() {
        $(config.modal).modal('show');

        $.post(controllers.GamePage.actions.tokensConfig(), function (data) {
            $(config.modal).html(data);

            bindUIEvents();
        });
    }


    return {
        load: load
    }

}());