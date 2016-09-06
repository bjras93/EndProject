var menu = {
    init: function () {
        $('#mood').on('click', function () {
            $('.mood').toggle("slide", { direction: "right" }, 1000);
        });       
    }
}