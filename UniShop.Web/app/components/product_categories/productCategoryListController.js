(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService'];

    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.getProductCategories = getProductCategories;

        function getProductCategories() {
            apiService.get('/api/productcategory/getall',
                null,
                function(response) {
                    $scope.productCategories = response.data;
                },
                function (response) {
                    Console.log(response.statusText);
                });
        }

        $scope.getProductCategories();

    }
})(angular.module('unishop.product_categories'));