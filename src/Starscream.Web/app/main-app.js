(function() {
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
                .when('/home', {
                    templateUrl: 'app/views/home.html',
                    controller: 'homeController'
                })
                .when('/activate-deactivate-users', {
                    templateUrl: 'app/views/activate-deactivateUsers.html',
                    controller: 'homeController'
                })
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
            '$httpProvider',
            function($httpProvider) {
                $httpProvider.interceptors.push([
                    '$q', '$location', 'userService',
                    function($q, $location, userService) {
                        return {
                            'responseError': function(rejection) {
                                //log here

                                if (rejection.status === 401) {
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
            '$httpProvider',
            function($httpProvider) {

                $httpProvider.interceptors.push([
                    '$q',
                    function($q) {

                        var logRejection = function(rejection) {
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
            '$httpProvider',
            function($httpProvider) {
                $httpProvider.interceptors.push([
                    '$q', 'userService',
                    function($q, userService) {
                        return {
                            'request': function(config) {

                                console.log(config.method + " " + config.url);

                                var user = userService.GetUser();
                                if (user) {

                                    config.headers.Authorization = 'Bearer ' + user.token;

                                }
                                if (config.method === "POST" || config.method === "PUT") {
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
        .run(function($rootScope, $location, loginService, userService, menuService) {
            var routesThatDontRequireAuth = ['/login', '/forgot-password', '/reset-password', '/register'];

            var routesThatRequireRole = menuService.getFeatures();


            var routeClean = function(route) {
                return routesThatDontRequireAuth.some(function(value, index, array) {
                    return route.toString().indexOf(value) !== -1;
                });
            };

            var routeWithRoles = function(route) {
                return routesThatRequireRole.some(function(value) {
                    return route === value.route;
                });
            };

            var urlFeature = function(url, features) {

                var feauturesNames = features.filter(function(value) {
                    return url === value.route;
                });
                return feauturesNames[0];
            };

            var featureInUserClaims = function(feature, claims) {

                return claims.some(function(value) {
                    return value === feature;
                });
            };

            $rootScope.$on('$routeChangeStart', function(event, next, current) {

                var features = menuService.getFeatures(); //menuService.features;
                var userClaims = userService.GetUser().claims;
                var url = $location.url();

                // if route requires auth and user is not logged in

                var route = routeClean(url);
                var loggedUser = loginService.GetLoggedIn();


                if (!route && !loggedUser) {
                    // redirect back to login

                    event.preventDefault();
                    $location.path('/login');
                } else {

                    if (routeWithRoles(url)) {
                        var feature = urlFeature(url, features);
                        if (!featureInUserClaims(feature.name, userClaims)) {
                            event.preventDefault();
                            $location.path("/404");
                        }

                    }

                }
            });

        });
}());;(function() {
    'use strict';

    angular.module('Starscream.Controllers').controller('forgotPasswordController', ['$scope', '$location', 'userService', 'accountService', function($scope, $location, userService, accountService) {

        if (userService.GetUser()) {
            $location.path("/");
        }

        $scope.user = {};

        $scope.$parent.title = "Reset Password";

        $scope.resetPassword = function() {
            accountService.RequestToResetPassword($scope.user.email).then(function() {
                $scope.success = true;
            });
        };
    }]);
}());;(function() {
    'use strict';

    angular.module('Starscream.Controllers').controller('homeController', ['$scope', 'userService', '$location', 'adminService', function($scope, userService, $location, adminService) {
        var user = userService.GetUser();
        if (!user) {
            $location.path("/login");
        }

        $scope.$parent.title = "Home";
        $scope.users = [];
        $scope.paginationPayload = {
            PageNumber: 1,
            PageSize: 20,
            Field: "Name"
        };
        $scope.sortCriteria = "";


        $scope.GetUsers = function(payload) {
            adminService.GetUsers(payload).then(function(data) {
                $scope.users = data.adminUsers;
            });
        };

        $scope.next = function() {
            $scope.paginationPayload.PageNumber += 1;
            $scope.GetUsers($scope.paginationPayload);
        };

        $scope.back = function() {
            $scope.paginationPayload.PageNumber -= 1;
            $scope.GetUsers($scope.paginationPayload);
        };

        $scope.sort = function(param) {
            $scope.sortCriteria = param;
            $scope.paginationPayload.Field = param;
            $scope.GetUsers($scope.paginationPayload);
        };

        $scope.EnableUser = function(payload) {
            adminService.EnableUser(payload).then(function() {
                $scope.GetUsers($scope.paginationPayload);
            });
        };

        $scope.GetUsers($scope.paginationPayload);
    }]);
}());;(function() {
    'use strict';
    
    angular.module('Starscream.Controllers').controller('loginController', ['$scope', '$location', 'accountService', 'loginService', 'userService', 'facebookService', 'googleService', 'menuService', function($scope, $location, accountService, loginService, userService, facebookService, googleService, menuService) {

        if (userService.GetUser()) {
            $location.path("/home");
        }

        $scope.user = {};
        $scope.$parent.title = "Login";


        var setUserSession = function(data) {
            userService.SetUser($scope.user.email, data.name, data.token, $scope.rememberMe, data.expires, data.claims);
            $scope.$parent.user = userService.GetUser();
            $scope.$parent.menu = menuService.getMenuForUser($scope.$parent.user.claims || []);
            $location.path("/home");
        };

        $scope.login = function() {
            $scope.error = "";

            loginService.Login($scope.user.email, $scope.user.password).then(function(data) {
                loginService.SetLoggedIn(true);
                setUserSession(data);
            }).catch(function(error) {
                loginService.SetLoggedIn(false);
                $scope.error = error;
            });
        };

        $scope.loginFacebook = function() {
            facebookService.Login().then(function(data) {
                loginService.SetLoggedIn(true);
                setUserSession(data);
            }).catch(function(error) {
                loginService.SetLoggedIn(false);
                $scope.error = error;
            });
        };

        $scope.loginGoogle = function() {
            googleService.Login().then(function(data) {
                loginService.SetLoggedIn(true);
                setUserSession(data);
            }).catch(function(error) {
                loginService.SetLoggedIn(false);
                $scope.error = error;
            });
        };

    }]);
}());;(function(){
    'use strict';
    
    angular.module('Starscream.Controllers').controller('parentController', ['$scope', 'userService', '$location', 'loginService', 'menuService', function($scope, userService, $location, loginService, menuService) {
        var user = userService.GetUser();
        if (user) {
            loginService.SetLoggedIn(true);
        }

        $scope.userMenuBar = [];
        $scope.user = user;
        $scope.title = "Welcome";
        $scope.logout = function() {
            userService.RemoveUser();
            $scope.user = false;
            $location.path("/login");
            $scope.menu = [];
        };

        //  $scope.menu = menuService.menu;
        $scope.menu = menuService.getMenuForUser(user.claims || []);
    }]);
}());
;(function(){
    'use strict';
    
    angular.module('Starscream.Controllers').controller('profileController', ['$scope', '$location', '$routeParams', 'adminService', function($scope, $location, $routeParams, adminService) {
        $scope.userId = $routeParams.userId;
        $scope.success = false;
        $scope.saveChanges = false;
        var getUserInfo = function() {
            adminService.GetUser($scope.userId).then(function(data) {
                $scope.user = data;
                $scope.name = data.name;
                $scope.email = data.email;
                console.log(data);
            }).catch(function() {});
        };

        $scope.updateProfile = function() {
            adminService.UpdateProfile({
                Id: $scope.userId,
                Email: $scope.email,
                Name: $scope.name
            }).then(function() {
                $scope.success = true;
            });
        };

        getUserInfo();
    }]);
}());
;(function() {
    'use strict';

    angular.module('Starscream.Controllers').controller('registrationController',['$scope', '$location', 'accountService', 'userService', 'userAbilitiesService', function($scope, $location, accountService, userService, userAbilitiesService) {
        /*jshint -W087 */
        $('.multiselect').multiselect({
            includeSelectAllOption: true
        });
        $scope.user = {};
        $scope.myAbilities = [];
        $scope.abilities = [];

        $scope.$parent.title = "Registration";

        $scope.cancel = function() {
            $location.path("/login");
            return false;
        };

        userAbilitiesService.GetAbilities()
            .then(function(data) {
                $scope.abilities = data;
            });

        var password1 = document.getElementById('password1');
        var password2 = document.getElementById('password2');

        var checkPasswordValidity = function() {
            if ($scope.user.password !== $scope.user.passwordConfirm) {
                password1.setCustomValidity('Passwords must match.');
            } else {
                if ($scope.user.password.length < 8) {
                    password1.setCustomValidity('Passwords must be at least 8 characters long.');
                } else {
                    password1.setCustomValidity('');
                }
            }


        };

        password1.addEventListener('change', checkPasswordValidity, false);
        password2.addEventListener('change', checkPasswordValidity, false);

        $scope.register = function() {

            $scope.registered = false;

            accountService.Register($scope.user.email, $scope.user.password, $scope.user.name, $scope.user.phoneNumber, $scope.myAbilities)
                .then(function() {
                    $scope.registered = true;
                }).catch(function(err1, err2, err3) {
                    debugger;
                });
        };
    }]);
}());;(function() {
    'use strict';

    angular.module('Starscream.Controllers').controller('resetPasswordController', ['$scope', '$routeParams', '$location', 'userService', 'accountService', function($scope, $routeParams, $location, userService, accountService) {

        if (userService.GetUser()) {
            $location.path("/");
        }

        var token = $routeParams.token;
        if (!token) {
            $location.path("/404");
        }

        $scope.$parent.title = "Reset Password";

        $scope.resetPassword = function() {
            accountService.ResetPassword(token, $scope.password1).then(function() {
                $scope.success = true;
            });
        };
    }]);
}());;(function() {
    'use strict';
    angular.module('Starscream.Directives')
        .directive('multiselectDropdown', ['scope', 'element', 'attributes', function() {
            return function(scope, element, attributes) {

                element = $(element[0]); // Get the element as a jQuery element

                // Below setup the dropdown:

                element.multiselect({
                    buttonClass: 'btn btn-small',
                    buttonWidth: '200px',
                    buttonContainer: '<div class="btn-group" />',
                    maxHeight: 200,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    buttonText: function(options) {
                        if (options.length === 0) {
                            return element.data().placeholder + ' <b class="caret"></b>';
                        } else if (options.length > 1) {
                            return _.first(options).text + ' + ' + (options.length - 1) + ' more selected <b class="caret"></b>';
                        } else {
                            return _.first(options).text + ' <b class="caret"></b>';
                        }
                    },
                    // Replicate the native functionality on the elements so
                    // that angular can handle the changes for us.
                    onChange: function(optionElement, checked) {
                        optionElement.removeAttr('selected');
                        if (checked) {
                            optionElement.attr('selected', 'selected');
                        }
                        element.change();
                    }

                });
                // Watch for any changes to the length of our select element
                scope.$watch(function() {
                    return element[0].length;
                }, function() {
                    element.multiselect('rebuild');
                });

                // Watch for any changes from outside the directive and refresh
                scope.$watch(attributes.ngModel, function() {
                    element.multiselect('refresh');
                });

                // Below maybe some additional setup
            };
        }]);
}());;(function() {
    'use strict';

    angular.module('Starscream.Services').factory('accountService', ['$httpq', function($httpq) {

        return {
            Register: function(email, password, name, phoneNumber, abilities) {
                return $httpq.post("/register", {
                    email: email,
                    password: password,
                    name: name,
                    phoneNumber: phoneNumber,
                    abilities: abilities
                });
            },
            RegisterFacebook: function(payload) {
                return $httpq.post("/register/facebook", payload);
            },
            RegisterGoogle: function(payload) {
                return $httpq.post("/register/google", payload);
            },
            RequestToResetPassword: function(email) {
                return $httpq.post("/password/requestReset", {
                    email: email
                });
            },
            ResetPassword: function(token, password) {
                return $httpq.put("/password/reset/" + token, {
                    password: password
                });
            }
        };
    }]);
}());;(function(){
    'use strict';
    
    angular.module('Starscream.Services').factory('adminService', ['$httpq', function($httpq) {

        return {
            GetUsers: function(payload) {
                return $httpq.get("/users?" + "PageNumber=" + payload.PageNumber + "&PageSize=" + payload.PageSize + "&Field=" + payload.Field);
            },
            EnableUser: function(payload) {
                return $httpq.post("/users/enable", payload);
            },
            GetUser: function(id) {
                return $httpq.get("/user/" + id);
            },
            UpdateProfile: function(payload) {
                return $httpq.post("/user/", payload);
            },
            GetRol: function() {
                return $httpq.get("/rol");
            }

        };
    }]);
}());
;(function() {
    'use strict';

    angular.module('Starscream.Services').factory('facebookService', ['$q', '$httpq', 'loginService', 'accountService', function($q, $httpq, loginService, accountService) {

        var login = function(payload) {
            var def = $q.defer();
            loginService.LoginFacebook(payload).then(function(data) {
                def.resolve(data);
            }).catch(function(error) {
                def.reject(error);
            });
            return def.promise;
        };

        var register = function(payload) {
            var def = $q.defer();
            accountService.RegisterFacebook(payload).then(function() {
                def.resolve();
            }).catch(function() {
                def.reject();
            });
            return def.promise;
        };

        var isDisable = function(error) {
            return error === "\"Your account has been disabled. Please contact your administrator for help.\"";
        };

        return {
            Login: function() {
                var def = $q.defer();
                FB.login(function(response) {
                    if (response.authResponse) {
                        FB.api('/me', function(response) {
                            login(response).then(function(data) {
                                def.resolve(data);
                            }).catch(function(error) {
                                if (isDisable(error)) {
                                    def.reject(error);
                                } else {
                                    register(response).then(function() {
                                        login(response).then(function(data) {
                                            def.resolve(data);
                                        });
                                    });
                                }
                            });
                        });
                    } else {
                        def.reject();
                    }
                }, {
                    scope: 'email'
                });
                return def.promise;
            }
        };
    }]);
}());;(function() {
    'use strict';

    angular.module('Starscream.Services').factory('googleService', ['$q', '$httpq', 'loginService', 'accountService', function($q, $httpq, loginService, accountService) {
        var login = function(payload) {
            var def = $q.defer();
            loginService.LoginGoogle(payload).then(function(data) {
                def.resolve(data);
            }).catch(function(error) {
                def.reject(error);
            });
            return def.promise;
        };

        var register = function(payload) {
            var def = $q.defer();
            accountService.RegisterGoogle(payload).then(function() {
                def.resolve();
            }).catch(function() {
                def.reject();
            });
            return def.promise;
        };

        var isDisable = function(error) {
            return error === "\"Your account has been disabled. Please contact your administrator for help.\"";
        };
        return {
            Login: function() {
                var def = $q.defer();
                gapi.auth.authorize({
                    client_id: clientId,
                    scope: scopes,
                    immediate: false
                }, function(authResult) {
                    if (authResult && !authResult.error) {
                        gapi.client.load('plus', 'v1', function() {
                            var request = gapi.client.plus.people.get({
                                'userId': 'me'
                            });
                            request.execute(function(response) {
                                response.email = response.emails[0].value;
                                login({
                                    Id: response.id,
                                    Email: response.email
                                }).then(function(data) {
                                    def.resolve(data);
                                }).catch(function(error) {
                                    if (isDisable(error)) {
                                        def.reject(error);
                                    } else {
                                        register(response).then(function() {
                                            login({
                                                Id: response.id,
                                                Email: response.email
                                            }).then(function(data) {
                                                def.resolve(data);
                                            });
                                        });
                                    }
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
    }]);
}());;(function() {
	'use strict';

	angular.module('Starscream.Services').factory('homeService', ['$httpq', function($httpq) {

	}]);
}());;(function() {
    'use strict';

    angular.module('Starscream.Services').factory('$httpq', ['$http', '$q', function($http, $q) {

        return {
            post: function(resource, payload) {
                var defer = $q.defer();
                $http.post(resource, payload)
                    .success(function(data) {
                        defer.resolve(data);
                    }).error(function(data) {
                        defer.reject(data);
                    });
                return defer.promise;
            },
            put: function(resource, payload) {
                var defer = $q.defer();
                $http.put(resource, payload)
                    .success(function(data) {
                        defer.resolve(data);
                    }).error(function(data) {
                        defer.reject(data);
                    });
                return defer.promise;
            },
            get: function(resource) {
                var defer = $q.defer();
                $http.get(resource)
                    .success(function(data) {
                        defer.resolve(data);
                    }).error(function(data) {
                        defer.reject(data);
                    });
                return defer.promise;
            },
            delete: function(resource) {
                var defer = $q.defer();
                $http.delete(resource)
                    .success(function(data) {
                        defer.resolve(data);
                    }).error(function(data) {
                        defer.reject(data);
                    });
                return defer.promise;
            },

        };
    }]);

}());;(function() {
    'use strict';
    
    angular.module('Starscream.Services').factory('loginService', ['$httpq', function($httpq) {
        var loggedIn = false;

        return {
            Login: function(email, password) {

                var response = $httpq.post("/login", {
                    email: email,
                    password: password
                });
                return response;
            },
            LoginFacebook: function(payload) {

                var response = $httpq.post("/login/facebook", payload);
                return response;
            },
            LoginGoogle: function(payload) {

                var response = $httpq.post("/login/google", payload);
                return response;
            },

            GetLoggedIn: function() {
                return loggedIn;
            },

            SetLoggedIn: function(value) {
                loggedIn = value;
            }
        };
    }]);
}());;(function() {
    'use strict';

    angular.module('Starscream.Services').factory('menuService', [function() {

        return {
            menu: [{
                display: 'Admin',
                features: [{
                    name: 'ActivateDeactivateUsers',
                    display: 'User Management',
                    route: '/activate-deactivate-users'
                }]
            }, {
                display: 'General',
                features: [{
                    name: 'Home',
                    display: 'Go to Home',
                    route: '/home'
                }]
            }],

            getFeatures: function() {
                var features = [];
                this.menu.forEach(function(value, index, array) {
                    var tmpFeature = features.concat(value.features);
                    features = tmpFeature;
                });

                return features;
            },
            getMenuForUser: function(claims) {

                var menuUser = [];
                this.menu.forEach(function(menuValue, index, array) {


                    var userFeatures = menuValue.features.filter(function(feature) {
                        return claims.some(function(value) {
                            return value === feature.name;
                        });
                    });
                    if (userFeatures.length > 0) {

                        var userMenu = {
                            display: menuValue.display,
                            features: []
                        };
                        userFeatures.forEach(function(value, index, array) {
                            userMenu.features.push(value);

                        });
                        menuUser.push(userMenu);
                    }



                });

                return menuUser;

            }
        };
    }]);
}());;(function() {
	'use strict';

	angular.module('Starscream.Services').factory('userAbilitiesService', ['$httpq', function($httpq) {

		return {
			GetAbilities: function() {
				return $httpq.get('/abilities');
			},
			AddAbilities: function(userId, abilities) {
				var response = $httpq.post('/user/abilites', {
					userId: userId,
					abilities: abilities
				});
				return response;
			}
		};
	}]);
}());;(function() {
    'use strict';
    
    angular.module('Starscream.Services').factory('userService', [function() {

        var key = "user";

        return {
            SetUser: function(email, name, token, remember, expires, claims) {
                var user = {
                    email: email,
                    name: name,
                    token: token,
                    expires: expires,
                    claims: claims
                };

                var userString = JSON.stringify(user);

                if (remember) {
                    window.localStorage.setItem(key, userString);
                } else {
                    window.sessionStorage.setItem(key, userString);
                }
            },
            GetUser: function() {
                var user;
                var userFromLocalStorage = window.localStorage.getItem(key);
                if (userFromLocalStorage) {
                    user = JSON.parse(userFromLocalStorage);
                } else {
                    var userFromSession = window.sessionStorage.getItem(key);
                    if (!userFromSession) {
                        return false;
                    }
                    user = JSON.parse(userFromSession);
                }
                var expires = new Date(user.expires);
                var isExpired = expires < new Date();
                if (isExpired) {
                    this.RemoveUser();
                    return false;
                }
                return user;
            },
            RemoveUser: function() {
                window.sessionStorage.removeItem("user");
                window.localStorage.removeItem("user");
            },

        };
    }]);
}());