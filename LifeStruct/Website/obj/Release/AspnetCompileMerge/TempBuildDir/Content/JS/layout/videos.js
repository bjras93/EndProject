var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
api = 'http://' + location.host + '/api/';
var videos = {
    init: function () {
        app.controller('videoIndexCtrl', ['$scope', '$http', '$document', '$sce', function ($scope, $http, $document, $sce) {
            $scope.page = 0;
            var t = 0;
            $scope.videoList = function (page, take, np, type) {
                if (t != type) {
                        t = type;
                    if (page == -1) {
                        page = 0;
                        $scope.page = 0;
                    }
                    $scope.videos = [];
                    if (np) {
                        $scope.page++;
                        page++;
                    }
                    else {
                        $scope.page = $scope.page - 1;
                        page = page - 1;
                    }
                    var skip = page * take;
                    $http({ method: 'POST', url: api + 'VideoApi/GetVideos', data: JSON.stringify({ take: take, skip: skip, type: type }), contentType: "application/json" }).then(function (data) {
                        $scope.videos = data.data;
                    });
                    $('#type-' + type).parent().children().each(function () {
                        $(this).removeClass('selected')
                    });
                    $('#type-' + type).addClass('selected');
                    localStorage.setItem('page', type)
                }
            }
            if (localStorage.getItem('page') == null) {
                $scope.videoList(0, 20, false, 2);
            }
            else {
                $scope.videoList(0, 20, false, localStorage.getItem('page'));

            }
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

            $('.btn-article').on('click', function (e) {
                e.preventDefault();
                if ($scope.videos.findIndex(x => x.Type == $(this).data('type')) == -1) {
                    $(this).parent().children().each(function () {
                        if ($(this).hasClass('selected')) {
                            $(this).removeClass('selected');
                        }
                    });
                    $(this).addClass('selected');
                }
            });
        }]);
    },
    create: function () {
        app.controller('videoCreateCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
            $scope.tags = [];
        }]);
    },
    edit: function () {        
        app.controller('videoEditCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {

        }]);
    }
}
var maxLen = 400;
$('.max-400').keydown(function(event){
    var Length = $(".max-400").val().length;
    var AmountLeft = maxLen - Length;
    $('#txt-length-left').html(AmountLeft + ' characters left');
    if(Length >= maxLen){
        if (event.which != 8) {
            return false;
        }
    }
    
});

var setUrl = function (t) {
    var data = $(t).prev().data('source')
    $(t).prev().prev().attr('src', data);
    $(t).hide();
}