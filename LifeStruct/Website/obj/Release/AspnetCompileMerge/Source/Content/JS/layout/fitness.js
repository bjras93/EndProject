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
                exercise: '',
                interval: ''
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
            $scope.setCalories = function (event, eIndex, rIndex) {
                console.log()
                var calInput = '#s_calories_e' + eIndex,
                    calories = event.target.getAttribute('data-val');
                $scope.eList.exercise['s_calories_e' + eIndex] = calories;
                

               
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