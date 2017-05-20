(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService'];

    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCatgories = getProductCatgories;
        $scope.keywords = '';

        $scope.search = search;

        function search() {
            getProductCatgories();
        }

        function getProductCatgories(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 2,
                    keyword: $scope.keywords
                }
            }

            apiService.get('/api/productcategory/getall',
                config,
                function (result) {

                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning('Không tìm thấy bản ghi nào');
                    }

                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log('Load productcategory failed.');
                });
        }

        $scope.getProductCatgories();

    }
})(angular.module('unishop.product_categories'));