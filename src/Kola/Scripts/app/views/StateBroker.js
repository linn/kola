﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var _ = require('underscore');

    var StateBroker = function () {
        _.bindAll(this, 'select');

        this.selected = null;
    };

    _.extend(StateBroker.prototype, {

        select: function (component) {
            var self = this;

            component.fetch({ propertyListRefresh: true }).then(function () {

                if (self.selected) {
                    self.selected.trigger('deselected');
                }

                if (self.selected == component) {
                    self.selected = null;
                }
                else {
                    self.selected = component;
                    self.selected.trigger('selected');
                    self.selected.trigger('active');
                }

                self.trigger('change');
            });
        },

        deselect: function () {
            if (this.selected) {
                this.selected.trigger('deselected');
                this.selected = null;
                this.trigger('change');
            }
        },

        highlight: function (component) {
            if (this.selected == null) {
                component.trigger('active');
            }
        },

        unhighlight: function (component) {
            if (this.selected == null) {
                component.trigger('inactive');
            }
        }


    }, Backbone.Events);

    return StateBroker;
});
