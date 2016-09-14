var app = angular.module('LifeStruct', []);
var api = 'http://' + location.host + '/api/';
var fitness = {
    init: function () {

    },
    schedule: function () {
        app.controller('FitnessCtrl', ['$scope', '$http', function ($scope, $http, $document) {
            var found = false;

            $scope.eList = {
                exercise: []
            };
            $scope.exercises = [{
                id: '',
                exercise: '',
                interval: '',
                exerciseId: '',
                day: '',
                calories: ''
            }];
            $scope.allExercises = [];
            $scope.showAc = function (event) {
                $(event.target).parent('.autocomplete').show();
            }
            $scope.sExercise = function (eId, sStr) {
                var search = $scope.eList.exercise[eId];
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
            $scope.getSchedule = function (fId) {
                var eNames = [];
                $http({
                    method: 'GET',
                    url: api + 'ScheduleApi/FindById?id=' + fId
                }).then(function (data) {
                    $scope.exercises = [];
                    for (var i = 0; i < data.data.length; i++) {
                        $scope.exercises.push({ id: data.data[i].Id, exercise: '', interval: data.data[i].Time, exerciseId: data.data[i].ExerciseId, day: data.data[i].Day, calories: '' });
                    
                        $http({
                            method: 'GET',
                            url: api + 'ExerciseApi/FindById?id=' + data.data[i].ExerciseId
                        }).then(function (eData) {
                            for (var e = 0; e < $scope.exercises.length; e++) {
                                $scope.exercises[e].exercise = eData.data.Name;
                                $scope.exercises[e].calories = eData.data.Calories;
                            }
                        });
                        console.log($scope.exercises)
                    }
                });
            }
            $scope.setCalories = function (event, eIndex, rIndex) {
                var calories = event.target.getAttribute('data-val')
                exerciseId = event.target.getAttribute('data-id');
                $scope.eList.exercise['s_calories_e' + eIndex] = calories;
                $scope.eList.exercise['s_exerciseId_e' + eIndex] = exerciseId;


            }
            $scope.addExercise = function (e) {
                e.preventDefault();
                $scope.exercises.push({
                    exercise: '',
                    interval: ''
                });
            }
        }]);
    }
}