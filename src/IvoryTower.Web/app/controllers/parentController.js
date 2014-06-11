angular.module('ivoryTower.Controllers').controller('parentController', function ($scope, userService, $location) {
        var user = userService.GetUser();
        $scope.user = user;
        $scope.title = "Welcome";
        $scope.logout = function() {
            userService.RemoveUser();
            $scope.user = false;
            $location.path("/login");
        };
    });