$(function () {
    var selectControl = $('.paging').find('select');

    selectControl.change(function () {
        var url = location.origin + location.pathname;
        var search = location.search.replace('?', '').split('&').map(function (item) {
            var splited = item.split('=');
            var obj = {};

            obj[splited[0]] = splited[1];

            return obj;
        });

        var found = search.filter(function (item) {
            return item.hasOwnProperty('perPage');
        });

        if (found.length > 0) {
            found.every(function (item) {
                item['perPage'] = this.value;
            }.bind(this));
        } else {
            search.push({ perPage: this.value });
        }


        var strSearch = search.map(function (item) {
            return Object.keys(item).map(function (key) {
                return key + '=' + item[key];
            });
        }).reduce(function (prev, curr) {
            return prev + '&' + curr;
        });

        location.href = url + '?' + strSearch;
    });
});