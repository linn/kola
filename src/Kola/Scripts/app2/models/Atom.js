﻿define(function (require) {
    "use strict";

    var Backbone = require('backbone');
    var $ = require('jquery');
    var _ = require('underscore');

    return Backbone.Model.extend({

        parse: function (response) {

            return response;
        }
    });
});
