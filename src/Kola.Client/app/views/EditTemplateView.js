﻿var Backbone = require('backbone');
var $ = require('jquery');
var _ = require('underscore');
var WysiwygEditorView = require('app/views/WysiwygEditorView');
var BlockEditorView = require('app/views/BlockEditorView');
var AmendmentsView = require('app/views/AmendmentsView');
var PropertiesView = require('app/views/PropertiesView');
var ToolboxView = require('app/views/ToolboxView');

var AmendmentBroker = require('app/views/AmendmentBroker');

var template = require('app/templates/EditTemplateTemplate.hbs');

module.exports = Backbone.View.extend({

    template: template,

    initialize: function (options) {

        this.uiStateDispatcher = _.clone(Backbone.Events);

        this.blockEditorView = new BlockEditorView({
            model: this.model,
            amendmentBroker: new AmendmentBroker(this.model.amendments),
            uiStateDispatcher: this.uiStateDispatcher
        });

        this.amendmentsView = new AmendmentsView({
            collection: this.model.amendments,
            uiStateDispatcher: this.uiStateDispatcher
        });

        this.wysiwygEditorView = new WysiwygEditorView({
            model: this.model,
            uiStateDispatcher: this.uiStateDispatcher
        });

        this.propertiesView = new PropertiesView({
            amendments: this.model.amendments,
            uiStateDispatcher: this.uiStateDispatcher
        });

        this.toolboxView = new ToolboxView({
            collection: options.componentTypes,
            uiStateDispatcher: this.uiStateDispatcher
        });

        this.uiStateDispatcher.on('toggle-tools', function () { this.$('.sidebar, .toolbars, .show-tools, .preview .address-bar').toggleClass('hidden'); }, this);
        this.uiStateDispatcher.on('toggle-toolbox', function () { this.$('.toggle-toolbox').toggleClass('selected'); }, this);
        this.uiStateDispatcher.on('toggle-block-editor', function () { this.$('.toggle-block-editor').toggleClass('selected'); }, this);
        this.uiStateDispatcher.on('toggle-properties', function () { this.$('.toggle-properties').toggleClass('selected'); }, this);
        this.uiStateDispatcher.on('toggle-pin-toolbars', function () { this.$('.toggle-pin-toolbars').toggleClass('selected'); this.$('.toolbars').toggleClass('pinned'); }, this);
    },

    events: {
        'click .show-tools': function () { this.uiStateDispatcher.trigger('toggle-tools'); },
        'click .hide-tools': function () { this.uiStateDispatcher.trigger('toggle-tools'); },
        'click .toggle-toolbox': function () { this.uiStateDispatcher.trigger('toggle-toolbox'); },
        'click .toggle-block-editor': function () { this.uiStateDispatcher.trigger('toggle-block-editor'); },
        'click .toggle-properties': function () { this.uiStateDispatcher.trigger('toggle-properties'); },
        'click .toggle-pin-toolbars': function () { this.uiStateDispatcher.trigger('toggle-pin-toolbars'); },
    },


    render: function () {
        this.$el.html(this.template());

        this.assign(this.toolboxView, '.toolbars .toolbox');
        this.assign(this.blockEditorView, '.toolbars .block-editor');
        this.assign(this.propertiesView, '.toolbars .properties');
        this.assign(this.wysiwygEditorView, '.preview');
        this.assign(this.amendmentsView, '.amendments');

        return this;
    }
});