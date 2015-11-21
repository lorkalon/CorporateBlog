(function (angular) {
    'use strict';

    var controllers = angular.module('controllers', []);
    var directives = angular.module('directives', []);
    var services = angular.module('services', ['LocalStorageModule']);

    var blogApp = angular.module('corporateBlogApplication', [
       'ngRoute',
       'controllers',
       'directives',
       'services',
       'ui.bootstrap',

    ]);

    blogApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/articles', {
            templateUrl: '/website/views/articles.html',
            controller: 'ArticlesController'
        }).when('/login', {
            templateUrl: '/website/views/login.html',
            controller: 'LoginController'
        }).when('/registration', {
            templateUrl: '/website/views/registration.html',
            controller: 'RegistrationController'
        }).otherwise({
            redirectTo: "/login"
        });
    }]);
})(angular);

