(function (angular) {
    'use strict';

    angular.module("controllers").controller('ErrorController', ['$scope', '$routeParams', function ($scope, $routeParams) {
        $scope.message = $routeParams.message;
    }]);

})(angular);