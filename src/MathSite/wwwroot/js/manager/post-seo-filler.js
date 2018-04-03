class PostSeoFiller {
    /**
     * Creates PostSeoFiller class.
     * @param {jQuery} $container
     * @param {boolean} validateEvents
     */
    constructor($container, validateEvents) {
        this.$container = $container;
        this.validateEvents = Boolean(validateEvents);
    }

    fillData() {
        var data = {};

        this.$container.serializeArray()
            .forEach(item => data[item.name] = item.value);

        this._fillUrl(data);
        this._fillTitle(data);
        this._fillDescription(data);
    }

    _fillUrl(data) {
        if (data.Url || !data.Title) {
            return;
        }

        this.$container.find('#Url').val(this._transliterate(data.Title));
    }

    _fillTitle(data) {
        if (!data.Title || data.SeoTitle) {
            return;
        }

        this.$container.find('#SeoTitle').val(data.Title);
    }

    _fillDescription(data) {
        if (!data.Excerpt || data.SeoDescription) {
            return;
        }

        this.$container.find('#SeoDescription').val(this._removeHtml(data.Excerpt));
    }

    _removeHtml(source) {
        const span = document.createElement('span');
        span.innerHTML = source;
        return span.innerText.replace('\r\n', ' ').replace('\r', ' ').replace('\n', ' ');
    }

    _transliterate(text) {
        return text.split('')
            .map((char) => {
                switch (char) {
                    case 'А':
                    case 'а':
                        return 'a';
                    case 'Б':
                    case 'б':
                        return 'b';
                    case 'В':
                    case 'в':
                        return 'v';
                    case 'Г':
                    case 'г':
                        return 'g';
                    case 'Д':
                    case 'д':
                        return 'd';
                    case 'Е':
                    case 'е':
                        return 'e';
                    case 'Ё':
                    case 'ё':
                        return 'yo';
                    case 'Ж':
                    case 'ж':
                        return 'zh';
                    case 'З':
                    case 'з':
                        return 'z';
                    case 'И':
                    case 'и':
                        return 'i';
                    case 'Й':
                    case 'й':
                        return 'j';
                    case 'К':
                    case 'к':
                        return 'k';
                    case 'Л':
                    case 'л':
                        return 'l';
                    case 'М':
                    case 'м':
                        return 'm';
                    case 'Н':
                    case 'н':
                        return 'n';
                    case 'О':
                    case 'о':
                        return 'o';
                    case 'П':
                    case 'п':
                        return 'p';
                    case 'Р':
                    case 'р':
                        return 'r';
                    case 'С':
                    case 'с':
                        return 's';
                    case 'Т':
                    case 'т':
                        return 't';
                    case 'У':
                    case 'у':
                        return 'u';
                    case 'Ф':
                    case 'ф':
                        return 'f';
                    case 'Х':
                    case 'х':
                        return 'h';
                    case 'Ц':
                    case 'ц':
                        return 'tc';
                    case 'Ч':
                    case 'ч':
                        return 'ch';
                    case 'Ш':
                    case 'ш':
                        return 'sh';
                    case 'Щ':
                    case 'щ':
                        return 'shch';
                    case 'Ъ':
                    case 'ъ':
                        return '';
                    case 'Ы':
                    case 'ы':
                        return 'yi';
                    case 'Ь':
                    case 'ь':
                        return '';
                    case 'Э':
                    case 'э':
                        return 'e';
                    case 'Ю':
                    case 'ю':
                        return 'yu';
                    case 'Я':
                    case 'я':
                        return 'ya';
                    case ' ':
                    case '/':
                    case '|':
                    case '\\':
                    case ',':
                    case '.':
                    case ':':
                    case '&':
                    case '?':
                    case '$':
                    case '#':
                    case '@':
                    case '!':
                    case '+':
                    case '*':
                    case ';':
                        return '-';
                    default:
                        return char;
                }
            })
            .join('');
    }
}