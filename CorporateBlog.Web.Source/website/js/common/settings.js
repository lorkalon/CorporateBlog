(function (angular) {
    'use strict';

    angular.module('common').factory('settings', [function () {
        return {
            adminRoleName: "Admin",
            publisherRoleName: "Publisher",
            clientRoleName: "Client"
        };
    }]);

})(angular);