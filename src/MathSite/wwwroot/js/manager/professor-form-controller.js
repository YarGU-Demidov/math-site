class ProfessorFormController {
    constructor(formId) {
        this._$form = $(`#${formId}`);

        this._initHandlers();
    }

    _initHandlers() {
        this._$form.submit(this._onSubmit.bind(this));
    }

    _onSubmit() {
        this.listIds.forEach(this._setValues.bind(this));
    }

    _setValues(itemId) {
        const $item = this._$form.find(`#${itemId}`);
        const values = this.getValuesForFormItem($item);
        $item.val(values);
    }

    getValuesForFormItem($formItem) {
        const options = $formItem.find('option');
        const values = options.map(i => $(options[i]).val());
        return Array.from(values || []);
    }

    get listIds() {
        return [
            'Graduated',
            'BibliographicIndexOfWorks',
            'Theses',
            'TermPapers'
        ];
    }
}