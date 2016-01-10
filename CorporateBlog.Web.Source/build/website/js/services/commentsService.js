(function (angular) {
    'use strict';

    angular.module('services').factory('commentsService', ['$http', function ($http) {
        var addComment = function (model) {
            return $http.post("api/Comment/Add", model);
        };

        var deleteComment = function (id) {
            return $http({
                url: "api/Comment/Delete/" + id,
                method: "DELETE"
            });
        };

        var getComments = function (model) {
            return $http({
                url: "api/Comment/GetByFilter",
                method: "GET",
                params: model
            });
        };

        return {
            addComment: addComment,
            deleteComment: deleteComment,
            getComments: getComments
        };
    }
    ]);
})(angular);