/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.components.GamePage.TokensPopup = (function () {

    var models = {
        Game:       ProjektGrupowy.App.Common.Models.Game,
    }

    var modes = {
        add: 1,
        set: 2
    };

    var _mode = modes.add;
    var _tokenId = undefined;
    var _elementId = undefined;

    var config = {
        tokenPopup:     ".tokenPopup",
        cancelBtn:      ".cancel",
        okBtn:          ".ok",
        tokensCount:    '[name="tokensCount"]'
    };

    function bindUIEvents()
    {
        $(config.tokenPopup).find(config.cancelBtn).click(hide);

        $(config.tokenPopup).find(config.okBtn).click(function () {
            var tokensCount = $(config.tokenPopup).find(config.tokensCount).val();

            if (_mode == modes.add) {
                models.Game.addTokens(_tokenId, _elementId, tokensCount, hide);
            }
            else if (_mode == modes.set) {
                models.Game.setTokens(_tokenId, _elementId, tokensCount, hide);
            }
        });

        $(config.tokenPopup).find(config.tokensCount).keypress(function (event) {
            if (event.key === 'Enter') {
                var tokensCount = $(config.tokenPopup).find(config.tokensCount).val();
                if (_mode == modes.add) {
                    models.Game.addTokens(_tokenId, _elementId, tokensCount, hide);
                }
                else if (_mode == modes.set) {
                    models.Game.setTokens(_tokenId, _elementId, tokensCount, hide);
                }
            }
            if ((event.key === 'Escape')) {
                hide();
            }
        });
    }

    function load(x, y, tokenId, elementId, mode, amount, container)
    {
        if (amount == undefined) amount = 0;

        _mode = mode || modes.add;

        _elementId = elementId;
        _tokenId = tokenId;

        $(config.tokenPopup).css('left', x);
        $(config.tokenPopup).css('top', y);
        $(config.tokenPopup).fadeIn(500);
        $(config.tokenPopup).find(config.tokensCount).focus();
        $(config.tokenPopup).find(config.tokensCount).val(amount);
    };

    function hide()
    {
        $(config.tokenPopup).fadeOut(500);
    }

    function init() {
        bindUIEvents();
    }

    return {
        modes: modes,
        init: init,
        load: load,
        hide: hide
    }

}());
