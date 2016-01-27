(function (angular) {
    'use strict';

    angular.module("controllers").controller('RestorePasswordController', [
        '$scope',
        'accountService',
        '$location',
        function ($scope, accountService, $location) {

            $scope.isUserLoggedIn = accountService.getAuthorizationData().isAuthorized;
            $scope.emailSentNotification = false;
            $scope.responseError = null;

            $scope.email = null;
            $scope.sendResetPasswordEmail = function(email) {
                accountService.sendRestoreEmail(email).then(function() {
                    $scope.emailSentNotification = true;
                    $scope.responseError = null;
                }, function() {
                    $scope.responseError = "User with this email hasn't been registered.";
                });
            };


            var redirectIfLoggedIn = function () {
                if ($scope.isUserLoggedIn) {
                    $location.path('/categories');
                }
            }

            redirectIfLoggedIn();
        }]);
})(angular);