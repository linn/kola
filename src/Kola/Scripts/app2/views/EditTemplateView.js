﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Handlebars = require('handlebars');
    var $ = require('jquery');

    var WysiwygEditorView = require('app2/views/WysiwygEditorView');
    var BlockEditorView = require('app2/views/BlockEditorView');
    var AmendmentsView = require('app2/views/AmendmentsView');
    var PropertiesView = require('app2/views/PropertiesView');
    var ToolboxView = require('app2/views/ToolboxView');

    var AmendmentBroker = require('app2/views/AmendmentBroker');

    var Template = require('text!app2/templates/EditTemplateTemplate.html');


    return Backbone.View.extend({

        template: Handlebars.compile(Template),

        initialize: function (options) {

            this.amendmentsView = new AmendmentsView({
                collection: this.model.amendments
            });

            this.blockEditorView = new BlockEditorView({
                model: this.model,
                amendmentBroker: new AmendmentBroker(this.model.amendments)
            });

            this.wysiwygEditorView = new WysiwygEditorView({
            });

            this.propertiesView = new PropertiesView({
            });

            this.toolboxView = new ToolboxView({
                collection: options.componentTypes
            });
        },

        render: function () {
            this.$el.html(this.template());
            this.assign(this.amendmentsView, '#amendments');
            this.assign(this.blockEditorView, '#block-editor');
            this.assign(this.wysiwygEditorView, '#wysiwyg-editor');
            this.assign(this.propertiesView, '#properties');
            this.assign(this.toolboxView, '#toolbox');

            return this;
        }
    });
});