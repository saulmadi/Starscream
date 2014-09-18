angular.module('Starscream.Controllers').controller('homeController', function ($scope, userService, $location, homeService) {
    if (!userService.GetUser()) $location.path("/login");
    $scope.$parent.title = "Home";
    $scope.users = [];
    
    $scope.GetUsers = function(payload) {
        homeService.GetUsers(payload).then(function(data) {
            $scope.users = data.adminUsers;
        });
    };

    $scope.GetUsers({PageNumber: 0, PageSize: 20, Field: "Name"});
});