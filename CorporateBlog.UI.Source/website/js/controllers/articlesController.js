(function (angular) {
    'use strict';

    angular.module("controllers").controller('ArticlesController', [
        '$scope',
        'sharedCategory', function ($scope, sharedCategory) {
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
            }, $scope.loadArticles);


            $scope.loadArticles = function () {
                //building articles query
            };

        }]);

})(angular);