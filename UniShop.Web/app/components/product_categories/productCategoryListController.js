(function(app) {
    app.controller("productCategoryListController", productCategoryListController);

    productCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCatgories = getProductCatgories;
        $scope.keywords = "";

        $scope.search = search;

        function search() {
            getProductCatgories();
        }


        $scope.deleteProductCategory = deleteProductCategory;

        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa?")
                .then(function() {
                    var config = {
                        params: {
                            id: id
                        }
                    };
                    apiService.del("/api/productcategory/delete",
                        config,
                        function(result) {
                            notificationService.displaySuccess("Đã xóa " + result.data.Name + " thành công");
                            search();
                        },
                        function(error) {
                            console.log(error);
                            notificationService.displayError("Xóa không thành công");
                        });
                });
        }

        $scope.isAll = false;

        $scope.selectAll = selectAll;

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories,
                    function(item) {
                        item.checked = true;
                    });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories,
                    function(item) {
                        item.checked = false;
                    });
                $scope.isAll = false;
            }
        }

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var lstId = [];
            $.each($scope.selected,
                function(i, item) {
                    lstId.push(item.ID);
                });
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(lstId)
                }
            };
            apiService.del("/api/productcategory/deletemulti",
                config,
                function(result) {
                    notificationService.displaySuccess("Đã xóa " + result.data + " bản ghi thành công");
                    search();
                },
                function(error) {
                    console.log(error);
                    notificationService.displayError("Xóa không thành công");
                });
        }


        $scope.$watch("productCategories",
            function(n, o) {
                var checked = $filter("filter")(n, { checked: true });
                if (checked.length) {
                    $scope.selected = checked;
                    $("#btnDeleteAll").removeAttr("disabled", "disabled");
                } else {
                    $("#btnDeleteAll").attr("disabled", "disabled");
                }
            },
            true);


        function getProductCatgories(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keywords
                }
            };
            apiService.get("/api/productcategory/getall",
                config,
                function(result) {

                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning("Không tìm thấy bản ghi nào");
                    }

                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function() {
                    console.log("Load productcategory failed.");
                });
        }

        $scope.getProductCatgories();

    }
})(angular.module("unishop.product_categories"));