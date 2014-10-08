'use strict';
var app = angular.module('Starscream', ['ng', 'ngRoute', 'Starscream.Controllers', 'Starscream.Services', 'Starscream.Directives']);


angular.module('Starscream.Controllers', []);
angular.module('Starscream.Services', []);
angular.module('Starscream.Directives', []);

app.config(function($routeProvider) {
        $routeProvider
            .when('/login', {
                templateUrl: 'app/views/login.html',
                controller: 'loginController'
            })
            .when('/forgot-password', {
                templateUrl: 'app/views/forgot-password.html',
                controller: 'forgotPasswordController'
            })
            .when('/reset-password', {
                templateUrl: 'app/views/reset-password.html',
                controller: 'resetPasswordController'
            })
            .when('/register', {
                templateUrl: 'app/views/registration.html',
                controller: 'registrationController'
            })
            .when('/', {
                templateUrl: 'app/views/home.html',
                controller: 'homeController'
            })
            .when('/activate-deactivate-users', {
                templateUrl: 'app/views/activate-deactivateUsers.html',
                controller: 'homeController'
            
                 }

            )
            .when('/404', {
                templateUrl: 'App/Views/404.html'
            })
            .when('/profile/:userId', {
                templateUrl: 'app/views/profile.html',
                controller: 'profileController'
            })
            .otherwise({
                redirectTo: '/404'
            });
    })
    .config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push([
                '$q', '$location', 'userService', function($q, $location, userService) {
                    return {
                        'responseError': function(rejection) {
                            //log here
                            if (rejection.status == 401) {
                                console.log("Not logged in. Redirecting to login page.");
                                userService.RemoveUser();
                                $location.path('/login');
                            }
                            return $q.reject(rejection);
                        }
                    };
                }
            ]);
        }
    ])
    .config([
        '$httpProvider', function ($httpProvider) {

            $httpProvider.interceptors.push([
                '$q', function ($q) {

                    var logRejection = function (rejection) {
                        LE.error({
                            method: rejection.config.method,
                            url: rejection.config.url,
                            data: rejection.config.data,
                            headers: rejection.config.headers,
                            status: rejection.status,
                            statusText: rejection.statusText
                        });
                        return $q.reject(rejection);
                    };

                    return {
                        'request': function(config) {
                            LE.info(config);
                            return config || $q.when(config);
                        },
                        'responseError': logRejection,
                        'requestError': logRejection
                    };
                }
            ]);
        }
    ])
    
    .config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push([
                '$q', 'userService', function($q, userService) {
                    return {
                        'request': function(config) {

                            console.log(config.method + " " + config.url);

                            var user = userService.GetUser();
                            if (user) {
                           
                                config.headers["Authorization"] = 'Bearer ' + user.token;

                            }
                            if (config.method == "POST" || config.method == "PUT") {
                                console.log(JSON.stringify(config.data));
                            }
                            return config || $q.when(config);
                        },
                        'requestError': function(rejection) {

                            //log here
                            console.log("RequestError: " + JSON.stringify(rejection));
                            return $q.reject(rejection);
                        }
                    };
                }
            ]);
        }
    ])
    .run(function ($rootScope, $location, loginService, userService, featureRoutesService) {
        var routesThatDontRequireAuth = ['/login'];
        
        var routesThatRequireRole = featureRoutesService.features;


        var routeClean = function (route) {
           return  routesThatDontRequireAuth.some(function(value, index, array) {

                return route.substr(0, value.length) === value;
            });

            
        };

        var routeWithRoles = function (route, claims) {
            var routeFeauture;
            var routeFound = routesThatRequireRole.some(function(value) {
                routeFeauture = value.name;
                return route.substr(0, value.length) === value.route;
            });
            if (routeFound) {
                claims.some(function(value) {
                    return value === routeFeauture;
                });
            }
            return false;
        };

        

        $rootScope.$on('$routeChangeStart', function (event, next, current) {

            var features = featureRoutesService.features;
            var user = userService.GetUser();

            // if route requires auth and user is not logged in


            if (!routeClean($location.url()) && !loginService.GetLoggedIn()) {
                // redirect back to login
             
                $location.path('/login');
            } else {
                
                if (!routeWithRoles($location.url(), user.claims) && loginService.GetLoggedIn()) {
                  
                    $location.path('/login');
                }

            }
        });
        
    })
;