var accordion = {
    init: function () {
        $(document).on('click', '.accordion', function () {
            var t = $(this);
            t.next().toggle();
            autocomplete.resize('.autocomplete-input', '.autocomplete')
        });
    }
}
accordion.init();