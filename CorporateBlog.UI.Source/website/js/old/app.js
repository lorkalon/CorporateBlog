(function () {
    var blogApp = angular.module('corporateBlogApp', [
        'ngRoute',
        'appControllers',
        'directives',
        'appServices'
    ]);

    blogApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/home', {
            templateUrl: '/website-build/views/home.html',
            controller: 'HomeController'
        }).
        when('/account', {
            templateUrl: '/website-build/views/account.html',
            controller: 'AccountController'
        }).
        when('/posts/:postId', {
            templateUrl: '/website-build/views/postDetails.html',
            controller: 'PostDetailsController'
        }).
        otherwise({
            redirectTo: "/home"
        })
    }]);
})();

