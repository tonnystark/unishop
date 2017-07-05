/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function() {
    angular.module("unishop.product_categories", ["unishop.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("product_categories",
            {
                url: "/product_categories",
                parent: "base",
                templateUrl: "/app/components/product_categories/productCategoryListView.html",
                controller: "productCategoryListController"
            })
            .state("add_product_categories",
            {
                url: "/add_product_categories",
                parent: "base",
                templateUrl: "/app/components/product_categories/productCategoryAddView.html",
                controller: "productCategoryAddController"
            })
            .state("update_product_categories",
            {
                url: "/update_product_categories/:id",
                parent: "base",
                templateUrl: "/app/components/product_categories/productCategoryUpdateView.html",
                controller: "productCategoryUpdateController"
            });
    }
})();