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

        $scope.logIn = function(loginForm) {
            accountService.logIn(loginForm).then(function() {
                $location.path('/articles');
            });
        };
    }]);

})(angular);