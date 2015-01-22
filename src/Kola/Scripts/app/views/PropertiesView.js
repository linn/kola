﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Handlebars = require('handlebars');
    var $ = require('jquery');
    var _ = require('underscore');
    var stateBroker = require('app/views/StateBroker');
    var PropertyView = require('app/views/PropertyView');
    var Template = require('text!app/templates/PropertiesTemplate.html');

    return Backbone.View.extend({

        template: Handlebars.compile(Template),

        initialize: function (options) {
            _.bindAll(this, 'render');
            this.amendments = options.amendments;
            stateBroker.on('selected', this.handleSelected, this);
        },

        events: {
            'click #remove': 'remove',
            'click #duplicate': 'duplicate'
        },

        handleSelected: function (model) {
            this.model = model;

            if (this.model !== null) {
                this.model.on('sync', this.render);
            }

            this.render();
        },

        render: function () {
            var self = this;

            if (this.model) {
                this.$el.html(this.template(this.model.toJSON()));

                var $tbody = this.$el.find('tbody').first();
                var componentPath = this.model.get('path');

                _.each(this.model.get('properties'), function (property) {
                    var $row = $tbody.append('<tr></tr>').find('tr').last();
                    var propertyView = new PropertyView({
                        model: property,
                        el: $row,
                        amendments: self.amendments,
                        componentPath: componentPath
                    });

                    propertyView.render();
                });
            }
            else {
                this.$el.html('');
            }

            return this;
        },

        remove: function (e) {
            e.preventDefault();
            var componentPath = this.model.get('path');
            this.amendments.removeComponent(componentPath);
            //this.stateBroker.deselect();
        },

        duplicate: function (e) {
            e.preventDefault();
            var componentPath = this.model.get('path');
            this.amendments.duplicateComponent(componentPath);
        }
    });
});