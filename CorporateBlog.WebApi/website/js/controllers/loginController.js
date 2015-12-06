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

            $scope.logIn = function (loginForm) {
                accountService.logIn(loginForm).then(function () {
                    $location.path('/categories');
                });
            };

            var redirectIfLoggedIn = function() {
                if ($scope.isUserLoggedIn) {
                    $location.path('/categories');
                }
            }

            redirectIfLoggedIn();
        }]);
})(angular);