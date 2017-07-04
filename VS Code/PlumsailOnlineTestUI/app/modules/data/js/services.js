'use strict';
angular.module('Data')
    .factory('DataService', ['$http',
        function ($http) {
            var service = {};

            var apiUrl = "http://patang.in/api/object";
            //var apiUrl = "http://localhost:50528/api/object"

            service.GetData = function () {
                return $http.get(apiUrl)
                    .then(function (response) {
                        return response.data;
                    });
            }

            service.SaveData = function (formData) {
                return $http.post(apiUrl, { formData: formData })
                    .then(function (response) {
                        return response.data;
                    });
            }

            return service;
        }])