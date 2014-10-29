var blogApp = angular.module('corporateBlogApp', [
    'ngRoute',
    'appControllers'
]);

blogApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/categories', {
        templateUrl: 'partials/categories.html',
        controller: 'CategoriesListController'
    }).
    when('/account', {
        templateUrl: 'partials/account.html',
        controller:'AccountController'
    }).
    when('/posts/:postId', {
        templateUrl: 'partials/postDetails.html',
        controller: 'PostDetailsController'
    }).
    otherwise({
        redirectTo: "/categories"
    })
}]);




//app.controller("AboutPersons", function($scope) {
//	$scope.persons = [
//		{
//			name: "Hanna",
//			about: "I am a girl",
//			age:23
//		},
//		{
//			name: "Misha",
//			about: "I am a boy",
//			age:17
//		}
//	];
//});

//app.controller("Swich", function($scope) {
//	var text = "No text";
//	$scope.showOne = function() {
//		text = "One askjhf solfj solfsoki fpsef \n soefijs \n oefilmiekdidkd dkdkkdk";
//		$scope.paragraph = text;
//	};

//	$scope.showTwo = function() {
//		text = "Two dogfk poks ok toiw oirf ep0oi rep epr09 gep0";
//		$scope.paragraph = text;
//	};


//	$scope.paragraph = text;

//});

//var myModule = angular.module('myModule', []);

//app.factory("corporateService", function(name) {
//	return {
//		getNews:function() {
//			return "Some news about corporation " + name;
//		}
//	};
//});

//var injector = angular.injector('corporateBlog');

//app.controller("TestInjector", function($scope) {
//	var service = injector.get("corporateService");

//	$scope.testInjector = function () {
//		$scope.serviceResponse = service.getNews("Lalka");
//	};
//});

