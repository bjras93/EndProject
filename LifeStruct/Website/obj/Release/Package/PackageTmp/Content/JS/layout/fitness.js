var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
var api = 'http://' + location.host + '/api/';
var fitness = {
    index: function() {
        app.controller('indexFitnessCtrl', ['$scope', '$http', function ($scope, $http) {
            $scope.fitnessInit = [];
            $http.get(api + 'FitnessApi/GetFitness').then(function (data) {
                $scope.fitnessInit = data.data;
            });
            $scope.l = {
                liked: {

                }
            }
            $scope.like = function (id) {
                $http({ method: 'POST', url: api + 'LikeApi/Like', data: JSON.stringify({ type: 2, typeId: id }), contentType: "application/json" }).then(function (data) {
                    $scope.l.liked[id] = data.data.UserId;
                });
            };
            $scope.getLikes = function (uId, dietId) {
                $http({ method: 'GET', url: api + 'LikeApi/FindByUIdType?uId=' + uId + '&type=' + 2 }).then(function (data) {
                    for (var i = 0; i < data.data.length; i++) {
                        if (dietId == data.data[i].TypeId) {
                            $scope.l.liked[dietId] = data.data[i].UserId;
                        }
                    }
                });
            };

            $scope.selectFitness = function (fitnessId, uId, selected) {
                $http({ method: 'POST', url: api + 'UserApi/SetDiet', data: JSON.stringify({ uId: uId, fId: fitnessId, type: 2, add: selected == fitnessId }), contentType: "application/json" }).then(function (data) {
                    $scope.selected = data.data.FitnessId;
                });
                $scope.selected = '';
            };
        }]);
    },
    schedule: function () {
        app.controller('FitnessCtrl', ['$scope', '$http', '$q', function ($scope, $http, $q) {

            $(document).on('click', '.panel', function (e) {
                e.preventDefault();
                $(this).next().toggleClass('hide');
            })
            var found = false,
                currentWeek = 1;
            $scope.tags = [];
            $scope.weeks = [1];
            $scope.eList = {
                exercise: []
            };
            $scope.exercises = [];
            $scope.allExercises = [];
            $scope.days = {
                day: {}
            };
            var days = $http({
                method: 'GET',
                url: api + 'DaysApi/GetDays'
            }).then(function (days) {
                for (var i = 1; i < (days.data.length+1); i++)
                {
                    $scope.days.day[i] = days.data[i-1].Name;
                }
            });

            // Initializes exercise inputs
            $scope.exerciseInit = function () {

                for (var i = 1; i < 8; i++) {
                        $scope.exercises.push({
                            id: '',
                            exercise: '',
                            interval: '',
                            exerciseId: '',
                            exerciseIndex: 1,
                            day: i,
                            week: 1,
                            calories: '',
                            subExercises: []
                        });
                    }
            }
            $scope.getSchedule = function (id) {
                $http.get(api + 'ScheduleApi/FindById?id=' + id).then(function (data) {
                    var sche = data.data.Schedule;
                    var week = Math.max.apply(Math, sche.map(function (o) { return o.Week }))
                    $scope.exercises = [];
                    
                    for (var w = 1; w < (week + 1) ; w++) {
                        if (w > 1) {
                            $scope.weeks.push(w);
                        }
                        for (var i = 1; i < 8; i++) {
                            $scope.exercises.push({
                                id: '',
                                exercise: '',
                                interval: '',
                                exerciseId: '',
                                exerciseIndex: 1,
                                day: i,
                                week: w,
                                calories: '',
                                subExercises: []
                            });
                        }
                    }
                    console.log(sche)
                    console.log($scope.exercises)
                    for (var w = 0; w < week; w++) {
                        for (var i = 0; i < sche.length; i++) {
                            if (sche[i].ExerciseIndex == 1) {
                                var exIndex = $scope.exercises.findIndex(function (x) {
                                    return x.day == (sche[i].Day+1) && x.week == sche[i].Week;
                                });
                                if (exIndex != -1) {
                                    $scope.exercises[exIndex].id = sche[i].Id;
                                    $scope.exercises[exIndex].exercise = sche[i].Exercise;
                                    $scope.exercises[exIndex].exerciseId = sche[i].ExerciseId;
                                    $scope.exercises[exIndex].exerciseIndex = sche[i].ExerciseIndex;
                                    $scope.exercises[exIndex].interval = sche[i].Time;
                                    $scope.exercises[exIndex].day = (sche[i].Day + 1);
                                    $scope.exercises[exIndex].week = sche[i].Week;
                                    $scope.exercises[exIndex].calories = sche[i].Calories;

                                }

                            }
                        }                        
                    }
                        for (var i = 0; i < sche.length; i++) {
                            
                            if(sche[i].ExerciseIndex > 1){
                                var exIndex = $scope.exercises.findIndex(function (x) {
                                    return x.day == (sche[i].Day + 1) && x.week == sche[i].Week;
                                });
                                if(exIndex > -1)
                                {
                                    $scope.exercises[exIndex].subExercises.push({
                                        id: sche[i].Id,
                                        exercise: sche[i].Exercise,
                                        interval: sche[i].Time,
                                        exerciseId: sche[i].ExerciseId,
                                        exerciseIndex: sche[i].ExerciseIndex,
                                        day: (sche[i].Day + 1),
                                        week: sche[i].Week,
                                        calories: sche[i].Calories
                                    });
                                }
                            }

                        }
                });
            }
            // Adds exercise
            $scope.addExercise = function (eIndex) {
                var split = eIndex.split('_'),
                     week = split[3][1],
                     day = split[2][1],
                     exercise = split[1][1],
                     exIndex = $scope.exercises.findIndex(function (x) {
                         return x.day == day && x.week == week && x.exerciseIndex == exercise;
                     });
                if (exIndex > -1) {
                    $scope.exercises[exIndex].subExercises.push({
                        id: '',
                        exercise: '',
                        interval: '',
                        exerciseId: '',
                        exerciseIndex: ($scope.exercises[exIndex].subExercises.length + 1),
                        day: day,
                        week: week,
                        calories: ''

                    });
                }
            };
            // Adds another week
            $scope.addWeek = function () {
                currentWeek++;
                $scope.weeks.push(currentWeek)
                for (var i = 1; i < 8; i++) {
                    $scope.exercises.push({
                        id: '',
                        exercise: '',
                        interval: '',
                        exerciseId: '',
                        exerciseIndex: 1,
                        day: i,
                        week: currentWeek,
                        calories: '',
                        subExercises: []
                    });
                }
            };
            // Sets calories inside input
            $scope.setCalories = function (event, eIndex) {
                var calories = event.target.getAttribute('data-val'),
                    exerciseId = event.target.getAttribute('data-id');
                $scope.eList.exercise['calories_' + eIndex] = calories;
                $scope.eList.exercise['exercise_' + eIndex] = exerciseId;
                $("#calories_" + eIndex).val(calories);
                $("#exerciseId_" + eIndex).val(exerciseId);

            }
            // Search
            $scope.sExercise = function (eId, sStr) {
                var search = $scope.eList.exercise['name_' + eId];
                if (search != undefined) {
                    if (search.length == 2) {
                        $http({
                            method: 'GET',
                            url: api + 'ExerciseApi/FindByName?s=' + search
                        }).then(function (data) {
                            $scope.allExercises = data.data;
                        });
                    }
                    for (var i = 0; i < $scope.allExercises.length; i++) {
                        if ($scope.allExercises[i].Name == search) {
                            found = true;
                        }
                    }
                    if (found) {
                        if (!$('#' + sStr).hasClass('hide')) {
                            $('#' + sStr).addClass('hide');
                        }
                    }
                    else {
                        if ($('#' + sStr).hasClass('hide')) {
                            $('#' + sStr).removeClass('hide');
                        }
                    }
                }
            }
        }]);
    },
    create: function () {
        app.controller('fitnessCreateCtrl', ['$scope', '$http', '$q', function ($scope, $http, $q) {
            $scope.tags = [];
        }]);
    }
}