(function (angular) {
    'use strict';

    angular.module("directives").directive('loginsnippet', function () {
        return {
            restrict: 'E',
            templateUrl: '/website/views/loginSnippet.html',
            controller: 'LoginSnippetController'
        };
    });

})(angular);