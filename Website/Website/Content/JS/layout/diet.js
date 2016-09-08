var app = angular.module('yougo', []);

var diet = {
    init: function () {
        diet.post();
        diet.get();
    },
    post: function () {
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
    get: function () {

        app.controller('dietGetCtrl', ['$scope', '$http', function ($scope, $http) {
            var loc = location.href,
                id = loc.split('?id=')[1];
           
            $scope.collectData = function () {
            }
            if (id != undefined) {
                var p = id.split('&p=1')[0];
                var getDiet = {
                    method: 'GET',
                    url: 'http://local.yougo.dk/api/DietApi/GetDiet?id=' + p
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
                $scope.allFood = [];
                var getFood = {
                    method: 'GET',
                    url: 'http://local.yougo.dk/api/FoodApi'
                }

                $http(getFood).then(function (data) {
                    for (var i = 0; i < data.data.length; i++){
                        $scope.allFood.push(data.data[i]);
                    }
                    console.log($scope.allFood);
                });
                // Add
                var weekDay = new Array(7),
                    meals = new Array(4);
                weekDay[0] = "Monday";
                weekDay[1] = "Tuesday";
                weekDay[2] = "Wednesday";
                weekDay[3] = "Thursday";
                weekDay[4] = "Friday";
                weekDay[5] = "Saturday";
                weekDay[6] = "Sunday";
                meals[0] = "Breakfast";
                meals[1] = "Lunch";
                meals[2] = "Dinner";
                meals[3] = "Snack";
                $scope.weekDays = weekDay;
                var currentW = 1;
                var days = [];
                var mealsEach = [];
                $scope.week = [];
                $scope.dC = ["Monday,1"];
                $scope.wC = [1];

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
                for (var i = 0; i < weekDay.length; i++) {
                    days.push({
                        day: weekDay[i],
                        edible: '',
                        calories: '',
                        meals: []
                    })
                    
                }
                
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

                $scope.amountS = [];
                $scope.amountSL = [];
                for (var i = 0; i < days.length; i++) {
                    for (var m = 0; m < meals.length; m++) {
                        $scope.amountS.push(m + ',' + i);
                        days[i].meals.push({
                            name: meals[m],
                            no: m,
                            edibles: []
                        });
                    }
                }
                $scope.setMeasure = function (measure, index, pIndex, aEdible) {
                    
                    if (aEdible == null) {
                        $scope.am.amountS[index + '' + pIndex] = measure;
                    }
                    else {
                        $scope.am.amountSL[index + '' + pIndex + '' + aEdible + ''] = measure;
                        console.log(index + '' + pIndex + '' + aEdible + '')

                    }
                }
                $scope.am = {
                    amountS: {
                    },
                    amountSL: {

                    }
                }
                $scope.addEdible = function (day, week, meal) {
                    $scope.week[0].days[weekDay.indexOf(day)].meals[meal].edibles.push({
                        edible: '',
                        amount: ''                        
                    });
                }
                
            }

        }]);

    }


}
diet.init();