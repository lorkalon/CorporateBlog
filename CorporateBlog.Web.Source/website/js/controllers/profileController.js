(function (angular) {
    'use strict';

    angular.module("controllers").controller('ProfileController', [
        '$scope',
        'accountService',
        '$location',
        'Upload',
        function ($scope, accountService, $location, Upload) {
            $scope.myProfile = {
                email: '',
                userName: '',
                name: '',
                surname: '',
                roleName: '',
            };

            $scope.avatar = null;

            $scope.showedAvatar = {
                src: "",
                saved: false
            };

            $scope.newPasswordModel = {
                oldPassword: null,
                newPassword: null
            };

            $scope.alert = {
                type: null,
                msg: null,
                displayed: false
            };

            $scope.uploadPicture = function (file) {

                file.upload = Upload.upload({
                    url: '/api/Account/SaveUserPicture',
                    data: { file: file }
                });

                file.upload.then(function () {
                    $scope.showedAvatar.saved = true;
                    $scope.alert.type = "success";
                    $scope.alert.msg = "Avatar's been successfully saved!";
                    $scope.alert.displayed = true;
                }, function() {
                    $scope.alert.type = "error";
                    $scope.alert.msg = "Internal server error!";
                    $scope.alert.displayed = true;
                });
            };

            $scope.deleteAvatar = function () {
                $scope.showedAvatar.src = "";

                if ($scope.showedAvatar.saved) {
                    accountService.deleteProfilePicture();
                    $scope.avatar = null;
                }
            };

            $scope.changePassword = function(model) {
                accountService.changePassword(model).then(function() {
                    $scope.newPasswordModel = {
                        oldPassword: null,
                        newPassword: null
                    };

                    $scope.alert.type = "success";
                    $scope.alert.msg = "Password's been successfully changed!";
                    $scope.alert.displayed = true;
                }, function() {
                    $scope.alert.type = "error";
                    $scope.alert.msg = "Internal server error!";
                    $scope.alert.displayed = true;
                });
            };

            $scope.closeAlert = function() {
                $scope.alert.type = null;
                $scope.alert.msg = null;
                $scope.alert.displayed = false;
            };

            loadMyProfile();

            function loadMyProfile() {
                accountService.getMyProfileInfo().then(function (response) {
                    var profile = response.data;
                    $scope.myProfile.userName = profile.userName;
                    $scope.myProfile.email = profile.email;
                    $scope.myProfile.roleName = profile.role.name;

                    if (profile.userInfo) {
                        $scope.myProfile.name =  profile.userInfo.name;
                        $scope.myProfile.surname = profile.userInfo.surname;

                        if (profile.userInfo.avatar) {
                            $scope.showedAvatar.src = profile.userInfo.avatar;
                            $scope.showedAvatar.saved = true;
                        }
                    }

                });
            }


        }]);

})(angular);