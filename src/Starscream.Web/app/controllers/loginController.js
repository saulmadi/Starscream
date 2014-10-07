angular.module('Starscream.Controllers').controller('loginController', function ($scope, $location, accountService, loginService, userService, facebookService, googleService) {
        
    if (userService.GetUser()) {
        $location.path("/");
    }

    $scope.user = {};
    $scope.$parent.title = "Login";


    var setUserSession = function(data) {
        userService.SetUser($scope.user.email, data.name, data.token, $scope.rememberMe, data.expires,data.claims);
        $scope.$parent.user = userService.GetUser();
        $location.path("/");
    };
    
    $scope.login = function () {
        $scope.error = "";

        loginService.Login($scope.user.email, $scope.user.password).then(function (data) {
            loginService.SetLoggedIn(true);
            setUserSession(data);
        }).catch(function (error) {
            loginService.SetLoggedIn(false);
            $scope.error = error;
        });
    };
    
    $scope.loginFacebook = function() {
        facebookService.Login().then(function (data) {
            loginService.SetLoggedIn(true);
            setUserSession(data);
        }).catch(function (error) {
            loginService.SetLoggedIn(false);
            $scope.error = error;
        });
    };

    $scope.loginGoogle = function () {
        googleService.Login().then(function (data) {
            loginService.SetLoggedIn(true);
            setUserSession(data);
        }).catch(function (error) {
            loginService.SetLoggedIn(false);
            $scope.error = error;
        });
    };
    
});