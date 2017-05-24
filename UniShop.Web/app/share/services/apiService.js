/// <reference path="D:\Project\Git Resource\UniShop\UniShop.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.factory("apiService", apiService);

    apiService.$inject = ["$http"];

    function apiService($http) {
        return {
            get: get,
            post: post
        };

        function get(url, params, success, failure) {
            $http.get(url, params)
                .then(function (response) {
                    success(response);
                }), function (error) {
                    failure(error);
                };
        }

        function post(url, data, success, failure) {
            $http.post(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                        failure(error);
                    });
        }
    }


})(angular.module("unishop.common"));