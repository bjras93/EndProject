var hover = {
    init: function () {
        var $mouseX = 0, $mouseY = 0;
        $(document).mousemove(function (e) {
            $('.hover').css({
                left: e.clientX,
                top: e.clientY
            });
        });
        $('.tile').on('mouseover', function () {
            $(this).find('.hover').show();
        });
        $('.tile').on('mouseleave', function () {
            $(this).find('.hover').hide();
        });
    }
};
hover.init();