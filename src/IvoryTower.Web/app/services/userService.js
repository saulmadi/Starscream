angular.module('ivoryTower.Services').factory('userService', function () {

    var key = "user";

    return {
        SetUser: function (email, name, token, remember) {
            var user = {
                email: email,
                name: name,
                token: token
            };
            
            var userString = JSON.stringify(user);

            if (remember) {
                window.localStorage.setItem(key, userString);
            } else {
                window.sessionStorage.setItem(key, userString);
            }            
        },
        GetUser: function () {
            var userFromLocalStorage = window.localStorage.getItem(key);
            if (userFromLocalStorage) {
                return JSON.parse(userFromLocalStorage);
            } else {
                var userFromSession = window.sessionStorage.getItem(key);
                if (!userFromSession) return false;
                return JSON.parse(userFromSession);
            }

        },
        RemoveUser: function () {
            window.sessionStorage.removeItem("user");
            window.localStorage.removeItem("user");
        },
        Register : function(email, password, name, phoneNumber) {
            $http
        }
    };
});