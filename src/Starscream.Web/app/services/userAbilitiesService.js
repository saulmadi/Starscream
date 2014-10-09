angular.module('Starscream.Services').factory('userAbilitiesService', function ($httpq) {

    return {
        GetAbilities: function () {
            return $httpq.get('/abilities');
        },
        AddAbilities: function (payload) {
            var response = $httpq.post('/user/abilites', payload);
            return response;
        }
    };
});