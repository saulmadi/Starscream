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
        .when('/register', {
            templateUrl: 'app/views/registration.html',
            controller: 'registrationController'
        })
        .when('/', {
            templateUrl: 'app/views/home.html',
            controller: 'homeController'
        })
        .when('/404', {
            templateUrl: 'App/Views/404.html'
        })
        .otherwise({
            redirectTo: '/404'
        });
})
    .config(['$httpProvider', function($httpProvider) {
        $httpProvider.interceptors.push(['$q', '$location', 'userService', function($q, $location, userService) {
            return {
                'responseError': function(rejection) {
                    if (rejection.status == 401) {
                        console.log("Not logged in. Redirecting to login page.");
                        userService.RemoveUser();
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        }]);
    }])
    .config(['$httpProvider', function($httpProvider) {
        $httpProvider.interceptors.push(['$q', 'userService', function($q, userService) {
            return {
                'request': function(config) {

                    console.log(config.method + " " + config.url);

                    var user = userService.GetUser();
                    if (user) {
                        config.headers["Authorization"] = 'Bearer ' + user.Token;
                    }
                    if (config.method == "POST" || config.method == "PUT") {
                        console.log(JSON.stringify(config.data));
                    }
                    return config || $q.when(config);
                },
                'requestError': function(rejection) {
                    console.log("RequestError: " + JSON.stringify(rejection));
                    return $q.reject(rejection);
                }
            };
        }]);
    }]);