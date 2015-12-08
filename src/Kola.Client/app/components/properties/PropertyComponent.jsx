﻿var PropertyHeaderComponent = require('app/components/properties/PropertyHeaderComponent.jsx');
var PropertyValueComponent = require('app/components/properties/PropertyValueComponent.jsx');
var _ = require('underscore');
var $ = require('jquery');
var React = require('react');

module.exports = React.createClass({
    propTypes: {
        onChange: React.PropTypes.func.isRequired,
        propertyName: React.PropTypes.string.isRequired,
        propertyType: React.PropTypes.string.isRequired,
        propertyValue: React.PropTypes.object.isRequired
    },

    render: function () {

        const childProps = {
            editMode: this.state.editMode,
            propertyName: this.props.propertyName,
            propertyType: this.props.propertyType,
            propertyValue: this.state.propertyValue
        };

        const divClass = this.state.editMode ? 'property editMode' : 'property';

        return (
            <div className={divClass} onClick={this.handleClick} onKeyUp={this.handleKeyUp}>
                <PropertyHeaderComponent {...childProps} onChange={this.handlePropertyValueTypeChange} />
                <PropertyValueComponent {...childProps} onChange={this.handlePropertyValueChange} />
            </div>
        );
    },

    componentWillMount: function () {
        this.processChange = _.once(this.processChange);
    },

    componentDidMount: function () {
        $(document.body).on('keyup', this.handleKeyUp);
    },

    componentWillUnmount: function() {
        $(document.body).off('keyup', this.handleKeyUp);
    },

    getInitialState: function () {
        return {
            editMode: false,
            propertyValue: this.props.propertyValue
        };
    },

    handleClick: function () {
        this.setState({ editMode: !this.state.editMode });
    },

    handleKeyUp: function (e) {
        if (e.which === 27) {
            this.doCancel();
        }
    },

    handlePropertyValueChange: function (propertyValue) {
        console.log('handlePropertyValueChange');

        if (true || this.valuesDiffer(this.props.propertyValue, propertyValue)) {
            this.processChange(propertyValue);
        }
    },

    processChange: function (propertyValue) {
        this.props.onChange({
            propertyName: this.props.propertyName,
            propertyType: this.props.propertyType,
            propertyValue: propertyValue
        });
    },

    handlePropertyValueTypeChange: function (propertyValueType) {
        if (this.state.propertyValue.type !== propertyValueType) {

            const propertyValue = propertyValueType === this.props.propertyValue.type
                ? this.props.propertyValue
                : { type: propertyValueType };

            if (propertyValue.type === 'inherited' && !propertyValue.key) {
                propertyValue.key = this.props.propertyName;
            }

            this.setState({ propertyValue: propertyValue });
        }
    },

    valuesDiffer: function (val1, val2) {

        if (val1.type !== val2.type) {
            return true;
        }
        
        if (val1.type === 'fixed' && val1.value !== val2.value) {
            return true;
        }

        if (val1.type === 'inherited' && val1.key !== val2.key) {
            return true;
        }

        return false
    },

    doCancel: function () {
        if (this.state.editMode) {
            this.setState({
                editMode: false,
                propertyValue: this.props.propertyValue
            });
        }
    }
});
