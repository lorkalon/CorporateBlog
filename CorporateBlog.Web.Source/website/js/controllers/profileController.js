(function(angular) {
    'use strict';

    angular.module("controllers").controller('ProfileController', [
        '$scope',
        'accountService',
        '$location',
        'Upload',
        function ($scope, accountService, $location, Upload) {
            var authData = accountService.getAuthorizationData();
            $scope.userName = authData.userName;
            $scope.userPicture = "";
            $scope.uploadPicture = function(data) {
                uploadUsingUpload(data);
            };

            function uploadUsingUpload(file) {
                file.upload = Upload.upload({
                    url: '/api/Account/SaveUserPicture',
                    data: { file: file }
                });
            }


        }]);

})(angular);