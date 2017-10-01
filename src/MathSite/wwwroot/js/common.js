$(function() {
    $('img, a').on(
        'dragstart',
        function(event) {
            event.preventDefault();
        }
    );
});