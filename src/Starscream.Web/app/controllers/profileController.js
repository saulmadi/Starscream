angular.module('Starscream.Controllers').controller('profileController', function ($scope, $location, $routeParams, adminService) {
    $scope.userId = $routeParams.userId;
    
    var getUserInfo = function() {
        adminService.GetUser($scope.userId).then(function(data) {
            $scope.user = data;
            $scope.name = data.name;
            $scope.email = data.email;
            console.log(data);
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