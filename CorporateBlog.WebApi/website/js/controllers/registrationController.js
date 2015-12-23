(function (angular) {
    'use strict';

    angular.module("controllers").controller('RegistrationController', [
        '$scope',
        'accountService',
        '$location',
        function ($scope, accountService, $location) {
            $scope.registrationForm = {
                userName: '',
                email: '',
                password: ''
            };

            $scope.confirmationMessageVisible = false;

            $scope.register = function(registrationData) {
                accountService.register(registrationData).success(function() {
                    $scope.confirmationMessageVisible = true;
                }).error(function(response) {
                    
                });
            };

            var redirectIfLoggedIn = function () {
                if (accountService.getAuthorizationData().isAuthorized) {
                    $location.path('/articles');
                }
            }

            redirectIfLoggedIn();
        }]);

})(angular);