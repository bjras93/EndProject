var ac = $('.autocomplete');
var autocomplete = {
    init: function () {
        $('body').on('click', function (e) {
            ac.hide();
        });
    },
    keyDown: function () {
        var anchors = "";
        ac.children().each(function () {
            console.log(this);
        });
    }
}