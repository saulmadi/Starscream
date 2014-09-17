angular.module('Starscream.Services').factory('loginService', function($httpq) {

    return {
        Login: function(email, password) {
            return $httpq.post("/login", { email: email, password: password });
        },
        LoginFacebook: function (payload) {
            return $httpq.post("/login/facebook", payload);
        },
        LoginGoogle: function (payload) {
            return $httpq.post("/login/google", payload);
        }
    };
});