﻿import React, { Component } from 'react';
import ComponentList from './ComponentList';
import Toolbar from '../Toolbar';
import ToolbarContent from '../ToolbarContent';
import ToolbarButtonTray from '../ToolbarButtonTray';
import Button from '../Button';

const styles = {
    base: {
        width: '250px'
    },  
    content: {
        padding: '10px'
    }
}

class Structure extends Component {
    state = {
        placeholderPath: ''
    }

    setPlaceholderPath(placeholderPath) {
        if (placeholderPath !== this.state.placeholderPath) {
            this.setState({ placeholderPath });
        }
    }

    render() {
        const { components = [], ...otherProps } = this.props;
        return (
            <Toolbar style={styles.base}>
                <ToolbarContent style={styles.content}>
                    <ComponentList 
                        components={components} 
                        componentPath="/" 
                        placeholderPath={this.state.placeholderPath} 
                        setPlaceholderPath={p => this.setPlaceholderPath(p) } 
                        style={{ minHeight: '100%' }} 
                        onAddComponent={e => this.handleAddComponent(e)} 
                        onMoveComponent={e => this.handleMoveComponent(e)} 
                        {...otherProps} />
                </ToolbarContent>
                <ToolbarButtonTray>
                    <Button title="Pin Toolbars" onClick={() => console.log('clicked')} icon="fa-cog" active={false} />
                </ToolbarButtonTray>
            </Toolbar>
        );
    }

    handleAddComponent(e) {
        this.props.addComponent(this.props.templatePath, e.componentPath, e.componentType);
        this.setState({ placeholderPath: '' });
    }

    handleMoveComponent(e) {
        if (e.sourcePath !== e.targetPath) {
            this.props.moveComponent(this.props.templatePath, e.sourcePath, e.targetPath);
        }
        this.setState({ placeholderPath: '' });
    }
}

export default Structure;
