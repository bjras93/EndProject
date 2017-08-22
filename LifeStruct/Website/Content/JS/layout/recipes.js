var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']),
    ingredients = { Id: 1, Name: '', Calories: '', Amount: '' },
    arrayOfIngredients = [],
    url = location.origin + '/api/';
arrayOfIngredients.push(ingredients);
var lastIngredient = arrayOfIngredients[arrayOfIngredients.length - 1];

app.controller('recipes-create_controller', ['$scope', '$http', function ($scope, $http) {
    var recipes = {
        init: function () {
            $scope.ingredients = arrayOfIngredients;
            $scope.addIngredient = recipes.addIngredient;
            $scope.submitRecipes = recipes.submitRecipes;
            $scope.searchFood = recipes.searchFood;
            $scope.selectFood = recipes.selectFood;
            $scope.keyDown = autocomplete.keyDown;
        },
        addIngredient: function () {
            var id = lastIngredient.Id,
                n = '',
                c = '',
                a = '';
            recipes.pushArray(id + 1, n, c, a);
            $scope.ingredients = arrayOfIngredients;
        },
        pushArray: function (id, n, c, a) {
            arrayOfIngredients.push({ Id: id, Name: n, Calories: c, Amount: a });
            lastIngredient = arrayOfIngredients[arrayOfIngredients.length - 1];
        },
        submitRecipes: function (d) {
            $http.post(url + 'RecipesApi/AddRecipes', JSON.stringify(d)).success(function (data) {
                console.log(data);
            });
        },
        searchFood: function (d) {
            var found = false;
            if ($scope.allFood != null) {
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
                }).then(function(data) {
                    $scope.allFood = data.data;
                });
            }
        },
        selectFood: function (i, f) {
            i.Calories = f.Calories;
            i.Name = f.Name;
        }
    }
    recipes.init();
        autocomplete.init();
    }
]);