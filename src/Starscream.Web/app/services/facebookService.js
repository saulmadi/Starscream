angular.module('Starscream.Services').factory('facebookService', function ($q, $httpq, loginService, accountService) {

    return {
        Register: function() {
            var def = $q.defer();
            FB.login(function(response) {
                if (response.authResponse) {
                    FB.api('/me', function(response) {
                        accountService.RegisterFacebook(response).then(function() {
                            def.resolve();
                        }).catch(function() {
                            def.reject();
                        });
                    });
                } else {
                    def.reject();
                }
            }, { scope: 'email' });
            return def.promise;
        },
        Login: function () {
            var def = $q.defer();
            FB.login(function (response) {
                if (response.authResponse) {
                    FB.api('/me', function (response) {
                        loginService.LoginFacebook({Id : response.id, Email: response.email}).then(function (data) {
                            def.resolve(data);
                        }).catch(function (error) {
                            def.reject(error);
                        });
                    });
                } else {
                    def.reject();
                }
            }, { scope: 'email'});
            return def.promise;
        }
    };
});