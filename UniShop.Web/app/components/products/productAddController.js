(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.chooseImage = function() {
            var finder = new CKFinder();
            finder.selectActionFunction = function(fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        };

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }


        $scope.addProduct = addProduct;

        function addProduct() {
            apiService.post('/api/product/create',
                $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('products');
                },
                function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }

        function loadProductCategory() {
            apiService.get('/api/productcategory/getallparents',
                null,
                function (result) {
                    $scope.productCategories = result.data;
                }, function () {
                    console.log('Can not get list parent');
                });
        }
        loadProductCategory();

    }


})(angular.module('unishop.products'));