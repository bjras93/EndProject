var app = angular.module('LifeStruct', ['ngTagsInput']);
var videos = {
    init: function () {

    },
    create: function () {
        app.controller('videoCreateCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {

            $scope.tags = [];
            console.log($scope.tags)
        }]);

    }
}