/// <reference path="components/home/homeView.html" />
/// <reference path="components/home/homeView.html" />
(function () {
    angular.module("unishop",
        [
            "unishop.products",
            "unishop.product_categories",
            "unishop.application_groups",
            "unishop.application_roles",
            "unishop.application_users",
            "unishop.statistics",
            "unishop.common"
        ])
        .config(config)
        .config(configAuthentication);
    

    config.inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("base",
        {
            url: "",
            templateUrl: "/app/share/views/baseView.html",
            abstract: true
        }).state("login",
        {
            url: "/login",
            templateUrl: "/app/components/login/loginView.html",
            controller: "loginController"
        }).state("home",
        {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            parent: 'base',
            controller: "homeController"
        });
        $urlRouterProvider.otherwise("/login");
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();