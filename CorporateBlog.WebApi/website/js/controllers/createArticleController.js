(function (angular) {
    'use strict';

    angular.module("controllers").controller('CreateArticleController', [
        '$scope',
        'settings',
        'accountService',
        'sharedCategory',
        '$location',
        'articleService', function ($scope, settings, accountService, sharedCategory, $location, articleService) {
            var redirectIfNeed = function() {
                var roleName = accountService.getAuthorizationData().roleName;
                var isClient = roleName === settings.clientRoleName;

                if (isClient || !sharedCategory.getCategory()) {
                    $location.path('#/categories');
                }
            };

            //redirectIfNeed();

            $scope.category = sharedCategory.getCategory();

            $scope.article = {
                header: '',
                body: ''
            };

            $scope.createArticle = function(article) {
                articleService.createArticle({
                    title: article.header,
                    text: article.body,
                    categoryId: $scope.category.id
                }).then(function() {
                   // clearArticleModel();
                });
            };

            function clearArticleModel() {
                $scope.article = {
                    header: '',
                    body: ''
                };
            }
        }]);

})(angular);