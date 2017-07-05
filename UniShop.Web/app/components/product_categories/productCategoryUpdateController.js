(function(app) {
    app.controller("productCategoryUpdateController", productCategoryUpdateController);

    productCategoryUpdateController.$inject = [
        "$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"
    ];

    function productCategoryUpdateController($scope,
        apiService,
        notificationService,
        $state,
        $stateParams,
        commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        };
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }


        $scope.updateProductCategory = updateProductCategory;

        function updateProductCategory() {
            apiService.post("/api/productcategory/update",
                $scope.productCategory,
                function(result) {
                    notificationService.displaySuccess(result.data.Name + " đã được cập nhật");
                    $state.go("product_categories");
                },
                function(error) {
                    notificationService.displayError("Cập nhật không thành công");
                });
        }

        function loadProductCategoryById() {
            apiService.get("/api/productcategory/getbyid/" + $stateParams.id,
                null,
                function(result) {
                    $scope.productCategory = result.data;
                },
                function(error) {
                    console.log(error.data);
                });
        }

        function loadParentCategory() {
            apiService.get("/api/productcategory/getallparents",
                null,
                function(result) {
                    $scope.parentCategories = result.data;
                },
                function() {
                    console.log("Can not get list parent");
                });
        }

        loadParentCategory();
        loadProductCategoryById();

    }


})(angular.module("unishop.product_categories"));