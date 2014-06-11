angular.module('ivoryTower.Services').factory('loginService', ['$http', '$q', function ($http, $q) {

    return {
        Login: function (email, password) {
            var defer = $q.defer();
            $http.post("/login", { email: email, password: password })
              .success(function (data) {
                  defer.resolve(data);
              }).error(function (data) {
                  defer.reject(data);
              });
            return defer.promise;
        }
    };
}]);