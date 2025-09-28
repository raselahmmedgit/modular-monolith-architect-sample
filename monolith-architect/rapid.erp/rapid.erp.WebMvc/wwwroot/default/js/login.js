!(function ($) {
    // App Preloader
    $(window).on('load', function () {
        if ($('#appPreLoader').length) {
            $('#appPreLoader').delay(100).fadeOut('slow', function () {
                //$(this).remove();
            });
        }
    });
})(jQuery);