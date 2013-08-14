﻿define([
    'backbone'
], function (Backbone) {

    "use strict";

    return Backbone.Router.extend({

        routes: {
            '': 'home',
            'create': 'create'
        }

    });
});