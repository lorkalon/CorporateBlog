(function (angular) {
    'use strict';

    angular.module('services').factory('adminService', ['$http', function ($http) {
            var getUsersReport = function(model) {
                return $http({
                    url: "/api/Admin/GetUsersReport",
                    method: "GET",
                    params: model
                });
            };

            var updateUser = function(model) {
                return $http({
                    url: "/api/Admin/UpdateUser",
                    method: "PUT",
                    data: model
                });
            };

            var deleteUser = function(id) {
                return $http({
                    url: "/api/Admin/DeleteUser/" + id,
                    method: "DELETE"
                });
            };

            var getRoles = function() {
                return $http({
                    url: "/api/Admin/GetRoles",
                    method: "GET"
                });
            };

        return {
            getUsersReport: getUsersReport,
            updateUser: updateUser,
            deleteUser: deleteUser,
            getRoles: getRoles
        };
    }
    ]);
})(angular);