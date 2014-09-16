angular.module('Starscream.Services').factory('googleService', function ($httpq) {

    return {
        Login: function () {
            function handleAuthResult(authResult) {
                if (authResult && !authResult.error) {
                    makeApiCall();
                }
            }
            
            function makeApiCall() {
                gapi.client.load('plus', 'v1', function () {
                    var request = gapi.client.plus.people.get({
                        'userId': 'me'
                    });
                    request.execute(function (resp) {
                        console.log(resp);
                    });
                });
            }
            
            gapi.auth.authorize({ client_id: clientId, scope: scopes, immediate: false }, handleAuthResult);
            return false;
        }
        

    };
});