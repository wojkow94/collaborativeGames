ProjektGrupowy.App.Mobile.pages.GamePage = (function () {


    var GamePageController =    ProjektGrupowy.App.Mobile.viewControllers.GamePage,
        GameController =        ProjektGrupowy.App.Api.Game,
        GameModel =             ProjektGrupowy.App.Common.Models.Game,
        GameHub =               ProjektGrupowy.App.Common.Hubs.Game;

    var PopupForm =             ProjektGrupowy.Framework.classes.PopupForm

    var components = {
        Tokens: ProjektGrupowy.App.Mobile.components.GamePage.Tokens,
        TokensPopup: ProjektGrupowy.App.Mobile.components.GamePage.TokensPopup
    };

    var config = {
        body:               ".body",
        elementItem:        ".elementItem",
        listProposed:       ".listProposed",
        listAccepted:       ".listAccepted",
        btnAddElement:      ".btnAddElement",
        formAddElement:     "#formAddElement",
        tokensContainer:    ".tokensContainer",
        tokenLabel:         ".tokenLabel",
    };

    var addElementForm = new PopupForm({
        formId: config.formAddElement,
    });

    function addElement(list, elDefId, elementId) {
        $.get(GamePageController.actions.elementListItem(elementId), function (data) {
            $(data).hide().appendTo(list.find('.accordion#' + elDefId)).fadeIn(500);
        });
    }

    function removeElement(list, elDefId, elementId) {
        $.get(GamePageController.actions.elementListItem(elementId), function (data) {
            list.find('.accordion#' + elDefId).find('#' + elementId).fadeOut(500, function () {
                this.remove();
            });
        });
    }

    function refreshElement(elementId) {
        $.get(GamePageController.actions.elementListItem(elementId), function (data) {

            var item = $(config.listAccepted + ", " + config.listProposed).find('.accordion').find('.elementItem#' + elementId);
            var isActive = false;
            if (item.find('.title').hasClass('active')) {
                isActive = true;
            }
            var newItem = $(data);
            if (isActive == true) {
                newItem.find('.title').addClass('active');
                newItem.find('.content').addClass('active');
            }
            item.replaceWith(newItem);
        });
    }

    function getElement(list, elDefId, elementId) {
        return list.find('.accordion#' + elDefId).find('#' + elementId);
    }

    function getList(list, elDefId) {
        return list.find('.accordion#' + elDefId);
    }

    function bindModelEvents() {
        GameModel.events.addEventListener(GameModel.enums.events.onAddElement, function (event) {
            addElement($(config.listProposed), event.elDefId, event.elementId);
        });

        GameModel.events.addEventListener(GameModel.enums.events.onRejectElement, function (event) {
            //getElement($(config.listAccepted), event.elDefId, event.elementId).appendTo(getList($(config.listProposed), event.elDefId));
            removeElement($(config.listAccepted), event.elDefId, event.elementId);
            addElement($(config.listProposed), event.elDefId, event.elementId);
        });

        //IMPORTANT!! on accept
        GameModel.events.addEventListener(GameModel.enums.events.onAcceptElement, function (event) {
            //getElement($(config.listProposed), event.elDefId, event.elementId).appendTo(getList($(config.listAccepted), event.elDefId));
            removeElement($(config.listProposed), event.elDefId, event.elementId);
            addElement($(config.listAccepted), event.elDefId, event.elementId);
        });

        GameModel.events.addEventListener(GameModel.enums.events.onEditElement, function (event) {
            updateElement(event.elementId);
        });

        GameModel.events.addEventListener(GameModel.enums.events.onMoveElement, function (event) {
            refreshElement(event.elementId);
        });

        GameModel.events.addEventListener(GameModel.enums.events.onAddTokens, function (event) {
            refreshElement(event.elementId);
        });

        GameModel.events.addEventListener(GameModel.enums.events.onSetTokens, function (event) {
            refreshElement(event.elementId);
        });
    }

    function bindUIEvents() {
        $(config.btnAddElement).click(function () {
            var elementDefId = this.id;
            addElementForm.options.view = GamePageController.actions.addElementForm(elementDefId);
            addElementForm.options.onExecute = function (data, form) {
                GameModel.addElement(elementDefId, data, function (data) {
                    form.afterPost(data);
                });
            }
            addElementForm.show();
        });

        $(config.listAccepted).on('click', config.tokenLabel, function (event) {
            var elementId = this.dataset.elementid;
            var tokenId = this.id;

            GameModel.getPlayerTokensAmount(tokenId, elementId, function (amount) {
                components.TokensPopup.load(event.pageX-100, event.pageY, tokenId, elementId, components.TokensPopup.modes.set, amount);
            });
        });
    }

    return {
        init: function (gameId, gameDefId, playerId) {

            GamePageController.init(gameId, gameDefId);
            GameController.init(gameId, gameDefId);

            bindModelEvents();
            bindUIEvents();
            GameHub.run(playerId);

            components.Tokens.load(config.tokensContainer);
            components.TokensPopup.init();

            $('.ui.dropdown').dropdown();
            $('.ui.menu .item').tab();
            $('.ui.accordion').accordion();
        }
    }
}());
