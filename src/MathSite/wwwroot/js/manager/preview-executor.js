class PreviewExecutor {
    constructor (url, hasAdditionalFields) {
        this.url = url;
        this.window = null;
        this.hasAdditionalFields = hasAdditionalFields || false;
    }

    openPreview() {
        const data = this._buidData();
        
        const fakeForm = $("<form target='_blank' method='POST' style='display:none;'></form>").attr({
            action: this.url
        }).appendTo(document.body);

        for (let i in data) {
            if (data.hasOwnProperty(i)) {
                $('<input type="hidden" />').attr({
                    name: i,
                    value: data[i]
                }).appendTo(fakeForm);
            }
        }

        fakeForm.submit();

        fakeForm.remove();
    }

    _buidData() {
        const defaultCfg = {
            Title: this._getTitle(),
            PreviewImageId: this._getPreviewImageId(),
            Content: this._getContent()
        };

        const extendedCfg = {
            ...defaultCfg,
            Date: this._getDate(),
            Time: this._getTime(),
            Location: this._getLocation()
        };

        return this.hasAdditionalFields
            ? defaultCfg
            : extendedCfg;
    }

    _getTitle() {
        return $('#Title').val();
    }

    _getContent() {
        return $('#Content').val();
    }

    _getPreviewImageId() {
        return $('#PreviewImageId').val();
    }

    _getDate() {
        var dateTime = new Date($('#EventTime').val());
        return `${dateTime.getDate()}.${Math.floor(dateTime.getMonth() / 10) !== 1 ? '0' + dateTime.getMonth() : dateTime.getMonth() }.${dateTime.getFullYear()}`;
    }

    _getTime() {
        var dateTime = new Date($('#EventTime').val());
        return `${dateTime.getHours()}:${dateTime.getMinutes()}`;
    }

    _getLocation() {
        return $('#EventLocation').val();
    }
}