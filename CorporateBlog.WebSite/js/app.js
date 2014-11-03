(function () {
    var blogApp = angular.module('corporateBlogApp', [
        'ngRoute',
        'appControllers',
        'directives',
        'appServices'
    ]);

    blogApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/home', {
            templateUrl: 'views/home.html',
            controller: 'HomeController'
        }).
        when('/account', {
            templateUrl: 'views/account.html',
            controller: 'AccountController'
        }).
        when('/posts/:postId', {
            templateUrl: 'views/postDetails.html',
            controller: 'PostDetailsController'
        }).
        otherwise({
            redirectTo: "/home"
        })
    }]);
})();

