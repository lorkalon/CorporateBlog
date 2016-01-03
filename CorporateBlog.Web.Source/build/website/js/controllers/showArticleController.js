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

            $scope.voteForArticle = function(rate) {
                articleService.voteForArticle({
                    articleId: $scope.article.id,
                    value: rate
                }).then(function() {
                    $scope.article.rate += rate;
                });
            };

        }]);
})(angular);