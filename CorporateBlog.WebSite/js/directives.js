(function () {
    var directives = angular.module("directives", []);

    directives.directive('navbar', function () {
        return {
            restrict: 'E',
            templateUrl: '../partials/navbar.html',
            controller: 'NavbarController',
            controllerAs: 'navigation'
        };
    });

})();