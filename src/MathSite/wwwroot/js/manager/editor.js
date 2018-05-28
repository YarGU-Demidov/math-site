class EditorConfig {
    /**
     * Initialize wysiwyg
     * @param {string} folder
     */
    initArea(folder, areaSelector) {
        areaSelector = areaSelector || '#Content';

        const $contentArea = $(areaSelector);

        // this._initCkEditor($contentArea);
        this._initFroala($contentArea, folder);
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

            //https://www.froala.com/wysiwyg-editor/docs/concepts/video/upload
            videoUploadParam: 'file',
            videoUploadURL: uploadFileUrl,
            videoUploadMethod: 'POST',
            videoMaxSize: 500 * 1024 * 1024, // Set max image size to 500MB.
            videoAllowedTypes: ['webm', 'mp4', 'ogg'],

            language: 'ru',
            heightMax: 500,

            fontSize: ['8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '30', '36', '48', '60', '72', '96', '100', '120'],

            key: 'OEM_LICENSE_KEY',

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