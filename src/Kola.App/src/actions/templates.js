﻿import { fetchJSON, postJSON } from './helpers/fetchJson';
import config from '../config';

export const RECEIVE_COMPONENT_TYPES = 'RECEIVE_COMPONENT_TYPES';
export const RECEIVE_TEMPLATE = 'RECEIVE_TEMPLATE';
export const SELECT_COMPONENT = 'SELECT_COMPONENT';
export const HIGHLIGHT_COMPONENT = 'HIGHLIGHT_COMPONENT';
export const UNHIGHLIGHT_COMPONENT = 'UNHIGHLIGHT_COMPONENT';
export const RECEIVE_AMENDMENT = 'RECEIVE_AMENDMENT';
export const RECEIVE_COMPONENT = 'RECEIVE_COMPONENT';

export const receiveComponentTypes = components => ({
    type: RECEIVE_COMPONENT_TYPES,
    payload: components
});

export const receiveTemplate = template => ({
    type: RECEIVE_TEMPLATE,
    payload: template
});

export const selectComponent = componentPath => ({
    type: SELECT_COMPONENT,
    payload: componentPath
});

export const highlightComponent = componentPath => ({
    type: HIGHLIGHT_COMPONENT,
    payload: componentPath
});

export const unhighlightComponent = componentPath => ({
    type: UNHIGHLIGHT_COMPONENT,
    payload: componentPath
});

export const receiveAmendment = amendment => ({
    type: RECEIVE_AMENDMENT,
    payload: amendment
});

export const receiveComponent = component => ({
    type: RECEIVE_COMPONENT,
    payload: component
});


export const fetchComponentTypes = () => async dispatch => {
    try {
        const data = await fetchJSON(`${config.appRoot}/component-types`);
        dispatch(receiveComponentTypes(data));
    } catch (error) {
        console.log(`request failed ${error}`);
    }
}

export const fetchComponent = (templatePath = '/test', componentPath) => async dispatch => {
    try {
        const data = await fetchJSON(`${config.appRoot}/templates/components?templatePath=${templatePath}&componentPath=${componentPath}`);
        dispatch(receiveComponent(data));
    } catch (error) {
        console.log(`request failed ${error}`);
    }
}

export const fetchTemplate = templatePath => async dispatch => {
    try {
        const data = await fetchJSON(`${config.appRoot}/templates?templatePath=${templatePath}`);
        dispatch(receiveTemplate(data));
    } catch (error) {
        console.log(`request failed ${error}`);
    }
}

const postAmendment = async (dispatch, templatePath, amendment) => {
    try {
        const data = await postJSON(`${config.appRoot}/templates/amendments?templatePath=${templatePath}`, amendment);
        dispatch(receiveAmendment(data));
    } catch (error) {
        console.log(`post failed ${error}`);
    }
}

export const addComponent = (componentPath, componentType) => async dispatch => {
    postAmendment(dispatch, '/test',
    {
        amendmentType: 'addComponent',
        componentType: componentType,
        targetPath: '/3/0/0'
    });
};

export const moveComponent = (sourcePath, targetPath) => async dispatch => {
    postAmendment(dispatch, '/test',
    {
        amendmentType: 'moveComponent',
        sourcePath: '/0/0/0',
        targetPath: '/1/0/0'
    });
};

