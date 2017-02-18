/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.pages.NewGamePage = (function () {

    var GamePageController =    ProjektGrupowy.App.Site.viewControllers.GamePage;
    var GameController =        ProjektGrupowy.App.Api.Game;
    var NewGamePageController = ProjektGrupowy.App.Site.viewControllers.NewGamePage;
    
    var PopupForm = ProjektGrupowy.Framework.classes.PopupForm;
    
    var config = {
        btnPlay: '.btnPlay',
        btnRules: '.btnRules',
        formNewGame:    "#formNewGame",  
    };
    
    var newGameForm = new PopupForm({
        view: NewGamePageController.actions.newGameForm(),
        formId: config.formNewGame,
        onSubmit: function (result) {
            window.location.href = GamePageController.actions.index(result.Data.gameId);
        }
    });
    
    function initPage() {
    
        $(config.btnPlay).click(function () {
            newGameForm.options.action = GameController.actions.newGame(this.id);
            newGameForm.show();
        });

        $(config.btnRules).click(function () {
            $('.ui.modal').html('<div style="height: 200px"></div>')
            $('.ui.modal').append("<div class='ui active inverted dimmer'><div class='ui loader'></div></div>");
            $('.ui.modal').modal('show');
            $('.ui.modal').modal('show');
            
            $.get(NewGamePageController.actions.gameRules(this.id), function (data) {
                $('.ui.modal').html('<div style="height: 85vh; overflow: auto">' + data + '</div>');
                $('.ui.modal div').scrollTop(0);
                $('.ui.modal').modal('refresh');
            });
        });
    }
    
    return {
        init: initPage
    }
}());


