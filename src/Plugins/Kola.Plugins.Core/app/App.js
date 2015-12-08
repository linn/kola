﻿var editorRegistry = require('app/EditorRegistry');
var textEditor = require('app/editors/TextEditor.jsx');
var multilineTextEditor = require('app/editors/MultilineTextEditor.jsx');
var markdownEditor = require('app/editors/MarkdownEditor.jsx');
var htmlStyleTypeEditor = require('app/editors/HtmlStyleTypeEditor.jsx');

editorRegistry.register('text', textEditor);
editorRegistry.register('multiline-text', multilineTextEditor);
editorRegistry.register('markdown', markdownEditor);
editorRegistry.register('html-style-type', htmlStyleTypeEditor);

