ProjektGrupowy.App.Mobile.pages.Layout = (function () {

    var Api = {
        site: ProjektGrupowy.App.Api.Site
    }

    var Components = {
        MainMenu: ProjektGrupowy.App.Mobile.components.Layout.MainMenu
    };
    
    function init() {
        Components.MainMenu.init();

        $('.ui.dropdown').dropdown();
        $('.ui.modal').modal({ inverted: true });
    }
    
    return {
        init: init
    };

}());