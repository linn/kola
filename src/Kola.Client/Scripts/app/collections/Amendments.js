﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var Amendment = require('app/models/Amendment');

    return Backbone.Collection.extend({

        parse: function (response, options) {

            if (options.url) {
                this.url = options.url;
            }

            return response;
        },

        addComponent: function (componentType, targetPath) {

            var amendment = new Amendment({
                componentType: componentType,
                targetPath: targetPath
            });

            amendment.url = this.combineUrls(this.url, 'addComponent');

            this.add(amendment);

            amendment.save();
        },

        moveComponent: function (sourcePath, targetPath) {
            var amendment = new Amendment({
                sourcePath: sourcePath,
                targetPath: targetPath
            });

            amendment.url = this.combineUrls(this.url, 'moveComponent');

            this.add(amendment);

            amendment.save();
        },

        removeComponent: function (componentPath) {
            var amendment = new Amendment({
                componentPath: componentPath
            });

            amendment.url = this.combineUrls(this.url, 'removeComponent');

            this.add(amendment);

            amendment.save();
        },

        duplicateComponent: function (componentPath) {
            var amendment = new Amendment({
                componentPath: componentPath
            });

            amendment.url = this.combineUrls(this.url, 'duplicateComponent');

            this.add(amendment);

            amendment.save();
        },

        applyAmendments: function () {
            var self = this;
            var amendment = new Amendment();

            amendment.url = this.combineUrls(this.url, 'apply');

            amendment.save().then(function () { self.fetch({ reset: true }); });
        },

        undoAmendment: function () {
            var self = this;
            var amendment = new Amendment();

            amendment.url = this.combineUrls(this.url, 'undo');

            amendment.save().then(function () { self.fetch({ reset: true }); });
        },

        setProperty: function (event) {
            var amendment = new Amendment({
                componentPath: event.componentPath,
                propertyName: event.propertyName,
                value: event.value
            });

            amendment.url = this.combineUrls(this.url, 'setProperty');

            this.add(amendment);

            amendment.save();
        },

        setComment: function (event) {
            var amendment = new Amendment({
                componentPath: event.componentPath,
                comment: event.comment
            });

            amendment.url = this.combineUrls(this.url, 'setComment');

            this.add(amendment);

            amendment.save();
        }
    });
});
