function upload(change, target) {
    $(change).change(function (e) {

        for (var i = 0; i < e.originalEvent.srcElement.files.length; i++) {

            var file = e.originalEvent.srcElement.files[i];

            var reader = new FileReader();
            reader.onloadend = function () {
                $(target).attr('src', reader.result);
                $(target).css('width', '100%').css('height', '300px')
            }
            reader.readAsDataURL(file);

        }
    });
}