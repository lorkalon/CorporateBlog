(function (angular) {
    'use strict';

    angular.module("controllers").controller('ResetPasswordController', [
        '$scope',
        'accountService',
        '$location',
        '$routeParams',
        function ($scope, accountService, $location, $routeParams) {
            var code = $routeParams.code;
            var email = $routeParams.email;

            $scope.newPassword = null;
            $scope.resetFailedMessage = null;

            $scope.saveNewPassword = function(password) {
                accountService.resetPassword({
                    email: email,
                    code: code,
                    password: password
                }).then(function() {
                    $location.path("/login");
                }, function(error) {
                    $scope.resetFailedMessage = error.data;
                });
            };
        }]);

})(angular);