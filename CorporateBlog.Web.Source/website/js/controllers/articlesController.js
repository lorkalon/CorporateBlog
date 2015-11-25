(function (angular) {
    'use strict';

    angular.module("controllers").controller('ArticlesController', [
        '$scope',
        'sharedCategory',
        'articleService', function ($scope, sharedCategory, articleService) {

            $scope.category = null;
            $scope.articles = [];

            $scope.$watch(function () {
                return sharedCategory.getCategory();
            }, function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    $scope.category = newValue;
                }
            });

            $scope.$watch(function () {
                return $scope.category;
            }, function () {
                if ($scope.category) {
                    $scope.loadArticles();
                }
            });

            $scope.loadArticles = function () {
                var passData = {
                    categoryId: $scope.category.id,
                    startDate: new Date(2015, 10, 1),
                    endDate: new Date(2015, 10, 30)
                };

                articleService.getByDateRange(passData).then(function (response) {
                    $scope.articles = response.data;
                });
            };

        }]);

})(angular);