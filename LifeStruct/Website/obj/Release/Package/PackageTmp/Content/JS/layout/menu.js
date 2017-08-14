var menu = {
    init: function () {
        menu.goal();
        menu.account();
    },
    goal: function () {        
        $('#goal').on('click', function () {
            $('.goal').toggle("slide", { direction: "right" }, 1000);
        });
        $('.goal-index').on('click', function () {
            setTimeout(function () {
                $('.goal').toggle("slide", { direction: "right" }, 1500);
            }, 300);
        });
    },
    account: function () {
        var prev = 'account-details',
            loc = location.href.split('?m=')[1];
        if (loc == null) {
            $('.' + prev).show();
        }
        $('.account-menu ul li a').each(function () {
            var $this = $(this),
                select = $('.' + $this.data('section'));
                if (loc != null)
                {
                    select.hide();
                    $('.account-' + loc).show();
                }

            $this.on('click', function (e) {
                $('.' + prev).hide();
                select.show();
                prev = $this.data('section');
                location.href = '?m=' + prev.replace('account-', '');
                e.preventDefault();

            });
        });
    }
}