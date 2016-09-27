var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
var api = 'http://' + location.host + '/api/';

var article = {
    init: function () {
        app.controller('articleIndexCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
            $scope.articles = [];
            $scope.likes = {
                like: {}
            };
            $http({ method: 'GET', url: api + 'HealthApi/GetArticles?type=' + 1 }).then(function (data) {
                $scope.articles = data.data;
            });
            $scope.findUser = function (uId, aId, data) {

                return data[data.findIndex(function (x) {
                    return x.UserId == uId && x.ArticleId == aId;
                })];
            };
            $scope.like = function (id) {
                $http({ method: 'POST', url: api + 'LikeApi/Like', data: JSON.stringify({ type: 3, typeId: id }), contentType: "application/json" }).then(function (data) {
                    $scope.likes.like[id] = data.data.UserId;
                });
            };
            $scope.getLikes = function (uId, articleId) {
                $http({ method: 'GET', url: api + 'LikeApi/FindByUIdType?uId=' + uId + '&type=' + 3 }).then(function (data) {
                    for (var i = 0; i < data.data.length; i++) {
                        if (articleId == data.data[i].TypeId) {
                            $scope.likes.like[articleId] = data.data[i].UserId;
                        }
                    }
                });
            };
            $scope.deleteArticle = function (user, article) {
                var w = window.confirm("Are you sure you'd like to delete " + article.Title);
                if (w) {
                    $http({ method: 'GET', url: api + 'HealthApi/DeleteArticle?id=' + article.Id + '&userId=' + user }).then(function () {
                        $scope.articles.Articles.splice($scope.articles.Articles.indexOf(article), 1);
                    })
                }
            }

            $('.btn-article').on('click', function (e) {
                e.preventDefault();
                if ($scope.articles.Articles.findIndex(function (x) {  return x.Type == $(this).data('type');
                }) == -1) {
                    $(this).parent().children().each(function () {
                        if ($(this).hasClass('selected')) {
                            $(this).removeClass('selected');
                        }
                    });
                    $scope.articles = [];
                    $http({ method: 'GET', url: api + 'HealthApi/GetArticles?type=' + $(this).data('type') }).then(function (data) {
                        $scope.articles = data.data;
                    });
                    $(this).addClass('selected');
                }
            });
        }]);
    },
    create: function () {
        app.controller('articleCreateCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
            $scope.tags = [];
        }]);
    }
}