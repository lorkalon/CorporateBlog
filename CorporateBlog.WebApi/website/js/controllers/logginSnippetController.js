(function (angular) {
    'use strict';

    angular.module("controllers").controller('LoginSnippetController', [
        '$scope',
        'accountService',
        '$location',
        'settings', function ($scope, accountService, $location, settings) {

            $scope.isUserLoggedIn = accountService.getAuthorizationData().isAuthorized;
            $scope.userName = accountService.getAuthorizationData().userName;
            $scope.isUserAdmin = $scope.isUserLoggedIn &&
                (accountService.getAuthorizationData().roleName === settings.adminRoleName);

            $scope.logOut = function () {
                accountService.logOut();
                $scope.isUserLoggedIn = false;
                $scope.userName = '';
                $location.path('/login');
            };
        }]);
})(angular);