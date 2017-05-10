/// <reference path="../plugins/angular/angular.js" />
var app = angular.module('myModule', [])
    .controller('myController',
        function ($scope, validate) {
            $scope.num = 1;
            $scope.checkNum = function () {
                $scope.Message = validate.checkNum($scope.num);
            }
            $scope.customer = {
                'Name': 'Muni',
                'Gender': 'Female'
            };
        });

app.factory('validate', validate);
app.directive('validDirective',
    function () {
        return {
            restrict: 'E',
            template: 'Name: {{customer.Name}} </br> ' +
                       'Gender: {{customer.Gender}} '
        }
    });

function validate() {
    return {
        checkNum: checkNum, addNum
    }

    function checkNum(input) {
        return input % 2 != 0 ? 'Odd number' : 'Even number';
    }

    function addNum(input) {
        return input++;
    }
}

