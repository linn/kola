﻿import React, { Component } from 'react';
import Atom from './Atom';
import { findDOMNode } from 'react-dom';
import Container from './Container';
import Widget from './Widget';
import { DropTarget } from 'react-dnd';

const componentMappings = {
    atom: Atom,
    container: Container,
    widget: Widget
}

const target = {
    drop(props, monitor) {
        const { onDrop, placeholderPath } = props;
        if (onDrop && monitor.isOver({ shallow: true })) {
            console.log('drop in StructureComponent');
            const item = monitor.getItem();
            onDrop({
                    componentPath: placeholderPath,
                    componentType: item.name
                }
            );
        }
    },

    hover(props, monitor, reactComponent) {
        if (monitor.isOver({ shallow: true })) {
            const { component, setPlaceholderPath } = props;

            // Determine rectangle on screen
            const hoverBoundingRect = findDOMNode(reactComponent).getBoundingClientRect();

            // Get vertical middle
            const hoverMiddleY = (hoverBoundingRect.bottom - hoverBoundingRect.top) / 2;

            // Determine mouse position
            const clientOffset = monitor.getClientOffset();

            // Get pixels to the top
            const hoverClientY = clientOffset.y - hoverBoundingRect.top;

            const componentPath = component.path.split('/').filter(s => s).map(s => parseInt(s, 10));

            // Dragging downwards
            if (hoverClientY <= hoverMiddleY) {
                setPlaceholderPath(componentPath);
            }

            // Dragging upwards
            if (hoverClientY > hoverMiddleY) {
                setPlaceholderPath([...componentPath.slice(0, componentPath.length - 1), componentPath[componentPath.length - 1] + 1]);
            }
        }
    }
};

function collect(connect, monitor) {
    return {
        connectDropTarget: connect.dropTarget(),
        isOver: monitor.isOver({ shallow: true }),
        isOverChild: monitor.isOver()
    };
}

class StructureComponent extends Component {
    render() {
        const { component, connectDropTarget, selectedComponent, highlightedComponent } = this.props;

        const selection = {
            isSelected: component.path === selectedComponent,
            isHighlighted: component.path === highlightedComponent
        }

        const handlers = {
            onClick: e => this.handleClick(e),
            onMouseOver: e => this.handleMouseOver(e),
            onMouseLeave: e => this.handleMouseLeave(e)
        };

        const TheComponent = componentMappings[component.type];

        return connectDropTarget(<div style={{paddingTop: '8px', paddingBottom: '8px'}}><TheComponent {...this.props} {...selection} {...handlers} /></div>);
    }

    handleClick(e) {
        e.stopPropagation();

        const { component, selectComponent } = this.props;
        selectComponent(component.path);
    }

    handleMouseOver(e) {
        e.stopPropagation();

        const { component, highlightComponent } = this.props;
        highlightComponent(component.path)
    }

    handleMouseLeave(e) {
        e.stopPropagation();

        const { component, unhighlightComponent } = this.props;
        unhighlightComponent(component.path)
    }
}

export default DropTarget('COMPONENT', target, collect)(StructureComponent);