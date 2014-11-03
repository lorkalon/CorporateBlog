(function () {
    var directives = angular.module("directives", []);

    directives.directive('navbar', function () {
        return {
            restrict: 'E',
            templateUrl: '../partials/general/navbar.html',
            controller: 'NavbarController'
        };
    });

    directives.directive('categories', function () {
        return {
            restrict: 'E',
            templateUrl: '../partials/home/categories.html'
        };
    });

    directives.directive('posts', function () {
        return {
            restrict: 'E',
            templateUrl: '../partials/home/posts.html'
        };
    });

    directives.directive('advancedsearch', function () {
        return {
            restrict: 'E',
            templateUrl: '../partials/home/advancedSearch.html'
        }
    });

})();