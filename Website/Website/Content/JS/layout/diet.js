var app = angular.module('yougo', []);

var diet = {
    init: function () {
        diet.pageOne();
        diet.pageTwo();
    },
    pageOne: function () {
        app.controller('dietPostCtrl', ['$scope', '$http', function ($scope, $http) {
            $scope.isClicked = false;

            $scope.newDiet = function () {
                $scope.isClicked = true;
                if ($scope.myForm.$valid) {
                    var postDiet = {
                        method: 'POST',
                        url: 'http://local.yougo.dk/api/DietApi',
                        data: JSON.stringify({ title: $scope.Title, description: $scope.Description, img: $scope.Img })
                    }
                    $http(postDiet).then(function (data) {
                        var id = data.data;
                        if (id != null) {
                            location.href = '?id=' + id + '&p=1';

                        }

                    });
                }
            }
        }]);
    },
    pageTwo: function () {
        app.controller('dietGetCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
            var loc = location.href,
                id = loc.split('?id=')[1],
                api = 'http://local.yougo.dk/api/',
                currentW = 1,
                days = [],
                meals = [],
                weekDay = [];

            $scope.searches = {
                searchEdibles: {

                },
                foodSearch: {

                }
            }
            $scope.result = {
                autoComplete: {

                },
                selected: {

                }
            }

            $scope.allFood = [];
            $scope.week = [];
            $scope.dC = ["Monday,1"];
            $scope.wC = [1];
            $scope.amountS = [];
            $scope.amountSL = [];
            $scope.weekDays = [];
            $scope.searches.foodSearch = [];
            $scope.hover = false;
            $scope.autoHide = false;
            $scope.hideResult = function (foodSearch, show) {
                $scope.searches.foodSearch[foodSearch] = show;
            }
            
            // Load 
            if (id != undefined) {
                var p = id.split('&p=1')[0];
                var getDiet = {
                    method: 'GET',
                    url: api + 'DietApi/GetDiet?id=' + p
                }
                $http(getDiet).then(function (data) {
                    if (loc.split('&p=')[1] == 1) {
                        $('.create-diet').children().each(function (i) {
                            if (i != 1) {
                                $(this).hide();
                            }
                            else {
                                $(this).show();
                            }
                        });
                        $scope.Data = data.data;
                    }
                });
                
                // Search edibles
                $scope.clearFood = function () {
                    $scope.allFood = [];
                }
                $('body').on('click', function (e) {
                    
                    $('.autocomplete').hide();

                });
                $scope.$watch('searches.searchEdibles', function (newVal, oldVal) {
                                       
                    
                    $('.autocomplete').on('mouseenter', function () {
                        $(this).children().each(function () {
                            $(this).children().each(function () {
                                $(this).removeClass('autocomplete-selected')
                            })
                        });
                    });
                    if ($('.autocomplete').width() < $('#selected0').outerWidth()) {
                        $('.autocomplete').css('width', $('#selected0').outerWidth())
                    }
                    $scope.setId = function (sId) {
                        $scope.sId = sId;
                    }
                    if (newVal != oldVal && newVal[$scope.sId] != "") {
                        $http({
                            method: 'GET',
                            url: api + 'EdiblesApi/FindByName?s=' + newVal[$scope.sId]
                        }).then(function (data) {
                            $scope.results = function (i) {
                                if (data.data.length == 0) {
                                    $('#' + i).removeClass('hide');
                                }
                                else {
                                    if (!$('#' + i).hasClass('hide')) {
                                        $('#' + i).addClass('hide');

                                    }
                                }
                            }
                            $scope.allFood = data.data;
                        });
                    }
                    else {
                        $scope.allFood = [];
                    }

                }, 400);

                $http({ method: 'GET', url: api + 'DaysApi/GetDays' }).then(function (data) {
                    weekDay = data.data;

                    for (var i = 0; i < weekDay.length; i++) {
                        days.push({
                            id: weekDay[i].Id,
                            day: weekDay[i].Name,
                            edibles: '',
                            calories: '',
                            meals: []
                        });
                        
                    }
                    $http({ method: 'GET', url: api + 'MealsApi/GetMeals' }).then(function (data) {
                        meals = data.data;
                        for (var i = 0; i < days.length; i++) {
                            for (var m = 0; m < meals.length; m++) {
                                $scope.amountS.push(m + ',' + i);
                                days[i].meals.push({
                                    name: meals[m].Name,
                                    no: m,
                                    edibles: []
                                });
                            }
                        }
                    });
                });
                // Add
                var clicked = 0;
                $scope.selectResult = function (e, list, sId) {
                    if (list.length != 0 && list.length >= clicked) {
                        if (e.keyCode == '13') {


                            $scope.result.autoComplete[sId + (clicked - 1)] = false;

                            clicked = 0;

                        }
                        if (e.keyCode == '38' && clicked != 0) {
                            e.preventDefault();
                            // up arrow

                            clicked = clicked - 1;
                            $(sId + clicked).focus();
                            $scope.result.selected[sId + clicked] = true
                            $scope.result.autoComplete[sId + (clicked - 1)] = true;
                            $scope.result.autoComplete[sId + clicked] = false;


                        }
                    }

                    if (list.length != 0 && list.length > clicked) {
                        if (e.keyCode == '40') {
                            e.preventDefault();
                            // down arrow
                            $scope.result.autoComplete[sId + (clicked - 1)] = false;
                            $scope.result.autoComplete[sId + clicked] = true;
                            $scope.result.selected[sId + clicked] = true;
                            if (clicked > 0) {
                                $scope.result.autoComplete[sId + clicked - 1] = false;
                            }

                            clicked++;
                            $('#' + sId + clicked).focus();
                        }

                    }
                    else {
                        if (e.keyCode == '38' && clicked != 0) {
                            e.preventDefault();
                            // up arrow

                            clicked = clicked - 1;
                            $('#selected' + clicked).focus();
                            $scope.result.autoComplete[sId+ (clicked + 1)] = false;


                        }
                        if(e.keyCode == '40' && clicked < 1)
                        {
                            clicked++;
                            e.preventDefault();
                            $scope.result.autoComplete[sId + clicked] = true;
                            $('#selected_' + clicked).focus();
                            
                        }
                    }


                }


                $scope.autoHide = function (i, b) {
                    if (b) {
                        $('#' + i).hide();
                    }
                    else {
                        $('#' + i).show();

                    }
                }
                $scope.weekClick = function (d) {
                    hide(d, $scope.wC, this);
                }
                $scope.dayClick = function (d) {
                    hide(d, $scope.dC, this);
                }
                var hide = function (d, scope, click) {
                    var i = scope.indexOf(d);
                    if (i > -1) {
                        scope.splice(i, 1);
                    }
                    else {
                        scope.push(d);
                    }
                };

                $scope.week.push({
                    no: currentW,
                    days: days
                });

                $scope.addWeek = function () {
                    currentW++

                    $scope.week.push({
                        no: currentW,
                        days: days
                    });
                }


                $scope.setCalories = function (calories, Ids) {
                    $scope.am.amountS[Ids] = calories;

                }
                $scope.setFoodId = function (foodId, Ids) {
                    $scope.foodName = Ids;
                    $scope.foodId = foodId;
                }
                $scope.am = {
                    amountS: {
                    },
                    amountSL: {

                    }
                }
                $scope.addEdible = function (day, week, meal) {
                    $scope.week[week - 1].days[day.id - 1].meals[meal].edibles.push({
                        name: '',
                        calories: ''
                    });
                }

            }

        }]);

    }


}
diet.init();