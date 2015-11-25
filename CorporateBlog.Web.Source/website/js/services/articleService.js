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

            return {
                getByDateRange: getByDateRange
            };
        }
    ]);
})(angular);