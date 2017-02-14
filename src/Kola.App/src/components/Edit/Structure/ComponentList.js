﻿import React from 'react';
import { DropTarget } from 'react-dnd';
import StructureComponent from './StructureComponent';
import Placeholder from './Placeholder';
import { arraysMatch, modifySiblingPath } from './helpers';
import { toIntArray } from '../../../utility';

const style = { minHeight: '38px' };

const dropTarget = {
    drop(props, monitor) {
        const { onAddComponent, onMoveComponent, placeholderPath = '' } = props;

        if (monitor.isOver({ shallow: true })) {
            if (monitor.getItemType() === 'COMPONENT_TYPE') {
                onAddComponent({
                    componentPath: placeholderPath,
                    componentType: monitor.getItem().name
                });
            } 
            else {
                onMoveComponent({
                    sourcePath: monitor.getItem().componentPath,
                    targetPath: modifySiblingPath(monitor.getItem().componentPath, placeholderPath)
                })
            }
        }
    },

    hover(props, monitor) {
        if (monitor.isOver({ shallow: true })) {
            const { componentPath, showPlaceholder, components } = props;
            if (components.length === 0) {
                showPlaceholder('/' + [...toIntArray(componentPath), 0].join('/'));
            }
        }
    }
};

function dropCollect(connect, monitor) {
    return {
        connectDropTarget: connect.dropTarget(),
        isOver: monitor.isOver({ shallow: true }),
        isOverChild: monitor.isOver()
    };
}

const insertPlaceholder = (components, componentPath, placeholderPathStr = '') => {
    const placeholderPath = toIntArray(placeholderPathStr);
    if (placeholderPath.length > 0) {
        const componentPathArray = toIntArray(componentPath);
        const placeholderParent = placeholderPath.slice(0, placeholderPath.length - 1);

        if (arraysMatch(componentPathArray, placeholderParent)) {
            const placeholderIndex = placeholderPath[placeholderPath.length - 1];
            return [ ...components.slice(0, placeholderIndex), { isPlaceholder: true }, ...components.slice(placeholderIndex) ]
        }
    }

    return components;
}

const ComponentList = ({ components, componentPath, connectDropTarget, ...props }) => connectDropTarget(
        <div style={style}>
            { insertPlaceholder(components, componentPath, props.placeholderPath).map((c, i) => c.isPlaceholder
                ? <Placeholder key={i} />
                : <StructureComponent key={i} component={c} {...props} />
            ) }
        </div>
);

export default DropTarget(['COMPONENT', 'COMPONENT_TYPE'], dropTarget, dropCollect)(ComponentList);