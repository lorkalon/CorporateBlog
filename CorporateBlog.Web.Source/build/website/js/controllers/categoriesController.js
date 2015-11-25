(function (angular) {
    'use strict';

    angular.module("controllers").controller('CategoriesController', [
        'categoryService',
        '$scope',
        'accountService',
        'sharedCategory', function (categoryService, $scope, accountService, sharedCategory) {

            $scope.categories = [];
            $scope.categoryModel = {
                name: ''
            };

            var adminRole = "Admin";
            var roleName = accountService.getAuthorizationData().roleName;

            $scope.isUserAdmiin = roleName === adminRole ;

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