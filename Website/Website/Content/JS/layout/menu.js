var menu = {
    init: function () {
        menu.mood();
        menu.account();
    },
    mood: function () {        
        $('#mood').on('click', function () {
            $('.mood').toggle("slide", { direction: "right" }, 1000);
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