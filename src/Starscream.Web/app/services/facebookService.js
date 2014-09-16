angular.module('Starscream.Services').factory('facebookService', function ($q, $httpq, accountService) {

    return {
        Register: function () {
            var def = $q.defer();
            FB.login(function (response) {
                if (response.authResponse) {
                    FB.api('/me', function (response) {
                        accountService.RegisterFacebook(response).then(function () {
                            def.resolve();
                        }).catch(function () {
                            def.reject();
                        });
                    });
                } else {
                    def.reject();
                }
            });
            return def.promise;
        }
    };
});