﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Handlebars = require('handlebars');
    var Template = require('text!app/templates/AtomTemplate.html');


    return Backbone.View.extend({

        template: Handlebars.compile(Template),

        tagName: 'li',

        className: function () { return 'atom' },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.$el.attr('data-component-path', this.model.get('path'));

            return this;
        }
    });
});