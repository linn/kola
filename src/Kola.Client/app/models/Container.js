﻿var Backbone = require('backbone');
var _ = require('underscore');
var Component = require('app/models/Component');

module.exports = Component.extend({

    parse: function (response) {

        this.url = this.getLink(response.links, 'self');

        var Components = require('app/collections/Components');

        return _.extend(response,
        {
            components: new Components(response.components, { parse: true })
        });
    }
});
