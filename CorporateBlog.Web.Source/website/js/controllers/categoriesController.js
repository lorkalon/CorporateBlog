(function (angular) {
    'use strict';

    angular.module("controllers").controller('CategoriesController', [
        'categoryService',
        '$scope',
        'accountService',
        'sharedCategory',
        'settings', function (categoryService, $scope, accountService, sharedCategory, settings) {

            $scope.categories = [];
            $scope.categoryModel = {
                name: ''
            };

            var roleName = accountService.getAuthorizationData().roleName;

            $scope.isUserAdmin = roleName === settings.adminRoleName;
            $scope.isUserPublisher = roleName === settings.publisherRoleName;

            var getAllCategories = function () {
                categoryService.getAll().then(function(response) {
                    $scope.categories = response.data;
                    sharedCategory.setCategory(_.first($scope.categories));
                });
            };

            $scope.createCategory = function(categoryModel) {
                categoryService.createCategory(categoryModel).then(function() {
                    getAllCategories();
                });
            };

            $scope.deleteCategory = function(categoryModel) {
                categoryService.deleteCategory(categoryModel.id).then(function() {
                    getAllCategories();
                });
            };

            $scope.selectCategory = function (category) {
                sharedCategory.setCategory(category);
            };

            getAllCategories();
        }]);

})(angular);