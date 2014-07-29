angular.module('Starscream.Services').factory('registrationService', function ($http, $q) {

    return {
        Register: function (email, password, name, phoneNumber) {
            var defer = $q.defer();
            $http.post("/register", { email: email, password: password, name: name, phoneNumber: phoneNumber })
                .success(function (data) {
                    defer.resolve(data);
                }).error(function (data) {
                    defer.reject(data);
                });
            return defer.promise;
        }
    };
});