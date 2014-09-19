angular.module('Starscream.Controllers').controller('homeController', function ($scope, userService, $location, homeService) {
    if (!userService.GetUser()) $location.path("/login");
    $scope.$parent.title = "Home";
    $scope.users = [];
    $scope.paginationPayload = { PageNumber: 1, PageSize: 20, Field: "Name" };

    $scope.GetUsers = function (payload) {
        homeService.GetUsers(payload).then(function(data) {
            $scope.users = data.adminUsers;
        });
    };

    $scope.next = function() {
        $scope.paginationPayload.PageNumber += 1;
        $scope.GetUsers($scope.paginationPayload);
    };

    $scope.back = function () {
        $scope.paginationPayload.PageNumber -= 1;
        $scope.GetUsers($scope.paginationPayload);
    };

    $scope.sort = function(param) {
        $scope.paginationPayload.Field = param;
        $scope.GetUsers($scope.paginationPayload);
    };
    
    $scope.EnableUser = function(payload) {
        homeService.EnableUser(payload).then(function() {
            $scope.GetUsers($scope.paginationPayload);
        });
    };
    
    $scope.GetUsers($scope.paginationPayload);
});