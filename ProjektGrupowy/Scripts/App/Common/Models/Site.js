/// <reference path="../../_references.js" />

ProjektGrupowy.App.Common.Models.Site = (function () {

    var Api = {
        site: ProjektGrupowy.App.Api.Site
    };
    
    return {
        setLanguage: function (lang, success, fail) {
            if (!lang) return;

            $.post(Api.site.actions.setLanguage(lang), function (result) {
                if (result.Succeed == true) {
                    success();
                }
                else if (fail){
                    fail();
                }
            });
        },

        getLanguage: function (success, fail) {
            $.post(Api.site.actions.getLanguage(), function (result) {
                if (result.Succeed == true) {
                    success(result.Data.culture);
                }
                else if (fail) {
                    fail(result);
                }
            });
        }
    }

}());