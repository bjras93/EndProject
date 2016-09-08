var modal = {
    init: function () {
        modal.run($('#open-modal'), $('#modal'));
    },
    run: function (click, target) {
            click.on('click', function(e){
                $.get($(this).data('url'), function(data) {
                    target.html(data);

                });
                e.stopPropagation();
                var loc = window.scrollY;
                $('body').addClass('stop-scroll')
                $('.modal-overlay').show();
                $('#modal').show();
                $('.modal-overlay').css('top', loc)
                $('body').on('click', function () {
                    $('#modal').hide();
                    $('.modal-overlay').hide();
                    $('body').removeClass('stop-scroll')

                });
                $('#modal').on('click', function (e) {
                    e.stopPropagation();
                });
            });
        
    }
}
modal.init();