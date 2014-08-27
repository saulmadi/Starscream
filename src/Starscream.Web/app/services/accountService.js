angular.module('Starscream.Services').factory('accountService', function ($httpq) {

    return {
        Register: function (email, password, name, phoneNumber) {
            return $httpq.post("/register", { email: email, password: password, name: name, phoneNumber: phoneNumber });            
        },
        resetPassword: function (email) {
            return $httpq.post("/reset-password", { email: email });
        }
    };
});