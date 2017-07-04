angular.module('Save')
    .controller('SaveController', ['$scope', '$location', 'DataService',
        function ($scope, $location, DataService) {

            $scope.resetForm = function () {

                console.log($scope.object);
                var subscriptionInit = { News: false, Messages: false, Jobs: false };

                $scope.object = {
                    name: '',
                    email: '',
                    gender: '',
                    country: '',
                    birthdate: '',
                    subscription: subscriptionInit
                };
                if ($scope.objectForm)
                    $scope.objectForm.$setPristine(); //reset Form
            }

            $scope.resetForm();

            $scope.popup1 = {
                opened: false
            };

            $scope.form_validation_status = function () {
                var is_form_valid = false;

                return is_form_valid;
            }

            $scope.open1 = function () {
                $scope.popup1.opened = true;
            };

            $scope.dateOptions = {
                formatYear: 'yy',
                maxDate: new Date(2017, 7, 1),
                minDate: new Date(1900, 1, 1),
                startingDay: 1
            };

            $scope.submitForm = function () {
                DataService.SaveData($scope.object).then(
                    function (data) { // Success callback
                        $location.path('/');
                    },
                    function (response) { // Error callback
                        //$location.path('/');
                        console.log("Error from API: " + response.data);
                        // Handle error state
                        $scope.error = "Sorry, API seems to be unavailable this time. Can not Insert data."
                    })
            }
        }])