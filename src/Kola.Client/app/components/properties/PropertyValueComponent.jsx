﻿var FixedPropertyValueComponent = require('app/components/properties/FixedPropertyValueComponent.jsx');
var InheritedPropertyValueComponent = require('app/components/properties/InheritedPropertyValueComponent.jsx');
var VariablePropertyValueComponent = require('app/components/properties/VariablePropertyValueComponent.jsx');
var React = require('react');

var Unset = (props) => {
    const divClasses = 'value ' + props.propertyType;
    return <div className={divClasses}></div>;
};

var PropertyValueComponent = React.createClass({

    propTypes: {
        editMode: React.PropTypes.bool.isRequired,
        propertyName: React.PropTypes.string.isRequired,
        propertyType: React.PropTypes.string.isRequired,
        propertyValue: React.PropTypes.object,
        onChange: React.PropTypes.func.isRequired,
        onCancel: React.PropTypes.func.isRequired
    },

    render: function () {
        const propertyValueType = this.props.propertyValue ? this.props.propertyValue.type : '';

        switch (propertyValueType) {
            case 'fixed':
                return <FixedPropertyValueComponent {...this.props} />;

            case 'inherited':
                return <InheritedPropertyValueComponent {...this.props} />;

            case 'variable':
                return <VariablePropertyValueComponent {...this.props} />;

            default:
                return <Unset />;
        };
    }
});

module.exports = PropertyValueComponent;
