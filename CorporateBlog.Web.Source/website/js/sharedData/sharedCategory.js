(function(angular) {
    'use strict';

    angular.module('sharedData').factory('sharedCategory', [function () {

            var category = null;

            return {
                getCategory: function() {
                    return category;
                },
                setCategory: function(value) {
                    category = value;
                }
            };
        }
    ]);

})(angular);