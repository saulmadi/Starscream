angular.module('Starscream.Services').factory('featureRoutesService', function () {
    'use strict';

    return {
        features:[ {
            name: 'ActivateDeactivateUsers',
            route: '/activate-deactivate-users'
        },
        {
            name: 'Home',
            route: '/'
        }
        ]

    };
});