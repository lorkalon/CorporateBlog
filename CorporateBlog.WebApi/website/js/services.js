(function () {
    var services = angular.module("appServices", []);

    services.factory("PostsSevice", ['$http', 'PostsCacheService', '$q', function ($http, PostsCacheService, $q) {

        function cacheData() {
            return $http.get('../website-build/sources/content.json').success(function (response) {
                console.log("downloaded from server");
                PostsCacheService.put('allPosts', response);
             }).then(function (response) { return response.data; });
        }

        function getPosts() {
            var data = PostsCacheService.get('allPosts');
            return $q.when(!data ? cacheData() : data);
        }

        function getCategories() {

        }

        return {
            getPosts: getPosts
        };
    }]);


    services.factory("PostsCacheService", ['$cacheFactory', function ($cacheFactory) {
        var cache = $cacheFactory('postsCache', {});
        return cache;
    }]);

})();