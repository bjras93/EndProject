var clicks = 0
fEvent = '',
 ac = '';
anchors = [],
autocomplete = {
    init: function () {
        $('body').on('click', function (e) {
            if (ac == '') {
                ac = e.target;
            }
            else {
                $(ac).next().find('.autocomplete').hide()
                if (clicks != 0) {
                    $(anchors[clicks - 1].anchor).removeClass('autocomplete-selected');
                }
                ac = '';
                clicks = 0;
            }
            if ($(e.target).next().hasClass('autocomplete-container')) {
                $(ac).next().find('.autocomplete').show()
            }
        });
    },

}
var keyDown = function (event) {
    anchors = []
    if (fEvent == '' && fEvent != event.target) {
        fEvent = event.target;
    }
    $(fEvent).next().find('a').each(function () {
        anchors.push({ anchor: this })
    });
    if (event.keyCode == 13) {
        $(anchors[clicks-1].anchor).removeClass('autocomplete-selected');
        clicks = 0;
    }
    if (event.keyCode == 38) {
        clicks = clicks - 1;
        event.preventDefault();
        if (clicks != 0) {
            $(anchors[clicks - 1].anchor).addClass('autocomplete-selected');
            $(anchors[clicks - 1].anchor).focus()
        }
        else {
            $(anchors[clicks].anchor).removeClass('autocomplete-selected');
            $(fEvent).focus();
            fEvent = '';
        }
        if (clicks >= 1) {
            $(anchors[clicks].anchor).removeClass('autocomplete-selected');
        }
    }
    if (event.keyCode == 40 && clicks != anchors.length) {
        event.preventDefault();
        $(anchors[clicks].anchor).focus()
        if (clicks >= 1) {
            $(anchors[clicks - 1].anchor).removeClass('autocomplete-selected');
        }
        $(anchors[clicks].anchor).addClass('autocomplete-selected');
        clicks++;
    }
};