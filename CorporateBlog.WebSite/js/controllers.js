(function () {

    var controllers = angular.module('appControllers', []);

    controllers.controller("HomeController", ['$scope', 'PostsSevice', function ($scope, postsSevice) {
        postsSevice.getPosts().then(function (response) {
            var content = response;

            if (content.length == 0) {
                return;
            }

            $scope.selectedCategory = content[0].id;
            $scope.categories = content;
            $scope.posts = content[0].posts;
        });

        $scope.selectCategory = function (newCategory) {
            $scope.selectedCategory = newCategory.catgoryId;
            $scope.posts = newCategory.posts;
        };

        $scope.isCategorySelected = function (categoryId) {
            return $scope.selectedCategory == categoryId;
        }


        $scope.orderParam = 'date';


        $scope.switchOrderBy = function (sortBy) {
            $scope.orderParam = sortBy;
        }
       
    }]);


    controllers.controller("AccountController", function ($scope, $http) {

    });

    controllers.controller("PostDetailsController", ['$scope', '$routeParams', 'PostsSevice', function ($scope, $routeParams, postService) {
        $scope.postId = $routeParams.postId;
        $scope.post = {};

        postService.getPosts().then(function (response) {
            var sourceData = response;
            sourceData.forEach(function (category) {
                category.posts.forEach(function (p) {
                    if (p.id == $scope.postId) {
                        $scope.post = p;
                    }
                });
            });
        });

    }]);


    controllers.controller("ReviewPostController", ['$scope', function ($scope) {
        $scope.review = {
            'author': 'Hanna Shviatsova',
            'date': '11.03.2014'
        };

        $scope.addReview = function (post) {
            post.reviews.push($scope.review);
            console.log(post.title);
            $scope.review = {
                'author': 'Hanna Shviatsova',
                'date': '11.03.2014'
            };
        }
    }]);


    controllers.controller("NavbarController", ['$scope', function ($scope) {
        //Get name from service
        $scope.name = "John";
        $scope.surname = "Dow";
    }]);
})();