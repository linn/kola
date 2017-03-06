﻿import React, { Component } from 'react';
import classnames from 'classnames';

const defaultStyles = {
    normal:
    {
        height: '36px',
        width: '36px',
        color: '#999',
        backgroundColor: 'transparent',
        borderWidth: '2px',
        borderColor: 'transparent',
        borderStyle: 'solid'
    },
    hover:
    {
        color: '#fff',
        borderColor: '#999'
    },
    active: {
        color: '#ccc',
        backgroundColor: '#555'
    },
    disabled: {
        color: '#555'
    }
};

class Button extends Component {
    state = {
        hovering: false
    }

    render() {
        const { title, icon, caption } = this.props;

        const styles = this.buildStyles();

        const iconClass = classnames('fa fa-lg', icon);

        return (
            <button style={styles} className="transition-all" type="button" onMouseEnter={() => this.toggleHover()} onMouseLeave={() => this.toggleHover()} title={title} onClick={() => this.handleClick()}>
            {caption ? caption : false}
            {icon ? <i className={iconClass}></i> : false}
            </button>
        );
    }

    buildStyles() {
        const { active, enabled = true, styles = defaultStyles } = this.props;

        if (!enabled) {
            return { ...styles.normal, ...styles.disabled };
        }

        const hoverStyle = this.state.hovering ? { ...styles.normal, ...styles.hover } : styles.normal;

        return active ? { ...hoverStyle, ...styles.active } : hoverStyle;
    }

    toggleHover() {
        this.setState({hovering: !this.state.hovering})
    }

    handleClick() {
        const { onClick, enabled = true } = this.props;

        if (enabled && onClick) {
            onClick();
        }
    }
}

export default Button;