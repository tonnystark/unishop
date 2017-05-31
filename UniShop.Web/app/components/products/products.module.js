/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function() {
    angular.module("unishop.products", ["unishop.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("products",
            {
                url: "/products",
                templateUrl: "/app/components/products/productListView.html",
                controller: "productListController"
            })
            .state("add_product",
            {
                url: "/add_product",
                templateUrl: "/app/components/products/productAddView.html",
                controller: "productAddController"
            }).state("update_product",
            {
                url: "/update_product/:id",
                templateUrl: "/app/components/products/productUpdateView.html",
                controller: "productUpdateController"
            });
    }
})();