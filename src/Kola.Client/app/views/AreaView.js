﻿var Backbone = require('backbone');
var template = require('app/templates/AreaTemplate.hbs');

module.exports = Backbone.View.extend({

    template: template,

    tagName: 'div',

    className: 'area',

    initialize: function (options) {
        this.amendmentBroker = options.amendmentBroker;

        this.model.on('sync', this.render, this);
        this.model.on('active', this.showActive, this);
        this.model.on('inactive', this.showInactive, this);
    },

    events: {
        'mouseover': 'handleMouseover',
        'mouseout': 'handleMouseout'
    },

    render: function () {
        var self = this;

        var componentViewFactory = require('app/views/ComponentViewFactory');

        this.$el.html(this.template(this.model.toJSON()));
        this.$el.attr('data-component-path', this.model.get('path'));

        var $list = this.$('ol').first();

        this.model.get('components').each(function (component) {
            var childView = componentViewFactory.build(component, self.amendmentBroker);
            $list.append(childView.render().$el);
        });

        $list.sortable({
            opacity: 0.75,
            placeholder: 'new',
            tolerance: 'pointer',
            connectWith: 'ol',
            stop: this.amendmentBroker.handleStop
        });

        return this;
    },

    handleMouseover: function (e) {
        e.stopPropagation();
        this.model.activate();
    },

    handleMouseout: function (e) {
        e.stopPropagation();
        this.model.deactivate();
    },

    showActive: function () {
        this.$el.addClass('active');
    },

    showInactive: function () {
        this.$el.removeClass('active');
    }
});