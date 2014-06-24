﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var ComponentType = require('app2/models/ComponentType');

    return Backbone.Collection.extend({

        model: ComponentType,

        url: '/_kola/component-types'
    });
});
