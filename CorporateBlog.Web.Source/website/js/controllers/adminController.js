(function (angular, _) {
    'use strict';

    angular.module("controllers").controller('AdminController', [
        '$scope',
        'adminService',
        'accountService',
        '$location',
        'settings', function ($scope, adminService, accountService, $location, settings) {
            var authInfo = accountService.getAuthorizationData();

            if (!authInfo.isAuthorized) {
                $location.path("/#login");
            } else if (authInfo.roleName !== settings.adminRoleName) {
                $location.path("/#categories");
            }

            $scope.users = [];
            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.pageSize = 10;
            $scope.roles = [];
            $scope.alerts = [];

            var getUsers = function () {
                adminService.getUsersReport({
                    from: ($scope.currentPage - 1) * $scope.pageSize,
                    count: $scope.pageSize
                }).then(function (response) {
                    var data = response.data;
                    $scope.users = data.users;
                    $scope.totalCount = data.totalCount;

                    //_.map(data, function(user) {
                    //    return {
                    //        id: user.id,
                    //        userName: user.userName,
                    //        email: user.email,
                    //        blocked: user.blocked,
                    //        emailConfirmed: user.emailConfirmed,
                    //        roleId: user.roleId,
                    //        createdOn: user.createdOn
                    //    };
                    //});
                }, function () {
                    addAlert("danger", "Unable to load users.");
                });
            };

            $scope.updateUser = function (userInfo) {
                adminService.updateUser({
                    userId: userInfo.id,
                    roleId: userInfo.roleId,
                    emailConfirmed: userInfo.emailConfirmed,
                    blocked: userInfo.blocked
                }).then(function () {
                    addAlert("success", "User's been successfully updated!");
                }, function () {
                    addAlert("danger", "Unable to update user!");
                });
            };

            $scope.deleteUser = function (id) {
                adminService.deleteUser(id)
                    .then(function () {
                        addAlert("success", "User's been successfully deleted!");
                    }, function () {
                        addAlert("danger", "Unable to delet user!");
                    });
            };

            var initialize = function () {
                adminService.getRoles().then(function (response) {
                    $scope.roles = response.data;
                    getUsers();
                    $scope.$watch("currentPage", function () {
                        getUsers();
                    });
                }, function () {
                    addAlert("danger", "Unable to upload data!");
                });
            };

            var addAlert = function (type, message) {
                $scope.alerts.push({
                    type: type,
                    msg: message
                });
            };

            $scope.closeAlert = function (i) {
                $scope.alerts.splice(i, 1);
            };

            initialize();

        }]);
})(angular, _);