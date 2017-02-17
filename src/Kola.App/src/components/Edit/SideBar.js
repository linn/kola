﻿import React from 'react';
import Button from './Button';
import { buttonTrayStyle }  from './commonStyles';

const styles = {
    normal: {
        position: 'relative',
        height: '100%',
        width: '60px',
        backgroundColor: '#333',
        color: '#eee',
        float: 'left',
        zIndex: '30'
    },
    label: {
        display: 'block',
        textAlign: 'center',
        padding: '18px 0',
        color: '#eee'
    },
    button: {
        normal:
        {
            border: 'none',
            padding: '18px 0',
            color: '#eee',
            backgroundColor: '#333',
            width: '100%'
        },
        hover:
        {
            color: '#fff',
            backgroundColor: '#555'
        },
        active: {
        },
        disabled: {
            color: '#555'
        }
    }
};

const SideBar = ({ togglePinToolbars = () => {}, toolbarsPinned, amendments, saveAmendments, undoAmendment }) => (
    <div style={styles.normal}>
        <div>
            <Button styles={styles.button} title="Save" enabled={amendments.length > 0} onClick={saveAmendments} icon="fa-save" />
            <Button styles={styles.button} title="Undo" enabled={amendments.length > 0} onClick={undoAmendment} icon="fa-undo" />
            <span style={styles.label}>{amendments.length}</span>
        </div>
        <div style={buttonTrayStyle}>
            <Button title="Pin Toolbars" onClick={togglePinToolbars} icon="fa-thumb-tack" active={toolbarsPinned} />
        </div>
    </div>
);

export default SideBar;