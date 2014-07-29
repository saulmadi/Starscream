angular.module('Starscream.Controllers').controller('homeController', function ($scope, userService, $location) {
    if (!userService.GetUser()) $location.path("/login");
    $scope.$parent.title = "Home";
});