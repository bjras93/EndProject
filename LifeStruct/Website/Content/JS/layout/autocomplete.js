var clicks = 0,
    nUntil = '',
    target = '',
    items = '',
    allTargets = [];

autocomplete = {
    init: function () {
        $('body').on('click', function (e) {
            var target = $(e.target);
            if (target.hasClass('autocomplete-input')) {
                nUntil = $(e.target).parent().find('.autocomplete-container');
                $('.autocomplete-container').hide();
                $(nUntil).show();
            } else {
                nUntil = $(e.target).parent().find('.autocomplete-container');
                $(nUntil).hide();
            }
            if (target.hasClass('autocomplete-items')) {
                $('.autocomplete-container').hide();
            }
            clicks = 0;
        });

    },
    keyDown: function (event) {
        if ($(event.target).hasClass('autocomplete-input')) {
            clicks = 0;
            target = $(event.target);
            items = target.parent().find('.autocomplete-items');
            allTargets = [];
            allTargets.push(target);
            for (var i = 0; i < items.length; i++) {
                allTargets.push($(items[i]));
            }
        }
        if (event.keyCode === 40 && clicks !== items.length) {
            event.preventDefault();
            $(allTargets[clicks]).removeClass('autocomplete-selected');
            clicks++;
            $(allTargets[clicks]).focus();
            $(allTargets[clicks]).addClass('autocomplete-selected');
        }
        if (event.keyCode === 38 && clicks !== 0) {
            event.preventDefault();
            $(allTargets[clicks]).removeClass('autocomplete-selected');
            clicks = clicks - 1;
            $(allTargets[clicks]).focus();
            $(allTargets[clicks]).addClass('autocomplete-selected');
        }
    },
    resize: function (input, main) {
        $(main).css('width',$(input).outerWidth());
    }
}