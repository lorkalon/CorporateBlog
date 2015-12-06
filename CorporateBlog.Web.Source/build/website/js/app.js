(function (angular) {
    'use strict';

    var controllers = angular.module('controllers', []);
    var directives = angular.module('directives', []);
    var services = angular.module('services', ['LocalStorageModule']);
    var interceptors = angular.module('interceptors', []);
    var shared = angular.module('sharedData', []);
    var filters = angular.module('filters', []);
    var common = angular.module('common', []);

    var blogApp = angular.module('corporateBlogApplication', [
       'ngRoute',
       'controllers',
       'directives',
       'services',
       'ui.bootstrap',
       'interceptors',
       'sharedData',
       'filters',
       'common',
       'textAngular'
    ]);

    blogApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/categories', {
            templateUrl: '/website/views/categories.html',
            controller: 'CategoriesController'
        }).when('/createArticle', {
            templateUrl: '/website/views/createArticle.html',
            controller: 'CreateArticleController'
        }).when('/login', {
            templateUrl: '/website/views/login.html',
            controller: 'LoginController'
        }).when('/registration', {
            templateUrl: '/website/views/registration.html',
            controller: 'RegistrationController'
        }).when('/error', {
            templateUrl: '/website/views/error.html',
            controller: 'ErrorController'
        }).otherwise({
            redirectTo: "/login"
        });
    }]).config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('tokenInterceptor');
    }]).run([
            '$rootScope',
            '$location',
            'accountService', function ($rootScope, $location, accountService) {

                //$rootScope.$on("$routeChangeStart", function (event, next, current) {

                //    permissionsService.getPermissionByName(next.permission).then(function (permissionValue) {
                //        if (!authenticateService.checkPermission(permissionValue)) {
                //            $location.path("/auth");
                //            event.preventDefault();
                //        }
                //    });
                //});
            }]);
})(angular);

