(function (angular) {
    'use strict';

    angular.module("controllers").controller('ArticlesController', [
        '$scope',
        '$sce',
        '$filter',
        'sharedCategory',
        'articleService',
        'settings', function ($scope, $sce, $filter, sharedCategory, articleService, settings) {

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
                    startDate: new Date(2015, 10, 10),
                    endDate: new Date(2016, 11, 7)
                };

                articleService.getByDateRange(passData).then(function (response) {
                    if (!response.data) {
                        return;
                    }

                    var mapped = _.map(response.data, function (article) {
                        var shortText = $filter('limitTo')(article.text, settings.limitSymbolsTo) + settings.limitContentEnding;
                        return angular.extend(article, {
                            link: '#/articles/' + article.id,
                            content: $sce.trustAsHtml(shortText),
                            editLink: '#/articles/update/' + article.id
                        });
                    });

                    $scope.articles = mapped;
                });
            };

        $scope.deleteArticle = function(article) {
            articleService.deleteArticle(article.id).then(function() {
                _.remove($scope.articles, function(art) {
                    return art.id == article.id;
                });
            });
        };

    }]);

})(angular);