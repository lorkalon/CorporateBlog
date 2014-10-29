var controllers = angular.module('appControllers', []);

var sourceData = [];

controllers.controller("CategoriesListController", function ($scope, $http) {
    $http.get('../scripts/sources/categories.json').success(function (data) {
        sourceData = data.slice();
        $scope.categories = data;
        $scope.posts = data[0].posts;
        $scope.selectedCategoryName = data[0].title;
        $scope.orderParam = 'date';
    });


    $scope.selectCategory = function (newCategory) {
        $scope.selectedCategoryName = newCategory.title;
        $scope.posts = newCategory.posts;
    };

    $scope.isCategorySelected = function (category) {
        return $scope.selectedCategoryName == category.title;
    }

    $scope.switchOrderBy = function (sortBy) {
        $scope.orderParam = sortBy;
    }
});

controllers.controller("AccountController", function ($scope, $http) {

});

controllers.controller("PostDetailsController", ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.postId = $routeParams.postId;
    $scope.post = {};

    sourceData.forEach(function (category) {
        category.posts.forEach(function (p) {
            if (p.id == $scope.postId) {
                $scope.post = p;
            }
        });

        
    });
   
}]);
