$(($) => {
    const selectControl = $('.paging').find('select');

    selectControl.change(function () {
        const url = location.origin + location.pathname;
        const search = location.search.replace('?', '').split('&').map((item) => {
            const splited = item.split('=');

            if (!splited[0] || !splited[1])
                return null;

            const obj = {};

            obj[splited[0]] = splited[1];

            return obj;
        }).filter((item) => {
            return item !== null;
        });

        const found = search.filter(function (item) {
            return item.hasOwnProperty('perPage');
        });

        if (found.length > 0) {
            found.every((item) => {
                item['perPage'] = this.value;
            });
        } else {
            search.push({ perPage: this.value });
        }


        const strSearch = search.map((item) => {
            return Object.keys(item).map((key) => {
                return key + '=' + item[key];
            });
        }).reduce((prev, curr) => {
            return prev + '&' + curr;
        });

        location.href = url + '?' + strSearch;
    });
});