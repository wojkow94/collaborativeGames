/// <reference path="../../_references.js" />

ProjektGrupowy.Framework.classes.Form = function (formId) {

    this.config = {
        formId: formId
    };

    

    this.actionUrl = '';

    this.onExecute = undefined;
    this.onSubmit = function (result) { }
    this.onError = function (result) { }
    this.onPost = function () { }

    this.asyncSubmit = function () {
        var self = this;
        var data = $(this.config.formId).serialize();

        if (this.onExecute === undefined) {
            $.post(this.actionUrl, data, function (data) {
                self.afterPost(data);
            });
            this.onPost();
        }
        else {
            this.onExecute(data, self);
        }
    };
    
    this.afterPost = function(data) {
        if (data.Succeed === true) {
            this.onSubmit(data);
        }
        else {
            this.onError(data);
        }
    }

    this.bindEvents = function () {
        var self = this;

        $(this.config.formId).submit(function () {
            self.asyncSubmit();
            return false;
        });
    };

    this.init = function () {
        this.bindEvents();
        $(this.config.formId).find('.ui.dropdown').dropdown();
    }

    this.init();
};