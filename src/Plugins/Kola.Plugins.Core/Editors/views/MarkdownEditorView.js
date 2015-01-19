﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Handlebars = require('handlebars');
    var Template = require('text!../templates/MarkdownEditorTemplate.html');

    return Backbone.View.extend({

        template: Handlebars.compile(Template),

        events: {
            'focusout': function () { this.trigger('submit'); }
        },

        render: function (editMode) {
            if (editMode) {
                this.$el.html(this.template(this.model));
            }
            else {
                this.$el.html(this.model.value.value);
            }

            return this;
        },

        value: function () {
            return this.$el.find('textarea').val();
        }
    });
});