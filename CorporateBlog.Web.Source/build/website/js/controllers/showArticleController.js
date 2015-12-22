(function (angular) {
    'use strict';

    angular.module("controllers").controller('ShowArticleController', [
        '$scope',
        '$routeParams',
        'sharedCategory',
        'articleService', function ($scope, $routeParams, sharedCategory, articleService) {

            $scope.article = {};

            articleService.getArticle($routeParams.id).then(function (response) {
                if (response.data) {
                    angular.extend($scope.article, response.data);
                }
            });

        }]);
})(angular);