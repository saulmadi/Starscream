angular.module('Starscream.Services').factory('facebookService', function ($httpq, accountService) {

    return {
        Register: function () {
            FB.login(function (response) {
                if (response.authResponse) {
                    FB.api('/me', function (response) {
                        accountService.RegisterFacebook(response);
                    });
                } else {
                    console.log('User cancelled login or did not fully authorize.');
                }
            });
        }
    };
});