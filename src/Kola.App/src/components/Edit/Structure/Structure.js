﻿import React from 'react';
import Component from './Component';
import Toolbar from '../Toolbar';
import ToolbarContent from '../ToolbarContent';
import ToolbarButtonTray from '../ToolbarButtonTray';
import Button from '../Button';

const styles = {
    base: {
        width: '242px'
    },  
    content: {
        padding: '10px'
    }
}

const Structure = ({ components = [] }) => (
    <Toolbar style={styles.base}>
        <ToolbarContent style={styles.content}>
            {components.map((component, i) => <Component key={i} component={component} />)}
        </ToolbarContent>
        <ToolbarButtonTray>
            <Button title="Pin Toolbars" onClick={() => console.log('clicked')} icon='fa-cog' active={false} />
        </ToolbarButtonTray>
    </Toolbar>
);

export default Structure;
