'use strict';
angular.module('Data')
    .factory('DataService', ['$http',
        function ($http) {
            var service = {};

            service.GetData = function () {
                return $http.get('http://localhost:50528/api/object')
                    .then(function (response) {
                        return response.data;
                    });
            }

            service.SaveData = function (formData) {
                return $http.post('http://localhost:50528/api/object', { formData: formData })
                    .then(function (response) {
                        return response.data;
                    });
            }

            return service;
        }])