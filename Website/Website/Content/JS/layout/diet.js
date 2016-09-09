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
        app.controller('dietGetCtrl', ['$scope', '$http', function ($scope, $http) {
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
            $scope.allFood = [];            
            $scope.week = [];
            $scope.dC = ["Monday,1"];
            $scope.wC = [1];
            $scope.amountS = [];
            $scope.amountSL = [];
            $scope.weekDays = [];
            $scope.searches.foodSearch = [];
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
                $scope.clearFood = function() {
                    $scope.allFood = "";
                }
                $scope.$watch('searches.searchEdibles', function (newVal, oldVal) 
                {

                    $scope.setId = function (sId) {
                        $scope.sId = sId;
                    }
                    console.log($scope.sId)
                    if (newVal != oldVal && newVal[$scope.sId] != "")
                    {
                        $http({
                            method: 'GET',
                            url: api + 'EdiblesApi/FindByName?s=' + newVal[$scope.sId]
                        }).then(function (data) {
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
                $scope.setFoodId = function(foodId, mealIndex, weekIndex, dayIndex)
                {
                    $scope.foodId = 'w' + weekIndex + '_m' + mealIndex + '_d' + dayIndex + '_f' + foodId;
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