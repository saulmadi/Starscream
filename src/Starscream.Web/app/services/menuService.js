﻿angular.module('Starscream.Services').factory('menuService', function () {
    'use strict';

    return {
        menu: [
            {
                display: 'Admin',
                features: [
                    {
                        name: 'ActivateDeactivateUsers',
                        display: 'User Management',
                        route: '/activate-deactivate-users'
                    }
                ]
            },
            {
                display: 'General',
                features: [
                    {
                        name: 'Home',
                        display: 'Go to Home',
                        route: '/home'
                    }
                ]
            }
        ],

        getFeatures: function() {
            var features = [];
            this.menu.forEach(function (value, index, array) {
                var tmpFeature = features.concat(value.features);
                features = tmpFeature;
            });

            return features;
        }


    };
});