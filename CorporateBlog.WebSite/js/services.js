(function () {
    var services = angular.module("appServices", []);

    services.factory("postsSevice", function ($http) {
        var getPosts = $http.get('../sources/categories.json');
            

        return {
            getPosts: getPosts
        };
    })
})();