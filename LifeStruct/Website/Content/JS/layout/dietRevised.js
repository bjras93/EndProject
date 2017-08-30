var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']),
    dietModel = { Id: '', Title: '', Description: '', User: '', Likes: 0, Weeks: 0, Img: '', Tags: '', Author: '' },
    mealsCollectionModel = [],
    url = location.origin + '/api/';

app.controller('diet-controller', ['$scope', '$http', function ($scope, $http) {
    var diet = {
        init: function () {
            diet.setMealCollection();
            $scope.dietModel = dietModel;
            $scope.mealsCollectionModel = mealsCollectionModel;
            // Using globalVars.js to get meals and days
            $scope.days = days;
            $scope.meals = meals;
            $scope.addEdible = diet.addEdible;
            $scope.createDiet = diet.createDiet;
            $scope.searchFood = diet.searchFood;
            $scope.getDiet = diet.getDiet;
            $scope.errors = [];
            if (typeof autocomplete !== 'undefined') {
                $scope.keyDown = autocomplete.keyDown;
            }
        },
        setMealCollection: function () {
            for (var d = 0; d < days.length; d++) {
                for (var meal = 0; meal < meals.length; meal++) {
                    mealsCollectionModel.push({ Id: '', Name: '', Meal: meal, WeekNo: 1, FoodId: '', DietId: '', Amount: 0, Edible: 1, Day: d });
                }
            }

        },
        createDiet: function (dietModel, mealCollectionModel) {
            $scope.errors = error.validModel($scope.dietModel, $scope.errors, null);
            console.log($scope.errors);
            if ($scope.errors.length === 0) {
                $http.post(url + 'DietApi/PostDiet', { DietModel: dietModel, MealCollectionModel: mealCollectionModel }).success(function (data) {
                    location.href = location.origin + '/Diets/Edit?dietId=' + data;
                });
            }
        },
        getDiet: function (dietId) {
            $http.get(url + 'DietApi/GetDiet?dietId=' + dietId).success(function (data) {
                $scope.dietModel = data.DietModel;
                $scope.mealsCollectionModel = data.MealCollectionModel;
            });
            
        },
        addEdible: function (weekNo, meal, day, edible) {
            diet.pushArray(weekNo, meal, day, edible);
            $scope.mealsCollectionModel = mealsCollectionModel;
        },
        pushArray: function (weekNo, meal, day, edible) {
            mealsCollectionModel.push({ Id: '', Name: '', Meal: meal, WeekNo: weekNo, FoodId: '', DietId: '', Amount: 0, Edible: edible, Day: day });
        },
        searchFood: function (d) {
            var found = false;
            if ($scope.allFood !== undefined) {
                for (var i = 0; i < $scope.allFood.length; i++) {
                    if ($scope.allFood[i].Name.indexOf(d) !== -1) {
                        found = true;
                    }
                }
            }
            if (!found) {
                $http({
                    method: 'GET',
                    url: url + '/EdiblesApi/FindByName?s=' + d
                }).then(function (data) {
                    $scope.allFood = data.data;
                });
            }
        },
        selectFood: function (i, f) {
            i.Name = f.Name;
            i.FoodId = f.Id;
        }
    }
    diet.init();
    if (typeof autocomplete !== 'undefined') {
        autocomplete.init();
    }
}
]).directive("removeMe", function () {
    return {
        link: function (scope, element) {
            element.bind("click", function () {
                element.remove();
            });
        }
    }

});
