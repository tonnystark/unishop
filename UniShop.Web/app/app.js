/// <reference path="components/home/homeView.html" />
/// <reference path="components/home/homeView.html" />
(function() {
    angular.module("unishop",
        [
            "unishop.products",
            "unishop.product_categories",
            "unishop.common"
        ])
        .config(config);

    config.inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("home",
        {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise("/admin");
    }
})();