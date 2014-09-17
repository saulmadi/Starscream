angular.module('Starscream.Services').factory('googleService', function ($q, $httpq, loginService, accountService) {
    
    return {
        Register: function () {
            var def = $q.defer();
            gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: false }, function (authResult) {
                if (authResult && !authResult.error) {
                    gapi.client.load('plus', 'v1', function() {
                        var request = gapi.client.plus.people.get({
                            'userId': 'me'
                        });
                        request.execute(function (resp) {
                            resp["email"] = resp.emails[0].value;
                            accountService.RegisterGoogle(resp).then(function() {
                                def.resolve();
                            }).catch(function() {
                                def.reject();
                            });
                        });
                    });
                } else {
                    def.reject();
                }
            });
            return def.promise;
        },
        Login: function () {
            var def = $q.defer();
            gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: false }, function(authResult) {
                if (authResult && !authResult.error) {
                    gapi.client.load('plus', 'v1', function() {
                        var request = gapi.client.plus.people.get({
                            'userId': 'me'
                        });
                        request.execute(function (response) {
                            response["email"] = response.emails[0].value;
                            loginService.LoginGoogle({ Id: response.id, Email: response.email }).then(function(data) {
                                def.resolve(data);
                            }).catch(function(error) {
                                def.reject(error);
                            });
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