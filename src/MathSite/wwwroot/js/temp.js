(function($) {

    /* toggle menu */

    $('.header__burger').on('click', function () {
        $('.header').toggleClass('header_expanded');
    });

    /* hide menu by click outside */

    $(document).on('click', function(event) {
        if (!$(event.target).closest('.header').length) {
            $('.header').removeClass('header_expanded');
        }
    });

    /* hide popup by Esc press */

    $(document).on('keyup', function(event) {
        if (event.keyCode === 27) {
            $('.header').removeClass('header_expanded');
        }
    });



    /* search */

    $('.search__handler').on('click', function () {
        $(this).parents('.search').addClass('search_active');
        $(this).parents('.search').find('.search__field').focus();
    });

    /* hide search by click outside */

    $(document).on('click', function(event) {
        if (!$(event.target).closest('.search').length) {
            $('.search_active').removeClass('search_active');
        }
    });

    /* hide search by Esc press */

    $(document).on('keyup', function(event) {
        if (event.keyCode === 27) {
            $('.search_active').removeClass('search_active');
        }
    });

})(jQuery);
