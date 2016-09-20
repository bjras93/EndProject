var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
    api = 'http://' + location.host + '/api/';
var videos = {
    init: function () {
        app.controller('videoIndexCtrl', ['$scope', '$http', '$document', '$sce', function ($scope, $http, $document, $sce) {
            $scope.videos = [];
            $http({ method: 'GET', url: api + 'VideoApi/GetVideos' }).then(function (data) {
                $scope.videos = data.data;
            });

            $scope.getFrameSrc = function (source) {
                return $sce.trustAsResourceUrl('https://youtube.com/embed/' + source);
            }
        }]);
    },
    create: function () {
        app.controller('videoCreateCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {

            $scope.tags = [];
            console.log($scope.tags)
        }]);
    }
}