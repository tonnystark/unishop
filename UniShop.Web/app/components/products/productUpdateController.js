(function (app) {
    app.controller('productUpdateController', productUpdateController);

    productUpdateController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"];

    function productUpdateController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.moreImages = []; 
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });

            }
            finder.popup();
        };

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        };

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }


        $scope.updateProduct = updateProduct;

        function updateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/update',
                $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                    $state.go('products');
                },
                function (error) {
                    notificationService.displayError('Cập nhật không thành công');
                });
        }

        function loadproductById() {
            apiService.get('/api/product/getbyid/' + $stateParams.id,
                null,
                function (result) {
                    $scope.product = result.data;
                    $scope.moreImages = JSON.parse($scope.product.MoreImages);
                }, function (error) {
                    console.log(error.data);
                });
        }

        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents',
                null,
                function (result) {
                    $scope.parentCategories = result.data;
                }, function () {
                    console.log('Can not get list parent');
                });
        }

        loadParentCategory();
        loadproductById();

    }


})(angular.module('unishop.products'));