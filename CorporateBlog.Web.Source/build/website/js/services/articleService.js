(function (angular) {
    'use strict';

    angular.module('services').factory('articleService', [
        '$http',
        '$q',
        'localStorageService', function ($http, $q, localStorageService) {
            var getByDateRange = function (model) {
                return $http({
                    url: "api/Article/GetBydateRange",
                    method: "GET",
                    params: model
                });
            };

            var createArticle = function(model) {
                return $http.post("api/Article/Add", model);
            };

            var getArticle = function(id) {
                return $http({
                    url: "api/Article/" + id,
                    method: "GET"
                });
            };

            return {
                getByDateRange: getByDateRange,
                createArticle: createArticle,
                getArticle: getArticle
            };
        }
    ]);
})(angular);