(function (angular) {
    'use strict';

    angular.module('filters').filter('cut', function () {
        return function (text, max, tail) {
            if (!text) return '';

            max = parseInt(max, 10);

            if (!max) return text;

            if (text.length <= max) return text;

            text = text.substr(0, max);

            return text + (tail || '…');
        };
    });

})(angular);