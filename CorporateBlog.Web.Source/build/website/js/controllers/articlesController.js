﻿(function (angular, moment) {
    'use strict';

    angular.module("controllers").controller('ArticlesController', [
        '$scope',
        '$sce',
        '$filter',
        'sharedCategory',
        'articleService',
        'settings',
        '$q', function ($scope, $sce, $filter, sharedCategory, articleService, settings, $q) {

            var dateFormat = "MM-DD-YYYY hh:mm:ss A";
            var getCurrentWeek = function () {
                return {
                    startDate: moment().subtract(7, 'days').hours(0).minutes(0).seconds(0),
                    endDate: moment().hours(23).minutes(59).seconds(59)
                };
            };

            $scope.dateLimit = null;

            var getDateLimit = function (categoryId) {
                var deferred = $q.defer();
                articleService.getDateLimit(categoryId).then(function (response) {
                    var limit = response.data;
                    if (limit !== null) {
                        $scope.dateLimit = [moment.utc(limit[0]).local(), moment.utc(limit[1]).local()];
                    }

                    deferred.resolve();
                });

                return deferred.promise;
            };


            $scope.category = null;
            $scope.articles = [];
            $scope.currentWeek = getCurrentWeek();
            $scope.isLoadEarlierAvailable = false;

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
                    $scope.articles = [];
                    $scope.dateLimit = null;
                    $scope.isLoadEarlierAvailable = false;

                    getDateLimit($scope.category.id).then(function () {
                        if ($scope.dateLimit != null) {
                            $scope.currentWeek = getCurrentWeek();
                            $scope.loadArticles();
                        }
                    });
                }
            });

            $scope.$watch(function () {
                return $scope.currentWeek.startDate;
            }, function () {
                if ($scope.dateLimit != null) {
                    if ($scope.currentWeek.startDate <= $scope.dateLimit[0]) {
                        $scope.isLoadEarlierAvailable = false;
                    } else {
                        $scope.isLoadEarlierAvailable = true;
                    }
                }
            });


            $scope.loadArticles = function () {
                var passData = {
                    categoryId: $scope.category.id,
                    startDate: $scope.currentWeek.startDate.format(dateFormat),
                    endDate: $scope.currentWeek.endDate.format(dateFormat)
                };

                articleService.getByDateRange(passData).then(function (response) {
                    if (response.data.length === 0) {
                    }

                    var mapped = _.map(response.data, function (article) {
                        var shortText = $filter('limitTo')(article.text, settings.limitSymbolsTo) + settings.limitContentEnding;
                        var avatar = null;

                        if (article.user.userInfo) {
                            avatar = article.user.userInfo.avatar;
                        }

                        return angular.extend(article, {
                            link: '#/articles/' + article.id,
                            content: $sce.trustAsHtml(shortText),
                            editLink: '#/articles/update/' + article.id,
                            avatar: avatar
                        });
                    });

                    $scope.articles.push.apply($scope.articles, mapped);
                });
            };

            $scope.deleteArticle = function (article) {
                articleService.deleteArticle(article.id).then(function () {
                    _.remove($scope.articles, function (art) {
                        return art.id == article.id;
                    });
                });
            };

            $scope.loadEarlier = function () {
                var newEndDate = moment($scope.currentWeek.startDate).hours(23).minutes(59).seconds(59);
                var newStartDate = moment($scope.currentWeek.startDate)
                    .subtract(7, 'days').hours(0).minutes(0).seconds(0);
                $scope.currentWeek.endDate = newEndDate;
                $scope.currentWeek.startDate = newStartDate;
                $scope.loadArticles();
            };

        }]);

})(angular, moment);