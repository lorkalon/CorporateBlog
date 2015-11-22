(function (angular) {
    'use strict';

    angular.module("controllers").controller('LoginController', [
        '$scope',
        'accountService',
        '$location', function ($scope, accountService, $location) {
            $scope.loginForm = {
                login: '',
                password: ''
            };

            $scope.isUserLoggedIn = accountService.getAuthorizationData().isAuthorized;
            $scope.userName = accountService.getAuthorizationData().userName;

            $scope.logIn = function (loginForm) {
                accountService.logIn(loginForm).then(function () {
                    $location.path('/categories');
                });
            };

            $scope.logOut = function() {
                accountService.logOut();
                $scope.isUserLoggedIn = false;
                $scope.userName = '';
                $location.path('/login');
            };

            var redirectIfLoggedIn = function() {
                if ($scope.isUserLoggedIn) {
                    $location.path('/categories');
                }
            }

            redirectIfLoggedIn();
        }]);
})(angular);