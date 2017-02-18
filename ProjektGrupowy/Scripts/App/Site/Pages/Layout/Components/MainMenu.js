/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.Layout.MainMenu = (function () {

    var PopupForm = ProjektGrupowy.Framework.classes.PopupForm;

    var View = {
        homePage: ProjektGrupowy.App.Site.viewControllers.HomePage,
        newGame: ProjektGrupowy.App.Site.viewControllers.NewGamePage,
        gamePage: ProjektGrupowy.App.Site.viewControllers.GamePage
    }

    var models = {
        site: ProjektGrupowy.App.Common.Models.Site
    }

    var Api = {
        site: ProjektGrupowy.App.Api.Site,
        game: ProjektGrupowy.App.Api.Game
    }

    var config = {

        btnLogin:       '#btnLogin',
        btnRegister:    '#btnRegister',
        btnLogout:      '#btnLogout',
        btnEditProfile: '#btnEditProfile',
        btnNewGameBtn:  '#btnNewGame',
        btnJoinGameBtn: '#btnJoinGame',

        selectLanguage: '#selectLanguage',

        formLogin:      '#formLogin',
        formRegister:   '#formRegister',
        formJoinGame:   '#formJoinGame',
    };

    var forms = {
        login: new PopupForm({
            action: Api.site.actions.login(),
            view: View.homePage.actions.loginForm(),
            formId: config.formLogin,
            onSubmit: function (result) {
                location.reload();
            }
        }),

        joinGame: new PopupForm({
            action: Api.game.actions.joinGame(),
            view: View.homePage.actions.joinGameForm(),
            formId: config.formJoinGame,
            onSubmit: function (result) {
                location.href = View.gamePage.actions.index(result.Data.gameId);
            }
        }),

        register: new PopupForm({
            action: Api.site.actions.register(),
            view: View.homePage.actions.registerForm(),
            formId: config.formRegister,
            onSubmit: function (result) {
                location.reload();
            }
        })
    };

    function bindUIEvents() {

        $(config.btnEditProfile).click(function() {

        });

        $(config.btnLogin).click(function() {
            forms.login.show('30%');
        });

        $(config.btnRegister).click(function() {
            forms.register.show();
        });

        $(config.btnJoinGameBtn).click(function() {
            forms.joinGame.show();
        });

        $(config.btnNewGameBtn).click(function() {
            window.location.href = View.newGame.actions.index();
        });

        $(config.btnLogout).click(function() {
            $.post(Api.site.actions.logout(), function (result) {
                location.reload();
            })
        });

        $('.ui.dropdown').dropdown();
        $(config.selectLanguage).on('change', function () {
            models.site.setLanguage($(config.selectLanguage).val(), function() {
                location.reload()
            });
        });
    }

    function init() {
        bindUIEvents();

        models.site.getLanguage(function(lang) {
            $(config.selectLanguage).closest('.ui.dropdown').dropdown("set selected", lang);
        })
    }

    return {
        init: init
    }
}());
