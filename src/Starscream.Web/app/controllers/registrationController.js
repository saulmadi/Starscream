angular.module('Starscream.Controllers').controller('registrationController', function ($scope, $location, accountService, facebookService, googleService) {

    $scope.user = { };

    $scope.$parent.title = "Registration";

    $scope.cancel = function () {
        $location.path("/login");
        return false;
    };

    var password1 = document.getElementById('password1');
    var password2 = document.getElementById('password2');

    var checkPasswordValidity = function () {
        if ($scope.user.password != $scope.user.passwordConfirm) {
            password1.setCustomValidity('Passwords must match.');
        } else {
            password1.setCustomValidity('');
        }
            
        if($scope.user.password.length<8) {
            password1.setCustomValidity('Passwords must be at least 8 characters long.');
        }
        else {
            password1.setCustomValidity('');
        }
    };

    password1.addEventListener('change', checkPasswordValidity, false);
    password2.addEventListener('change', checkPasswordValidity, false);
        
    $scope.register = function () {
            
        $scope.registered = false;
            
        accountService.Register($scope.user.email, $scope.user.password, $scope.user.name, $scope.user.phoneNumber)
            .then(function() {
                $scope.registered = true;
            }).catch(function(err1, err2, err3) {
                debugger;
            });
    };
    
    $scope.registerFacebook = function () {
        facebookService.Register().then(function () {
            $scope.registered = true;
        }).catch(function () {
            debugger;
        });
    };

    $scope.registerGoogle = function () {
        googleService.Register().then(function () {
            $scope.registered = true;
        }).catch(function() {
            debugger;
        });
    };
});