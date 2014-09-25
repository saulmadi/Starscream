angular.module('Starscream.Services').factory('adminService', function ($httpq) {

    return {
        GetUsers: function (payload) {
            return $httpq.get("/users?" + "PageNumber=" + payload.PageNumber + "&PageSize=" + payload.PageSize + "&Field=" + payload.Field);
        },
        EnableUser: function (payload) {
            return $httpq.post("/users/enable", payload);
        }
    };
});