angular.module('Starscream.Services').factory('homeService', function ($httpq) {

    return {
        GetUsers: function (payload) {
            return $httpq.get("/users?" + "PageNumber=" + payload.PageNumber + "&PageSize=" + payload.PageSize + "&Field=" + payload.Field);
        }
    };
});