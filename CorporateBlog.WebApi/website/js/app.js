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
       'textAngular',
       'ngFileUpload'

    ]);

    blogApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/categories', {
            templateUrl: '/website/views/categories.html',
            controller: 'CategoriesController'
        }).when('/articles/update/:id', {
            templateUrl: '/website/views/addOrUpdateArticle.html',
            controller: 'AddOrUpdateArticleController'
        }).when('/articles/add', {
            templateUrl: '/website/views/addOrUpdateArticle.html',
            controller: 'AddOrUpdateArticleController'
        }).when('/articles/:id', {
            templateUrl: '/website/views/showArticle.html',
            controller: 'ShowArticleController'
        }).when('/login', {
            templateUrl: '/website/views/login.html',
            controller: 'LoginController'
        }).when('/registration', {
            templateUrl: '/website/views/registration.html',
            controller: 'RegistrationController'
        }).when('/profile', {
            templateUrl: '/website/views/profile.html',
            controller: 'ProfileController'
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
            }]);
})(angular);

