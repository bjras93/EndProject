var modal = {
    init: function () {
        
        modal.run('.open-modal', $('#modal'));
    },
    run: function (click, target) {
        $(document).on('click', click, function (e) {
            var sStr = $(this).data('url').split('_')[1];
            $.get($(this).data('url').split('_')[0], function (data) {
                    target.html(data);
                    $('#edible_name').val(sStr);
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
        
    },
    addFood: function () {
        var hideModal = function () {

            $('.modal-overlay').hide();
            $('#modal').html();
        }
        var url = 'http://dev.lifestruct.dk/api/FoodApi/AddFood',
            name = $('#edible_name'),
            edible = $('#edible_calories');
        if (name.val() != '' && edible.val() != '') {
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify({ Name: $('#edible_name').val(), Calories: $('#edible_calories').val() }),
                contentType: "application/json"
            });
            hideModal();
        }
        else {           
            if(name.val() == '')
            {
                name.next().show();
                console.log(name);
            }
            else {
                name.next().hide();
            }
            if(edible.val() == '')
            {
                edible.next().show();
            }
            else {
                edible.next().hide();
            }
        }

        
    }
}
modal.init();
