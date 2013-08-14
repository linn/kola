﻿define([
    'backbone',
    'app/views/AppView',
    'app/Router'
], function (
    Backbone,
    AppView,
    Router) 
{
    "use strict";
    return {

        init: function () {
            var appView = new AppView({
                router: new Router()
            });

            appView.render();

            Backbone.history.start({ pushState: true });
        }
    };
});