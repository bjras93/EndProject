var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']),
    ingredients = { Id: 1, Name: '', Calories: '', Amount: '', foodId: '' },
    recipesModel = { Id: '', Description: '', UserId: '' },
    arrayOfIngredients = [];
arrayOfIngredients.push(ingredients);
var lastIngredient = arrayOfIngredients[arrayOfIngredients.length - 1];

app.controller('recipes-create_controller', ['$scope', '$http', function ($scope, $http) {
    var recipes = {
        init: function () {
            $scope.success = false;
            $scope.ingredients = arrayOfIngredients;
            $scope.addIngredient = recipes.addIngredient;
            $scope.submitRecipes = recipes.submitRecipes;
            $scope.searchFood = recipes.searchFood;
            $scope.selectFood = recipes.selectFood;
            $scope.getRecipes = recipes.getRecipes;
            if (typeof autocomplete !== 'undefined') {
                $scope.keyDown = autocomplete.keyDown;
            }
            $scope.recipes = recipesModel;
        },
        addIngredient: function () {
            var id = lastIngredient.Id,
                n = '',
                c = '',
                a = '',
                fId = '';
            recipesModel.pushArray(id + 1, n, c, a, fId);
            $scope.ingredients = arrayOfIngredients;
        },
        pushArray: function (id, n, c, a, fId) {
            arrayOfIngredients.push({ Id: id, Name: n, Calories: c, Amount: a, FoodId: fId });
            lastIngredient = arrayOfIngredients[arrayOfIngredients.length - 1];
        },
        submitRecipes: function (im, rm) {
            $scope.errors = error.validModel($scope.recipes, $scope.errors, null);
            
            if ($scope.errors.length === 0) {
                $http.post(url + 'RecipesApi/AddRecipes', { Im: im, Rm: rm }).success(function(data) {
                    $scope.success = true;
                    setTimeout(function() {
                            if (rm.Id === '' || rm.Id === undefined) {
                                location.href = location.origin + '/Recipes/Edit?recipesId=' + data;
                            } else {
                                location.href = location.origin + '/Recipes/Details?recipesId=' + data;
                            }

                        },
                        300);
                });
            }
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
                }).then(function(data) {
                    $scope.allFood = data.data;
                });
            }
        },
        selectFood: function (i, f) {
            i.Calories = f.Calories;
            i.Name = f.Name;
            i.FoodId = f.Id;
        },
        getRecipes: function(recipesId) {
            $http.get(url + 'RecipesApi/GetRecipes?recipesId=' + recipesId).success(function (data) {
                $scope.ingredients = data.IngredientsModelList;
                $scope.recipes = data.RecipesModel;
                arrayOfIngredients = data.IngredientsModelList;
            });
        }
    }
    recipes.init();
        if (typeof autocomplete !== 'undefined') {
            autocomplete.init();
        }
    }
]);