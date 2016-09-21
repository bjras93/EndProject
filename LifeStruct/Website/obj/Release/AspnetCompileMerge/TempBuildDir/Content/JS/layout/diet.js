var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
var api = 'http://' + location.host + '/api/';

var diet = {
    init: function () {
    },
    pageOne: function () {
        app.controller('dietPostCtrl', ['$scope', '$http', function ($scope, $http) {
            $scope.isClicked = false;
            $scope.tags = [];

        }]);
    },
    pageTwo: function () {
        app.controller('dietGetCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
            var loc = location.href,
                id = loc.split('?id=')[1],
                currentW = 1,
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
            $scope.setValue = {
                calories: {

                }
            }
            $scope.calc = {
                edibleCalories: {

                },
                measureEdible: {

                },
                calories: {

                },
                days: {

                }
            }
            $scope.dietData = [];
            $scope.allFood = [];
            $scope.weeks = [{ no: 1, text: 'Week ' }];
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

            $('.week-container').css('margin-top', $('#title').outerHeight());
            // Load 
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
                            if (mc[i].Edible == 1) {
                                $scope.dietData[dIndex].id = mc[i].Id;
                                $scope.dietData[dIndex].name = f[i].split('|')[0];
                                $scope.dietData[dIndex].calories = f[i].split('|')[1];
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
                                    name: f[i].split('|')[0],
                                    calories: f[i].split('|')[1],
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

                    for (var i = 1; i < weekDay.length ; i++) {
                        for (var m = 1; m < 5; m++) {
                            $scope.dietData.push({
                                name: '',
                                calories: '',
                                week: currentW,
                                day: i,
                                edible: 1,
                                meal: m,
                                amount: '',
                                foodId: '',
                                edibles: []
                            })
                        }
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
                var index = $scope.dietData.findIndex(x => x.day == ids[2] && x.week == ids[0] && x.meal == ids[1]);
                if (index > -1) {
                    $scope.dietData[index].edibles.push({
                        name: '',
                        calories: '',
                        week: ids[0],
                        day: ids[2],
                        edible: $scope.dietData[index].edibles.length + 2,
                        meal: ids[1],
                        amount: '',
                        foodId: '',

                    });
                }
            }
            var prevIndex = -1;
            var prevCal = 0;
            var prevIds = '';
            var days = 7;
            $scope.obj = [];
            $scope.$watch('dietData', function (oldValue, newValue) {

                for (var i = 0; i < $scope.dietData.length; i++) {
                    var Ids = 'w' + $scope.dietData[i].week + '_m' + $scope.dietData[i].meal + '_d' + $scope.dietData[i].day + '_e' + $scope.dietData[i].edible;
                    var idIndex = $scope.obj.findIndex(x=> x.id == Ids);
                    if ($scope.dietData[i].calories != undefined && $scope.dietData[i].calories != null && $scope.dietData[i].calories != '' && idIndex == -1) {
                        $scope.obj.push({ cal: $scope.dietData[i].calories, amount: $scope.dietData[i].amount, index: i, id: Ids, prevCal: ($scope.dietData[i].calories / 100) * $scope.dietData[i].amount });
                    }
                }
            }, true);
            $scope.$watch('obj', function (oldVal, newVal) {
                var total = 0;
                var week = 0;
                var meal = 0;
                var day = 0;
                var edible = 0;
                for (var i = 0; i < $scope.obj.length; i++) {
                    var d = $scope.obj[i];
                    var split = $scope.obj[i].id.split('_');
                    week = split[0].replace('w', '');
                    meal = split[1].replace('m', '');
                    day = split[2].replace('d', '');
                    edible = split[3].replace('e', '');

                }
                for (var w = 0; w < $scope.weeks.length; w++) {
                    $scope.calc.days[w + '_' + 0] = 0;
                    $scope.calc.days[w + '_' + 1] = 0;
                    $scope.calc.days[w + '_' + 2] = 0;
                    $scope.calc.days[w + '_' + 3] = 0;
                    $scope.calc.days[w + '_' + 4] = 0;
                    $scope.calc.days[w + '_' + 5] = 0;
                    $scope.calc.days[w + '_' + 6] = 0;

                    for (var i = 0; i < 7; i++) {
                        $scope.calc.calories[1 + '_' + w + '_' + i] = 0;
                        $scope.calc.calories[2 + '_' + w + '_' + +i] = 0;
                        $scope.calc.calories[3 + '_' + w + '_' + +i] = 0;
                        $scope.calc.calories[4 + '_' + w + '_' + +i] = 0;
                    }
                }
                for (var i = 0; i < $scope.dietData.length; i++) {
                    var s = $scope.dietData[i];
                    var indexObj = $scope.obj.findIndex(x => x.id == 'w' + s.week + '_m' + s.meal + '_d' + s.day + '_e' + s.edible)
                    if (indexObj > -1) {
                        $scope.calc.days[(s.week - 1) + '_' + (s.day - 1)] += ($scope.obj[indexObj].cal / 100) * $scope.obj[indexObj].amount;
                    }
                    for (var e = 0; e < $scope.dietData[i].edibles.length; e++) {
                        if ($scope.obj[indexObj] != "") {
                            var edi = s.edibles[e];
                            var indexObj = $scope.obj.findIndex(x => x.id == 'w' + edi.week + '_m' + edi.meal + '_d' + edi.day + '_e' + edi.edible)
                            if (indexObj > -1) {
                                $scope.calc.days[(edi.week - 1) + '_' + (edi.day - 1)] += ($scope.obj[indexObj].cal / 100) * $scope.obj[indexObj].amount;
                            }
                        }
                    }
                    for (var m = 0; m < 4; m++) {
                        if (s.meal == (m + 1)) {
                            var iObj = $scope.obj.findIndex(x => x.id == 'w' + s.week + '_m' + (m + 1) + '_d' + s.day + '_e' + s.edible)
                            if (iObj > -1) {
                                $scope.calc.calories[(m + 1) + '_' + (s.week - 1) + '_' + (s.day - 1)] += ($scope.obj[iObj].cal / 100) * $scope.obj[iObj].amount;
                            }

                            for (var e = 0; e < $scope.dietData[i].edibles.length; e++) {
                                var edi = s.edibles[e];
                                var indexObj = $scope.obj.findIndex(x => x.id == 'w' + edi.week + '_m' + edi.meal + '_d' + edi.day + '_e' + edi.edible);
                                if (indexObj > -1) {
                                    $scope.calc.calories[(m + 1) + '_' + (s.week - 1) + '_' + (s.day - 1)] += ($scope.obj[indexObj].cal / 100) * $scope.obj[indexObj].amount;
                                }
                            }
                        }
                    }
                }
            }, true);
            $scope.calCalc = function (calories, amount, index, Ids) {
                var idIndex = $scope.obj.findIndex(x=> x.id == Ids);
                if (isNaN($scope.calc.calories[index])) {
                    $scope.calc.calories[index] = 0;
                }
                if (amount != undefined && calories != undefined) {
                    if (idIndex == -1) {
                        $scope.obj.push({ cal: calories, amount: amount, index: index, id: Ids, prevCal: 0 })
                    }
                    else {
                        $scope.obj[idIndex].cal = calories;
                        $scope.obj[idIndex].amount = amount;
                        var calAdd = ($scope.obj[idIndex].cal / 100) * $scope.obj[idIndex].amount;
                        $scope.obj[idIndex].prevCal = calAdd;

                    }
                }
                else {

                    if (idIndex > -1) {
                        $scope.obj[idIndex].cal = 0;
                        $scope.obj[idIndex].amount = 0;
                    }
                    if (amount == null && calories != undefined && idIndex != -1) {
                        $scope.obj[idIndex].prevCal = 0;
                    }
                }
            }
        }]);

    },
    indexPage: function () {

        app.controller('dietIndexCtrl', ['$scope', '$http', function ($scope, $http) {
            $scope.liked = '';
            $scope.dietData = [];
            $scope.user = '';
            userIds = [];
            $scope.l = {
                liked: {

                }
            }
            $http({ method: 'GET', url: api + 'DietApi/' }).then(function (data) {
                $scope.dietData = data.data;
            });
            $scope.users = function (uId, index) {
                if (uId != '') {
                    $http({ method: 'GET', url: api + 'UserApi/GetById?Id=' + uId }).then(function (data) {
                        $scope.user = data.data.Name;
                    });
                }
            }
            $scope.like = function (id) {
                $http({ method: 'POST', url: api + 'LikeApi/Like', data: JSON.stringify({ type: 1, typeId: id }), contentType: "application/json" }).then(function (data) {
                    $scope.l.liked[id] = data.data.UserId;
                });
            };
            $scope.getLikes = function (uId, dietId) {
                $http({ method: 'GET', url: api + 'LikeApi/FindByUIdType?uId=' + uId + '&type=' + 1 }).then(function (data) {
                    for (var i = 0; i < data.data.length; i++) {
                        if (dietId == data.data[i].TypeId) {
                            console.log(data.data[i])
                            $scope.l.liked[dietId] = data.data[i].UserId;
                        }
                    }
                })
            }
            $scope.selectDiet = function (dietId, uId, selected) {
               
               
                $http({ method: 'POST', url: api + 'UserApi/SetDiet', data: JSON.stringify({ uId: uId, dId: dietId, type: 1, add: selected == dietId }), contentType: "application/json" }).then(function (data) {
                    $scope.selected = data.data.DietId;
                });
                $scope.selected = '';
            }
        }]);
    }
}
diet.init();