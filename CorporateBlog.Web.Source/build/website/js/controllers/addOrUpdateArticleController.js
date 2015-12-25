(function (angular) {
    'use strict';

    angular.module("controllers").controller('AddOrUpdateArticleController', [
        '$scope',
        'settings',
        'accountService',
        '$location',
        'articleService',
        '$routeParams',
        'categoryService', function ($scope, settings, accountService, $location, articleService, $routeParams, categoryService) {

            var Article = function(data) {
                this.id = null;
                this.title = '';
                this.text = '';
                this.categoryId = null;

                if (data) {
                    this.id = data.id;
                    this.title = data.title;
                    this.text = data.text;
                    this.categoryId = data.categoryId;
                }
            };

            $scope.article = {};
            $scope.categories = [];
            $scope.selectedCategory = null;

            var articleId = $routeParams.id;
           
            var createArticle = function (article) {
                articleService.createArticle(article).then(function (response) {
                    if (response.data.id) {
                        var url = '/articles/' + response.data.id;
                        $location.path(url);
                    } else {
                        $location.path('/categories');
                    }
                });
            };

            var updateArticle = function(article) {
                articleService.updateArticle(article).then(function(response) {
                    var url = '/articles/' + $scope.article.id;
                    $location.path(url);
                });
            };

            categoryService.getAll().then(function(response) {
                $scope.categories = response.data;
                
                if (articleId) {
                    articleService.getArticle(articleId).then(function(response) {
                        $scope.article = new Article(response.data);
                        $scope.selectedCategory = response.data.category;
                    });
                } else {
                    $scope.article = new Article();
                    $scope.selectedCategory = _.first($scope.categories);
                }
            });


            $scope.saveChanges = function () {
                $scope.article.categoryId = $scope.selectedCategory.id;

                if ($scope.article.id !== null) {
                    updateArticle($scope.article);
                } else {
                    createArticle($scope.article);
                }
            };
        }]);

})(angular);