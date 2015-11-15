(function () {
    var blogApp = angular.module('corporateBlogApp', [
        'ngRoute',
        'appControllers',
        'directives',
        'appServices'
    ]);
  

    blogApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/login', {
            templateUrl: '/build/views/login.html',
            controller: 'LoginController'
            }).when('/home', {
            templateUrl: '/build/views/home.html',
            controller: 'HomeController'
        }).when('/account', {
            templateUrl: '/build/views/account.html',
            controller: 'AccountController'
        }).when('/posts/:postId', {
            templateUrl: '/build/views/postDetails.html',
            controller: 'PostDetailsController'
        }).otherwise({
            redirectTo: "/login"
        });
    }]);
})();

