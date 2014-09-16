angular.module('Starscream.Services').factory('accountService', function ($httpq) {

    return {
        Register: function (email, password, name, phoneNumber) {
            return $httpq.post("/register", { email: email, password: password, name: name, phoneNumber: phoneNumber });            
        },
        RegisterFacebook: function (payload) {
            return $httpq.post("/register/facebook",payload);
        },
        RequestToResetPassword: function (email) {
            return $httpq.post("/password/requestReset", { email: email });
        },
        ResetPassword: function (token, password) {
            return $httpq.put("/password/reset/" + token, { password: password });
        }
    };
});