var app = angular.module('corporateBlog', []);

app.controller("AboutPersons", function($scope) {
	$scope.persons = [
		{
			name: "Hanna",
			about: "I am a girl",
			age:23
		},
		{
			name: "Misha",
			about: "I am a boy",
			age:17
		}
	];
});