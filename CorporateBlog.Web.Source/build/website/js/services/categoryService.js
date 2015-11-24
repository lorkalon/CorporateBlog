(function(angular) {
    'use strict';

    angular.module('services').factory('categoryService', [
        '$http',
        '$q',
        'localStorageService', function ($http, $q, localStorageService) {
            var getAll = function() {
                return $http.get('api/Category/GetAll');
            };

            var createCategory = function(model) {
                return $http.post('api/Category/Create', model);
            };

            var deleteCategory = function(id) {
                return $http.delete('api/Category/Delete/' + id);
            };

            return {
                getAll: getAll,
                createCategory: createCategory,
                deleteCategory: deleteCategory
            };
        }
    ]);
})(angular);