angular.module('Starscream.Controllers').controller('loginController', function ($scope, $location, loginService, userService, facebookService, googleService) {
        
    if (userService.GetUser()) {
        $location.path("/");
    }

    $scope.user = {};
    $scope.$parent.title = "Login";

    $scope.login = function () {
        $scope.error = "";
        loginService.Login($scope.user.email, $scope.user.password).then(function (data) {
            userService.SetUser($scope.user.email, data.name, data.token, $scope.rememberMe);
            $scope.$parent.user = userService.GetUser();
            $location.path("/");
        }).catch(function (data) {
            $scope.error = "Invalid email address or password. Please try again.";
        });
    };
    
    $scope.loginFacebook = function() {
        facebookService.Login().then(function (data) {
            userService.SetUser($scope.user.email, data.name, data.token, $scope.rememberMe);
            $scope.$parent.user = userService.GetUser();
            $location.path("/");
        }).catch(function () {
            $scope.error = "Invalid facebook user, you need to register first.";
        });
    };

    $scope.loginGoogle = function() {
    };

});