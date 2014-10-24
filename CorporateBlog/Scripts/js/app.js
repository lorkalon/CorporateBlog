var app = angular.module('corporateBlog', []);

app.controller("CategoriesList", function($scope, $http) {
	$http.get('/Scripts/js/sources/categories.json').success(function (data) {
		$scope.categories = data;
		$scope.posts = data[0].posts;
	});

	$scope.changeCategory = function(selectedCategory) {
		$scope.posts = selectedCategory.posts;
	};
});







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

