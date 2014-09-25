angular.module('Starscream.Controllers').controller('profileController', function ($scope, $location, $routeParams, adminService) {
    $scope.userId = $routeParams.userId;
    $scope.user = null;
    $scope.name = "";
    $scope.email = "";
    
    var getUserInfo = function() {
        adminService.GetUser($scope.userId).then(function(data) {
            $scope.user = data;
        }).catch(function() {
            console.error("error");
        });
    };

    $scope.updateProfile = function() {
        adminService.UpdateProfile({ Id: $scope.userId, Email: $scope.email, Name: $scope.name }).then(function() {
            console.log("success");
        });
    };

    getUserInfo();
});