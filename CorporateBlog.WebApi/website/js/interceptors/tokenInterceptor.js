(function (angular) {
    'use strict';

    angular.module("interceptors").factory('tokenInterceptor', [
        '$q',
        '$injector',
        '$location',
        'localStorageService', function ($q, $injector, $location, localStorageService) {
       
            var request = function (config) {

                config.headers = config.headers || {};

                var authData = localStorageService.get('authorizationData');

                if (authData && authData.token) {
                    config.headers.Authorization = 'Bearer ' + authData.token;
                }

                return config;
            };

            var responseError = function (rejection) {
                if (rejection.status === 401) {
                    var authService = $injector.get('accountService');
                    authService.logOut();
                    $location.path('/login');
                }

                return $q.reject(rejection);
            };

            return {
                request: request,
                responseError: responseError
            };

    }]);

})(angular);