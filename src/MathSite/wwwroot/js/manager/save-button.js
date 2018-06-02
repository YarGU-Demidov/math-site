class SaveButton {
    constructor(cfg) {
        cfg.clickHandler = cfg.clickHandler || (() => {});
        cfg.selector = cfg.selector || '.save-image';

        this.$container = $(cfg.selector);
        this._enabled = true;
        this.$container.click(cfg.clickHandler.bind(this));

        this.disable();
    }

    set enabled(value) {
        value ? this.enable() : this.disable();
    }

    get enabled() {
        return this._enabled;
    }

    disable() {
        this._enabled = false;
        this.$container.addClass('disabled');
    }

    enable() {
        this._enabled = true;
        this.$container.removeClass('disabled');
    }
}