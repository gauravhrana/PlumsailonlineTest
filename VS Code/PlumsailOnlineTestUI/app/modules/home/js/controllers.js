angular.module('SimpleWebApiApp')
    .controller('HomeController', ['$scope', '$location', 'DataService',
        function ($scope, $location, DataService) {

            $scope.objects = [];

            $scope.GoToAddForm = function () {
                $location.path('/add');
            }
            $scope.searchTerm = '';

            $scope.getData = function () {
                DataService.GetData().then(
                    function (data) { // Success callback
                        $scope.objects = data;
                    },
                    function (response) { // Error callback
                        // Handle error state
                        $scope.error = "Sorry, API seems to be unavailable this time. Can not fetch data."
                    })
            }

            $scope.getData();

        }])