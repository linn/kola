﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Handlebars = require('handlebars');
    var Template = require('text!/_kola/editors/templates/TextEditorTemplate.html');

    return Backbone.View.extend({

        template: Handlebars.compile(Template),

        initialize: function () {

        },

        //        events: {
        //            "mouseover": "activate",
        //            "mouseout": "deactivate",
        //            "click": "select"
        //        },

        render: function () {
            this.$el.html(this.template(this.model));

            return this;
        }
    });
});