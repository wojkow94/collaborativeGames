/// <reference path="../../_references.js" />

ProjektGrupowy.App.Site.pages.Layout = (function() {

    var components = {
        MainMenu: ProjektGrupowy.App.Site.components.Layout.MainMenu
    };

    var config = { 
        modalId: '.ui.modal',
    };
    
    function init() {
        components.MainMenu.init();
    
        $(config.modalId).modal({ inverted: true });
    }
    
    return {
        init: init
    };

}());