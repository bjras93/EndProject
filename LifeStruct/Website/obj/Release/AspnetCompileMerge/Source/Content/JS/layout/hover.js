var hover = {
    init: function () {
        var $mouseX = 0, $mouseY = 0;
        $(document).mousemove(function (e) {
            $('.hover').css({
                left: e.pageX,
                top: e.pageY
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