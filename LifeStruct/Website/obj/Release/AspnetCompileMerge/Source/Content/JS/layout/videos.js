var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
api = 'http://' + location.host + '/api/';
var videos = {
    init: function () {
        app.controller('videoIndexCtrl', ['$scope', '$http', '$document', '$sce', function ($scope, $http, $document, $sce) {
            $scope.page = 0;
            $scope.videoList = function (page, take, np) {
                if (page == -1)
                {
                    page = 0;
                    $scope.page = 0;
                }
                $scope.videos = [];
                if (np) {
                    $scope.page++;
                    console.log($scope.page)
                    page++;
                }
                else
                {
                    $scope.page = $scope.page - 1;
                    page = page - 1;
                }                
                var skip = page * take;
                $http({ method: 'POST', url: api + 'VideoApi/GetVideos', data: JSON.stringify({ take: take, skip: skip }), contentType: "application/json" }).then(function (data) {
                    $scope.videos = data.data;
                });
            }
            $scope.videoList(0, 20, false);
            $scope.getFrameSrc = function (source) {
                return $sce.trustAsResourceUrl('https://www.youtube.com/v/' + source + '&autoplay=1&rel=0');
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

var setUrl = function (t) {
    var data = $(t).prev().data('source')
    $(t).prev().prev().attr('src', data);
    $(t).hide();
}