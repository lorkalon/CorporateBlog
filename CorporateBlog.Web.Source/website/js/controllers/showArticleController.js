(function (angular) {
    'use strict';

    angular.module("controllers").controller('ShowArticleController', [
        '$scope',
        '$routeParams',
        'sharedCategory',
        'articleService', function ($scope, $routeParams, sharedCategory, articleService) {

            $scope.article = {};
            $scope.avatar = null;

            articleService.getArticle($routeParams.id).then(function (response) {
                if (response.data) {
                    angular.extend($scope.article, response.data);

                    if ($scope.article.user.userInfo) {
                        $scope.avatar = $scope.article.user.userInfo.avatar;
                    }

                }
            });

            $scope.voteForArticle = function(rate) {
                articleService.voteForArticle({
                    articleId: $scope.article.id,
                    value: rate
                }).then(function() {
                    $scope.article.rate += rate;
                    $scope.article.currentUserRate = rate;
                });
            };

        }]);
})(angular);