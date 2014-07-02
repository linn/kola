﻿define(function (require) {
    "use strict";

    var _ = require('underscore');
    require('jqueryui');

    var AmendmentBroker = function (amendments) {
        this.amendments = amendments;
        _.bindAll(this, 'handleStop');
    };

    _.extend(AmendmentBroker.prototype, {

        handleStop: function (event, ui) {
            var newParent = ui.item.parent().closest('[data-component-path]');

            var targetPath = (newParent.length == 0)
                            ? '/' + ui.item.index().toString()
                            : this.combineUrls(newParent.data('component-path'), ui.item.index().toString());

            if (ui.item.hasClass('tool')) {
                var componentType = ui.item.data('href');

                this.amendments.addComponent(componentType, targetPath);
            }
            else {
                var sourcePath = ui.item.attr('data-component-path');

                this.amendments.moveComponent(sourcePath, targetPath);
            }
        }
    });

    return AmendmentBroker;
});
