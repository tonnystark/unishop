﻿(function(app) {
    app.controller("productCategoryAddController", productCategoryAddController);

    productCategoryAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true,
            Name: "Danh mục 1"
        };
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }


        $scope.addproductCategory = addproductCategory;

        function addproductCategory() {
            apiService.post("/api/productcategory/create",
                $scope.productCategory,
                function(result) {
                    notificationService.displaySuccess(result.data.Name + " đã được thêm mới.");
                    $state.go("product_categories");
                },
                function(error) {
                    notificationService.displayError("Thêm mới không thành công");
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

    }


})(angular.module("unishop.product_categories"));