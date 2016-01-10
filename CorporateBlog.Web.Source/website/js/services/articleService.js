(function (angular) {
    'use strict';

    angular.module('services').factory('articleService', ['$http', function ($http) {
        var getByDateRange = function (model) {
            return $http({
                url: "api/Article/GetBydateRange",
                method: "GET",
                params: model
            });
        };

        var createArticle = function (model) {
            return $http.post("api/Article/Add", model);
        };

        var getArticle = function (id) {
            return $http({
                url: "api/Article/" + id,
                method: "GET"
            });
        };

        var deleteArticle = function (id) {
            return $http({
                url: "api/Article/Delete/" + id,
                method: "DELETE"
            });
        };

        var updateArticle = function (model) {
            return $http({
                url: "api/Article/Update",
                method: "PUT",
                data: model
            });
        };

        var getDateLimit = function () {
            return $http({
                url: "api/Article/GetDateLimit",
                method: "GET"
            });
        };

        var voteForArticle = function (model) {
            return $http({
                url: "api/ArticleRate/Vote",
                method: "POST",
                data: model
            });
        };

        return {
            getByDateRange: getByDateRange,
            createArticle: createArticle,
            getArticle: getArticle,
            deleteArticle: deleteArticle,
            updateArticle: updateArticle,
            getDateLimit: getDateLimit,
            voteForArticle: voteForArticle
        };
    }
    ]);
})(angular);