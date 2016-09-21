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

            $scope.deleteVideo = function (id, str, userId) {
                var w = window.confirm("Are you sure you'd like to delete " + str);
                if(w)
                {
                    $http({ method: 'GET', url: api + 'VideoApi/DeleteVideo?id=' + id + '&userId=' + userId }).then(function () {
                        $scope.videos.splice($scope.videos.findIndex(x => x.Id == id), 1);
                    });
                }
            }

            
        }]);
    },
    create: function () {
        app.controller('videoCreateCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {

            $scope.tags = [];
            console.log($scope.tags)
        }]);
    },
    edit: function () {        
        app.controller('videoEditCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {

        }]);
    }
}