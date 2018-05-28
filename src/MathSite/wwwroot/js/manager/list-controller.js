class ListController {
    constructor(id) {
        this._id = id;
        this._$formField = $(`#${id}`);
        this._$container = $(`.${id}__container`);
        this._initHandlers();
        this._initSelection();
    }

    _initSelection() {
        this._$formField.val([]);
    }

    _initHandlers() {
        this._$addButton.click(this.addClickHandler.bind(this));
        this._$editButton.click(this.editClickHandler.bind(this));
        this._$removeButton.click(this.removeClickHandler.bind(this));
        
        this._$formField.change(this._selectionChanged.bind(this));
    }

    _selectionChanged() {
        const selected = this._selectedItems;
        
        if (selected && selected.length) {
            this._enableRemoveButton();
            this._enableEditButton();
        } else {
            this._disableRemoveButton();
            this._disableEditButton();
        }
    }

    _disableRemoveButton() {
        this._$removeButton.addClass('disabled');
    }

    _disableEditButton() {
        this._$editButton.addClass('disabled');
    }

    _enableRemoveButton() {
        this._$removeButton.removeClass('disabled');
    }

    _enableEditButton() {
        this._$editButton.removeClass('disabled');
    }

    async editClickHandler() {
        const items = this._selectedItems;

        if (!items) {
            return;
        }

        if (items.length !== 1) {
            return;
        }

        const currentVal = items[0];
        const item = await this._requestItem(currentVal);
        
        if (!item) {
            return;
        }

        this.editItem(currentVal, item);
        this._selectionChanged();
    }

    async addClickHandler() {
        const item = await this._requestItem();
        
        if (!item) {
            return;
        }

        this.addItem(item);
        this._selectionChanged();
    }

    removeClickHandler() {
        const selected = this._selectedItems;
        
        for (let item of selected) {
            this.removeItem(item);
        }

        this._selectionChanged();
    }

    editItem(oldValue, newValue) {
        const currentState = this._values;
        
        const currentIndex = currentState.indexOf(oldValue);
        currentState[currentIndex] = newValue;
        
        this._values = currentState;
    }

    addItem(val) {
        const currentState = this._values;
        currentState.push(val);
        this._values = currentState;
    }

    removeItem(val) {
        const currentState = this._values;
        const newVals = currentState.filter(item => item !== val);
        this._values = newVals;
    }

    async _requestItem(data) {
        const res = await swal({
            title: 'Что добавить?',
            input: 'text',
            inputValue: data || '',
            inputPlaceholder: 'Что добавить?',
            showCancelButton: true,
            inputValidator: (value) => {
                return !value && 'Нужно что-нибудь ввести!';
            }
        });

        return res.value;
    }

    get _selectedItems() {
        return Array.from(this._$formField.val() || []);
    }

    get _$addButton() {
        return this._$container.find('.add');
    }

    get _$removeButton() {
        return this._$container.find('.remove');
    }

    get _$editButton() {
        return this._$container.find('.edit');
    }

    get _values() {
        const options = this._$container.find('option');
        const values = options.map(i => $(options[i]).val());
        return Array.from(values);
    }

    set _values(vals) {
        this._$formField.empty();

        for (let val of vals) {
            const item = $('<option>');
            item.attr('value', val);
            item.text(val);

            this._$formField.append(item);
        }
    }
}