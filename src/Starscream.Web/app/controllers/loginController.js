angular.module('Starscream.Controllers').controller('loginController', [
    '$scope', '$location', 'loginService', 'userService', function ($scope, $location, loginService, userService) {
        
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
    }
]);