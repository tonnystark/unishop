(function (app) {
    app.controller('rootController', rootController);
    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];
    function rootController($state, authData, loginService, $scope, authenticationService) {
        $scope.logout = function () {
            $state.go('login');
        };
        $scope.authentication = authData.authenticationData;

        authenticationService.validateRequest();
    }

})(angular.module('unishop'));