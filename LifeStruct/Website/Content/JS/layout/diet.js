var app = angular.module('LifeStruct', []);
var api = 'http://' + location.host + '/api/';

var diet = {
    init: function () {
    },
    pageOne: function () {
        app.controller('dietPostCtrl', ['$scope', '$http', function ($scope, $http) {
            $scope.isClicked = false;

            $scope.newDiet = function () {
                $scope.isClicked = true;
                if ($scope.myForm.$valid) {
                    var postDiet = {
                        method: 'POST',
                        url: api + 'DietApi',
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
                currentW = 1,
                days = [],
                meals = [],
                weekDay = [],
                dietId = $('#dietId').val();

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
            $scope.calc = {
                edibleCalories: {

                },
                measureEdible: {

                }
            }
            $scope.dietData = [];
            $scope.allFood = [];
            $scope.weeks = [{ no: 1, text: 'Week ' }];
            $scope.wC = [1];
            $scope.amountS = [];
            $scope.amountSL = [];
            $scope.days = [];
            $scope.meals = [];
            $scope.searches.foodSearch = [];
            $scope.hover = false;
            $scope.autoHide = false;
            $scope.hideResult = function (foodSearch, show) {
                $scope.searches.foodSearch[foodSearch] = show;
            }
            // Search edibles
            $scope.clearFood = function () {
                $scope.allFood = [];
            }
            // Load 

            var getDiet = {
                method: 'GET',
                url: api + 'DietApi/GetDiet?id=' + dietId
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

            $scope.$watch('searches.searchEdibles', function (newVal, oldVal) {


                $scope.setId = function (sId) {
                    $scope.sId = sId;
                }
                if (newVal != oldVal && newVal[$scope.sId] != "" && newVal[$scope.sId].length == 2) {

                    $http({
                        method: 'GET',
                        url: api + 'EdiblesApi/FindByName?s=' + newVal[$scope.sId]
                    }).then(function (data) {

                        $scope.allFood = data.data;
                    });
                }
                var f = false;
                $scope.results = function (i, str) {
                    for (var s = 0; $scope.allFood.length > s; s++) {
                        if ($scope.allFood[s].Name == str) {
                            f = true;
                        }
                    }
                    if (!f) {
                        $('#' + i).removeClass('hide');
                    }
                    else {
                        if (!$('#' + i).hasClass('hide')) {
                            $('#' + i).addClass('hide');

                        }
                    }

                }

            }, 400);
            var foodList = [];
            var foodListed = [];
            $http({ method: 'GET', url: api + 'DaysApi/GetDays' }).then(function (data) {
                weekDay = data.data;

            }).then(function () {
                $http({ method: 'GET', url: api + 'MealsApi/GetMeals' }).then(function (data) {
                    meals = data.data;

                    for (var i = 0; i < weekDay.length; i++) {
                        for (var m = 0; m < meals.length; m++) {
                            $scope.dietData.push({
                                name: '',
                                calories: '',
                                week: currentW,
                                day: i + 1,
                                edible: 1,
                                meal: meals[m].Id,
                                amount: '',
                                foodId: '',
                                edibles: []
                            });
                            $scope.meals.push(meals[m].Name)
                        }
                        $scope.days.push(weekDay[i].Name + ',' + 1)
                    }
                }).then(function () {
                    $http({ method: 'GET', url: api + 'MealCollectionApi/FindByDietId?id=' + dietId }).then(function (data) {
                        var all = data.data,
                            eI = '',
                            ediblesI = '',
                            eNum = 0;


                        for (var i = 0; i < all.length; i++) {
                            eI = $scope.dietData.findIndex(x=> x.day == all[i].Day && x.week == all[i].WeekNo && x.edible == all[i].Edible && x.meal == all[i].Meal);
                            if (eI > -1) {
                                $scope.dietData[eI].amount = all[i].Amount;
                                $scope.dietData[eI].day = all[i].Day;
                                $scope.dietData[eI].week = all[i].WeekNo;
                                $scope.dietData[eI].edible = all[i].Edible;
                                $scope.dietData[eI].meal = all[i].Meal;
                                $scope.dietData[eI].foodId = all[i].FoodId;
                                $http({ method: 'GET', url: api + 'FoodAPi/GetFood?id=' + all[i].FoodId }).then(function (fData) {
                                    $scope.dietData[eI].calories = fData.data.Calories;
                                    $scope.dietData[eI].name = fData.data.Name;
                                });

                            }
                        }
                        var index
                        for (var i = 0; i < all.length; i++)
                        {
                            if (all[i].Edible > 1)
                            {
                                $http({ method: 'GET', url: api + 'FoodAPi/GetFood?id=' + all[i].FoodId }).then(function (fData) {
                                    var aIn = '';
                                    var dIn = '';
                                    for (var i = 0; i < $scope.dietData.length; i++)
                                    {
                                        aIn = all.findIndex(x => x.Day == $scope.dietData[i].day && x.WeekNo == $scope.dietData[i].week && x.Meal == $scope.dietData[i].meal);
                                        console.log($scope.dietData[i].meal)
                                         if (aIn > -1)
                                         {
                                                 $scope.dietData[i].edibles.push({
                                                     amount: all[aIn].Amount,
                                                     day: all[aIn].Day,
                                                     week: all[aIn].WeekNo,
                                                     edible: all[aIn].Edible,
                                                     meal: all[aIn].Meal,
                                                     foodId: all[aIn].foodId,
                                                     calories: fData.data.Calories,
                                                     name: fData.data.Name


                                                 });
                                             console.log($scope.dietData[i])
                                        }
                                    }
                                });

                               
                            }
                        }
                    });
                });
            });
            $scope.autoHide = function (i, b) {

                if (b) {
                    $('#' + i).hide();
                }
                else {
                    $('#' + i).show();

                }
            }
            $scope.hideNxt = function (event, child) {
                $(event.target).next().toggleClass('hide');
            }


            $scope.addWeek = function () {
                currentW++

                $http({ method: 'GET', url: api + 'DaysApi/GetDays' }).then(function (data) {
                    weekDay = data.data;

                    for (var i = 1; i < weekDay.length; i++) {
                        $scope.dietData.push({
                            name: '',
                            calories: '',
                            week: currentW,
                            day: i,
                            edible: '',
                            meal: '',
                            amount: '',
                            foodId: '',
                            edibles: []
                        })
                    }
                });
                $scope.weeks.push({ no: currentW, text: 'Week ' })
            }


            $scope.setCalories = function (calories, Ids) {
                $scope.calc.edibleCalories[Ids] = calories;

            }
            $scope.noCal = function (calories) {
                if (calories != '') {
                    edibleCalories[Ids] = calories;
                }

            }
            $scope.setFoodId = function (Ids, foodId) {
                $('#' + Ids).val(foodId);

            }
            $scope.am = {
                amountS: {
                },
                amountSL: {

                }
            }
            $scope.addEdible = function (i) {
                var ids = i.match(/\d/g);
                console.log(ids)
                var index = $scope.dietData.findIndex(x => x.day == ids[2] && x.week == ids[0] && x.meal == ids[1]);
                if (index > -1) {
                    $scope.dietData[index].edibles.push({
                        name: '',
                        calories: '',
                        week: ids[0],
                        day: ids[2],
                        edible: ids[3],
                        meal: ids[1] + 1,
                        amount: '',
                        foodId: '',

                    });
                }
            }


        }]);

    }


}
diet.init();