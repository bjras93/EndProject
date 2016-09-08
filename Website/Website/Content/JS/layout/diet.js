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
                $scope.plans = [{ title: "", edible: "", calories: "", edibles: [] }]

                $scope.addPlan = function () {
                    $scope.plans.push({
                        title: "",
                        edible: "",
                        calories: "",
                        edibles: []
                    })
                };
                $scope.addEdible = function (index) {
                    $scope.plans[index].edibles.push({
                        edible: "",
                        calories: ""
                    });
                }
            }

        }]);

    }

}
diet.init();