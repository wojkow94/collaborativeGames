/// <reference path="../../_references.js" />

ProjektGrupowy.Framework.classes.Controller = function (moduleName, name) {

    var controllerName = name;
    var moduleName = moduleName;

    this.action = function (action, params) {
        var s = '/' + moduleName + '/' + controllerName + '/' + action + '?';

        for (var key in params) {
            if (params.hasOwnProperty(key)) {
                s += key + '=' + params[key] + '&';
            }
        }
        s = s.slice(0, -1);
        return s;
    }
};