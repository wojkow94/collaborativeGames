/// <reference path="../../_references.js" />

ProjektGrupowy.Framework.classes.PopupForm = function (options) {

    var Form = ProjektGrupowy.Framework.classes.Form;

    this.options = options;

    var config = {
        modalId: '.modal.ui'
    };

    this.show = function (width) {
        $(config.modalId).html('<div style="height: 200px"></div>')
        this.addDimer();
        $(config.modalId).modal('show');
        var self = this;

        $.get(this.options.view, function (data) {
            $(config.modalId).html(data);
            $(config.modalId).modal('refresh');
            $(config.modalId).find('.input').popup();

            var form = new Form(self.options.formId);
            form.actionUrl = self.options.action;

            if (self.options.onExecute) {
                form.onExecute = self.options.onExecute;
            }

            form.onSubmit = function (result) {
                if (self.options.onSubmit) self.options.onSubmit(result);
                $(config.modalId).modal("hide");
            }

            form.onPost = function () {
                self.addDimer();
                self.clearErrors();
            }

            form.onError = function (result) {
                self.addErrors(result.Messages);
                self.hideDimer();
            }
        });
    }

    this.addDimer = function()
    {
        $(config.modalId).append("<div class='ui active inverted dimmer'><div class='ui loader'></div></div>");
    }

    this.hideDimer = function()
    {
        $(config.modalId).find('.dimmer').remove();
    }

    this.addErrors = function (errors) {
        if (!errors) return;

        var errorsArea = $(config.modalId).find('.errors');
        errorsArea.show();
        errors.forEach(function (error) {
            errorsArea.append('<div class="ui negative message">' + error + '</div>');
        });
        $(config.modalId).modal('refresh');
    }

    this.clearErrors = function () {
        var errors = $(config.modalId).find('.errors');
        errors.hide();
        errors.empty();
    }

    this.hide = function () {
        $(config.modalId).modal("hide");
    }
};
