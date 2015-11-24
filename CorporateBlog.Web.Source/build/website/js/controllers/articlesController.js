(function (angular) {
    'use strict';

    angular.module("controllers").controller('ArticlesController', [
        '$scope',
        'sharedCategory', function ($scope, sharedCategory) {
            $scope.sharedCategory = sharedCategory;


        }]);

})(angular);