(function (angular) {
    'use strict';

    angular.module("controllers").controller('LoginSnippetController', [
        '$scope',
        'accountService',
        '$location', function ($scope, accountService, $location) {

            $scope.isUserLoggedIn = accountService.getAuthorizationData().isAuthorized;
            $scope.userName = accountService.getAuthorizationData().userName;

            $scope.logOut = function () {
                accountService.logOut();
                $scope.isUserLoggedIn = false;
                $scope.userName = '';
                $location.path('/login');
            };
        }]);
})(angular);