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

                $http.post('/api/token', data, {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }).success(function (response) {
                    authenticationData.token = response.access_token,
                    authenticationData.isAuthorized = true;
                    authenticationData.userName = loginData.login;
                    localStorageService.set('authorizationData', authenticationData);
                    deferred.resolve(response);

                }).error(function (err, status) {
                    logOut();
                    deferred.reject(err);
                });

                return deferred.promise;
            };

            return {
                logIn: logIn,
                logOut: logOut
            };

        }]);

})(angular);