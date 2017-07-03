angular.module('SimpleWebApiApp')
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', {
                controller: 'HomeController',
                templateUrl: 'app/modules/home/views/home.html'
            })
            .when('/add', {
                controller: 'SaveController',
                templateUrl: 'app/modules/save/views/save.html'
            })
    }])