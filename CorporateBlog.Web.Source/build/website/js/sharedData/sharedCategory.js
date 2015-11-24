(function(angular) {
    'use strict';

    angular.module('sharedData').factory('sharedCategory', [function () {
            return {
                selectedCategory: null
            };
        }
    ]);

})(angular);