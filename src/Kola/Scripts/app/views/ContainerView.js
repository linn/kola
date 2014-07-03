﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Handlebars = require('handlebars');
    var Template = require('text!app/templates/ContainerTemplate.html');

    return Backbone.View.extend({

        template: Handlebars.compile(Template),

        tagName: 'li',

        className: 'container',

        initialize: function (options) {
            this.amendmentBroker = options.amendmentBroker;
            this.model.on('sync', this.render, this);
        },

        events: {
            "mouseover": "activate",
            "mouseout": "deactivate"
        },

        render: function () {
            var self = this;

            var componentViewFactory = require('app/views/ComponentViewFactory');

            this.$el.html(this.template());
            this.$el.attr('data-component-path', this.model.get('path'));

            var $list = this.$('ul').first();

            this.model.get('components').each(function (component) {
                var childView = componentViewFactory.build(component, self.amendmentBroker);
                $list.append(childView.render().$el);
            });

            $list.sortable({
                opacity: 0.75,
                placeholder: 'new',
                tolerance: 'pointer',
                connectWith: 'ul',
                stop: this.amendmentBroker.handleStop
            });

            return this;
        },

        activate: function (e) {
            this.model.trigger('active');
            e.stopPropagation();
        },

        deactivate: function (e) {
            this.model.trigger('inactive');
            e.stopPropagation();
        }
    });
});