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

            $scope.uploadPicture = function (file) {

                file.upload = Upload.upload({
                    url: '/api/Account/SaveUserPicture',
                    data: { file: file }
                });

                file.upload.then(function () {
                    $scope.showedAvatar.saved = true;
                });
            };

            $scope.deleteAvatar = function () {
                $scope.showedAvatar.src = "";

                if ($scope.showedAvatar.saved) {
                    accountService.deleteProfilePicture();
                    $scope.avatar = null;
                }
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