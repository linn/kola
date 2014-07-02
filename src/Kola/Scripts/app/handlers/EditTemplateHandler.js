﻿define(function (require) {
    "use strict";

    var $ = require('jquery');
    var EditTemplateView = require('app/views/EditTemplateView');
    var ComponentTypes = require('app/collections/ComponentTypes');
    var Template = require('app/models/Template');

    return {
        execute: function (options, templatePath) {

            var d = $.Deferred();

            var componentTypes = new ComponentTypes();

            var template = new Template();
            template.url = '/_kola/templates/' + templatePath;

            $.when(componentTypes.fetch(), template.fetch()).then(function () {

                template.listenTo(template.amendments, 'sync', function (amendment) {
                    template.refresh(amendment.get('subject'));
                });

                d.resolve(new EditTemplateView({
                    componentTypes: componentTypes,
                    model: template,
                    router: options.router
                }));
            });

            return d.promise();
        }
    };
});
