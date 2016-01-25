(function (angular) {
    'use strict';

    angular.module('services')
        .factory('accountService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
            var authenticationData = localStorageService.get('authorizationData') || {
                isAuthorized: false,
                userName: '',
                roleName: ''
            };

            var logOut = function () {
                localStorageService.remove('authorizationData');

                authenticationData.isAuthorized = false;
                authenticationData.userName = '';
                authenticationData.roleName = '';
                authenticationData.token = null;
            };

            var logIn = function (loginData) {
                var data = "grant_type=password&username=" + loginData.login + "&password=" + loginData.password,
                    deferred = $q.defer();

                $http.post('/api/Account/Login', data, {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }).success(function (response) {
                    authenticationData.token = response.access_token,
                    authenticationData.isAuthorized = true;
                    authenticationData.userName = response.userName;
                    authenticationData.roleName = response.role;
                    localStorageService.set('authorizationData', authenticationData);
                    deferred.resolve(response);

                }).error(function (err, status) {
                    logOut();
                    deferred.reject(err);
                });

                return deferred.promise;
            };

            var register = function (registrationData) {
                return $http.post('/api/Account/Register', registrationData);
            };

            var saveUserPicture = function (file) {
                return $http.post('/api/Account/SaveUserPicture', file);
            };

            var getMyProfileInfo = function () {
                return $http({
                    url: "/api/Account/GetMyProfileInfo",
                    method: "GET"
                });
            };

            var deleteProfilePicture = function () {
                return $http({
                    url: "/api/Account/DeleteUserPicture",
                    method: "DELETE"
                });
            };

            return {
                logIn: logIn,
                logOut: logOut,
                getAuthorizationData: function () {
                    return {
                        userName: authenticationData.userName,
                        isAuthorized: authenticationData.isAuthorized,
                        roleName: authenticationData.roleName
                    };
                },
                register: register,
                saveUserPicture: saveUserPicture,
                getMyProfileInfo: getMyProfileInfo,
                deleteProfilePicture: deleteProfilePicture
            };

        }]);

})(angular);