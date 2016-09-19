var readmore = {
    init: function () {
        var d = $('.description');
        var r = $('.read');
        var max = 300;
        $(document).on('click', '.read', function () {
            var content = $(this).prev().text();
            var c = content.substr(0, max);
            console.log(c)
        });
    }
}