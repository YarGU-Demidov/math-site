﻿class EditorConfig {
    /**
     * Initialize wysiwyg
     * @param {string} folder
     */
    initArea(folder) {
        $(document).ready(() => {
            const $contentArea = $('#content-textarea');
            // this._initCkEditor($contentArea);
            this._initFroala($contentArea, folder);
        });
    }

    _initCkEditor($contentArea) {
        const editor = CKEDITOR.instances.Content;
        
        editor.on(
            'change',
            (evt) => {
                $contentArea.val(evt.editor.getData());
            }
        );
    }

    _initFroala($contentArea, folder) {
        const uploadFileUrl = location.origin + `/api/froala/upload-file/${folder}`;
        const allowedImageFormats = ['jpeg', 'jpg', 'png', 'gif'];

        $contentArea.froalaEditor({
            // https://www.froala.com/wysiwyg-editor/docs/concepts/image/manager
            imageManagerLoadURL: location.origin + '/api/froala/get-images',
            imageManagerPageSize: 30,
            imageManagerLoadMethod: 'GET',
            imageManagerDeleteURL: '/api/froala/delete-image',
            imageManagerDeleteMethod: 'POST',

            // https://www.froala.com/wysiwyg-editor/docs/concepts/image/upload
            imageUploadParam: 'file',
            imageUploadURL: uploadFileUrl,
            imageUploadMethod: 'POST',
            imageMaxSize: 50 * 1024 * 1024, // Set max image size to 50MB.
            imageAllowedTypes: [...allowedImageFormats],

            // https://www.froala.com/wysiwyg-editor/docs/concepts/file/upload
            fileUploadParam: 'file',
            fileUploadURL: uploadFileUrl,
            fileMaxSize: 500 * 1024 * 1024, // Set max image size to 500MB.
            fileAllowedTypes: ['*'],
            fileUploadMethod: 'POST',

            language: 'ru',

            toolbarButtons: [
                'fullscreen',
                '|',
                'undo',
                'redo',
                '|',
                'bold',
                'italic',
                'underline',
                'strikeThrough',
                'subscript',
                'superscript',
                '|',
                'fontFamily',
                'fontSize',
                'color',
                'inlineStyle',
                'paragraphStyle',
                '|',
                'paragraphFormat',
                'align',
                'formatOL',
                'formatUL',
                'outdent',
                'indent',
                'quote',
                '-',
                'insertLink',
                'insertImage',
                'insertVideo',
                'embedly',
                'insertFile',
                'insertTable',
                '|',
                'emoticons',
                'specialCharacters',
                'insertHR',
                'selectAll',
                'clearFormatting',
                '|',
                'print',
                'spellChecker',
                'help',
                'html',
            ]
        });
    }
}