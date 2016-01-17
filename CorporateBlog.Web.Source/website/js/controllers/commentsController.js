(function (angular, moment) {
    'use strict';

    angular.module("controllers").controller('CommentsController', [
        '$scope',
        '$routeParams',
        '$q',
        'settings',
        'commentsService', function ($scope, $routeParams, $q, settings, commentsService) {
            var articleId = $routeParams.id;
            var from = 0;
            var count = settings.commentsRangeLimit;
            var dateFormat = "MM/DD/YYYY hh:mm A";

            $scope.isLoadMoreAvailable = true;
            $scope.loadMoreDisabled = false;
            $scope.commentBody = "";
            $scope.comments = [];

            $scope.loadComments = function () {
                $scope.loadMoreDisabled = true;
                commentsService.getComments({
                    articleId: articleId,
                    from: from,
                    count: count
                }).then(function(response) {
                    if (response.data.length === 0) {
                        $scope.isLoadMoreAvailable = false;
                    } else {
                        from += count;
                        var mapped = _.map(response.data, function(cm) {
                            cm.formattedDate = moment.utc(cm.createdOnUtc).local().format(dateFormat);
                        });

                        $scope.comments.push.apply($scope.comments, response.data);
                    }

                    $scope.loadMoreDisabled = false;
                });
            };

            $scope.addComment = function(comment) {
                commentsService.addComment({
                    text: comment,
                    articleId: articleId
                }).then(function() {
                    from = 0;
                    $scope.comments = [];
                    $scope.loadComments();
                    $scope.commentBody = "";
                });
            };

            $scope.deleteComment = function(id) {
                commentsService.deleteComment(id).then(function() {
                    from = 0;
                    $scope.comments = [];
                    $scope.loadComments();
                });
            };

            $scope.voteForComment = function(comment, rate) {
                commentsService.voteForComment({
                    commentId: comment.id,
                    value: rate
                }).then(function() {
                    comment.rate += rate;
                    comment.userVotedRate = rate;
                });
            };

            $scope.loadComments();

        }]);
})(angular, moment);