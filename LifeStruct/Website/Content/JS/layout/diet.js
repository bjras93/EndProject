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

            console.log($('#dietId').val())
            var getDiet = {
                method: 'GET',
                url: api + 'DietApi/GetDiet?id=' + dietId
            }
            $http(getDiet).then(function (data) {
                $scope.Data = data.data;
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
                    $http({ method: 'GET', url: api + 'MealCollectionApi/GetCollection?dietId=' + dietId }).then(function (data) {
                        var mc = data.data.MealCollection,
                            f = data.data.Food;
                        for (var i = 0; i < mc.length; i++) {
                            var dIndex = $scope.dietData.findIndex(x => x.week == mc[i].WeekNo && x.day == mc[i].Day && x.meal == mc[i].Meal);
                            console.log(mc[i])
                            if (mc[i].Edible == 1) {
                                $scope.dietData[dIndex].id = mc[i].Id;
                                $scope.dietData[dIndex].name = f[i].split(',')[0];
                                $scope.dietData[dIndex].calories = f[i].split(',')[1];
                                $scope.dietData[dIndex].week = mc[i].WeekNo;
                                $scope.dietData[dIndex].day = mc[i].Day;
                                $scope.dietData[dIndex].edible = mc[i].Edible;
                                $scope.dietData[dIndex].meal = mc[i].Meal;
                                $scope.dietData[dIndex].amount = mc[i].Amount;
                                $scope.dietData[dIndex].foodId = mc[i].FoodId;
                            }
                            else {
                                $scope.dietData[dIndex].edibles.push({
                                    id: mc[i].Id,
                                    name: f[i].split(',')[0],
                                    calories: f[i].split(',')[1],
                                    week: mc[i].WeekNo,
                                    day: mc[i].Day,
                                    edible: mc[i].Edible,
                                    meal: mc[i].Meal,
                                    amount: mc[i].Amount,
                                    foodId: mc[i].FoodId,
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
            $scope.noCal = function (calories, Ids) {
                if (calories != '') {
                    $scope.calc.edibleCalories[Ids] = calories;
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
            $scope.addEdible = function (i, m) {
                var ids = i.match(/\d/g);
                console.log(ids)
                var index = $scope.dietData.findIndex(x => x.day == ids[2] && x.week == ids[0] && x.meal == ids[1]);
                if (index > -1) {
                    $scope.dietData[index].edibles.push({
                        name: '',
                        calories: '',
                        week: ids[0],
                        day: ids[2],
                        edible: $scope.dietData[index].edibles.length +1,
                        meal: ids[1],
                        amount: '',
                        foodId: '',

                    });
                    console.log($scope.dietData[index].edibles)
                }
            }


        }]);

    }


}
diet.init();