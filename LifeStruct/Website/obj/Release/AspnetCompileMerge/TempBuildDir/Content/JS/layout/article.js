var app = angular.module('LifeStruct', ['ngTagsInput', 'ngSanitize']);
var api = 'http://' + location.host + '/api/';

var article = {
    init: function() {        
        app.controller('articleIndexCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
            $scope.articles = [];
            $http({ method: 'GET', url: api + 'HealthApi/GetArticles' }).then(function (data) {
                console.log(data)
                $scope.articles = data.data;
            })
            $scope.findUser = function (uId, aId, data) {
                
                return data[data.findIndex(x => x.UserId == uId && x.ArticleId == aId)];
            }
        }]);
    },
    create: function () {
        app.controller('articleCreateCtrl', ['$scope', '$http', '$document', function ($scope, $http, $document) {
           
        }]);
    }
}