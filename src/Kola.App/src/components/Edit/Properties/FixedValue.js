﻿import React,  { Component } from 'react';

class FixedValue extends Component {
    render() {
        const divClasses = 'value ' + this.props.type;
        return <div className={divClasses} ref={el => this.el = el } onClick={this.handleClick}></div>;
    }

    componentDidMount() {
        var propertyType = this.props.type;

        this.editor = window.kola.propertyEditors.find(e => e.propertyType === propertyType);

        if (!this.editor) {
            console.log('No editor for property type ' + this.props.type);
        }

        this.componentDidUpdate();
    }

    componentDidUpdate() {
        if (this.editor) {
            this.editor.render({
                element: this.el,
                value: this.props.value ? this.props.value.value : '',
                editMode: false,
                onChange: this.handleChange,
                onCancel: this.props.onCancel
            });
        }
    }

    handleChange(value) {
        this.props.onChange({
            type: 'fixed',
            value: value
        });
    }

    handleClick(e) {
        if (this.props.editMode) {
            e.stopPropagation();
        }
    }
}

export default FixedValue;