var api = 'http://' + location.host + '/api/';
var modal = {
    init: function () {
        console.log(api)
        modal.run('.open-modal', $('#modal'));
    },
    run: function (click, target) {
        $(document).on('click', click, function (e) {
            var sStr = $(this).data('content');
            $.get($(this).data('url'), function (data) {
                    target.html(data);
                    $('#edible_name').val(sStr);
                    $('#exercise_name').val(sStr);
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
    hideModal: function() {
        $('.modal-overlay').hide();
        $('#modal').html();
    },
    addFood: function () {
        var url = api + 'FoodApi/AddFood',
            name = $('#edible_name'),
            edible = $('#edible_calories');
        if (name.val() != '' && edible.val() != '') {
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify({ Name: $('#edible_name').val(), Calories: $('#edible_calories').val() }),
                contentType: "application/json"
            });

            modal.hideModal();
        }
        else {           
            if(name.val() == '')
            {
                name.next().show();
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

        
    },
    addExercise: function () {
        var url = api + 'ExerciseApi/AddExercise',
            name = $('#exercise_name'),
            calories = $('#exercise_calories');
        
        if (name.val() != '' && calories.val() != '') {
            $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify({ Name: name.val(), Calories: calories.val() }),
                contentType: "application/json"
            });
            modal.hideModal();
        }
        else {
            if (name.val() == '') {
                name.next().show();
            }
            else {
                name.next().hide();
            }
            if (calories.val() == '') {
                calories.next().show();
            }
            else {
                calories.next().hide();
            }
        }
    }
}
modal.init();
