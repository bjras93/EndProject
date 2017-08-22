var overlay = {
    init: function (color) {
        var bh = $('body').height(),
            o = $('.overlay');
        o.height(bh);
        o.addClass(color);
        $(window).resize(function () {
            var bh = $('body').height(),
                o = $('.overlay');
            o.height(bh);
        });
    }
}
