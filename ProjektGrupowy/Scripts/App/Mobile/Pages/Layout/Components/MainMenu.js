ProjektGrupowy.App.Mobile.components.Layout.MainMenu = (function () {

    var PopupForm = ProjektGrupowy.Framework.classes.PopupForm;

    var View = {
        homePage: ProjektGrupowy.App.Mobile.viewControllers.HomePage,
        newGame:  ProjektGrupowy.App.Mobile.viewControllers.NewGamePage,
        gamePage: ProjektGrupowy.App.Mobile.viewControllers.GamePage
    }

    var Models = {
        site: ProjektGrupowy.App.Common.Models.Site
    }

    var Api = {
        site: ProjektGrupowy.App.Api.Site,
        game: ProjektGrupowy.App.Api.Game
    }

    var config = {
        btnLogin:       '#btnLogin',
        btnRegister:    '#btnRegister',
        btnJoin:        '#btnJoin',
        btnProfile:     '#btnProfile',
        btnLogout:      '#btnLogout',
        btnLanguage:    '.btnLanguage',
        selectLanguage: '.selectLanguage',

        loginForm:      '.loginForm',
        registerForm:   '.registerForm',
        joinGameForm:   '#joinGameForm',
    };

    var forms = {
        login: new PopupForm({
            action: Api.site.actions.login(),
            view: View.homePage.actions.loginForm(),
            formId: config.loginForm,
            onSubmit: function (result) {
                location.reload();
            }
        }),

        joinGame: new PopupForm({
            action: Api.game.actions.joinGame(),
            view: View.homePage.actions.joinGameForm(),
            formId: config.joinGameForm,
            onSubmit: function (result) {
                window.location.href = View.gamePage.actions.index(result.Data.gameId);
            }
        }),

        register: new PopupForm({
            action: Api.site.actions.register(),
            view: View.homePage.actions.registerForm(),
            formId: config.registerForm,
            onSubmit: function (result) {
                location.reload();
            }
        })
    };

    function selectLan(lang) {
        $.post(Api.site.actions.setLanguage(lang), function (result) {
            if (result.Succeed == true) location.reload();
        });
    }


    function setLanguage(lang) {
        $(config.langSelect).closest('.ui.dropdown').dropdown("set selected", lang);
    }

    function bindEvents() {
        var self = this;

        $(config.btnLogin).click(function() {
            forms.login.show();
        });

        $(config.btnRegister).click(function() {
            forms.register.show();
        });

        $(config.btnJoin).click(function() {
            forms.joinGame.show();
        });

        $(config.btnProfile).click(function() {
        });

        $(config.btnLogout).click(function() {
            $.post(Api.site.actions.logout(), function(result) {
                location.reload();
            });
        });

        $('.ui.dropdown').dropdown();
        $(config.btnLanguage).click(function () {
            Models.site.setLanguage(this.id, function() {
                location.reload()
            });
        });
    }

    return {
        init: function() {
            bindEvents()

            Models.site.getLanguage(function(lang) {
                $(config.selectLanguage).dropdown("set selected", lang);
            })
        },
    }
}());
