(function (app) {
    app.controller("productListController", productListController);

    productListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.keywords = "";

        $scope.search = search;

        function search() {
            getProducts();
        }


        $scope.deleteProduct = deleteProduct;

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete',
                    config,
                    function (result) {
                        notificationService.displaySuccess('Đã xóa ' + result.data.Name + ' thành công');
                        search();
                    },
                    function (error) {
                        console.log(error);
                        notificationService.displayError('Xóa không thành công');
                    });
            });
        }

        $scope.isAll = false;

        $scope.selectAll = selectAll;

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products,
                    function (item) {
                        item.checked = true;
                    });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products,
                    function (item) {
                        item.checked = false;
                    });
                $scope.isAll = false;
            }
        }

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var lstId = [];
            $.each($scope.selected,
                function (i, item) {
                    lstId.push(item.ID);
                });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(lstId)
                }
            }

            apiService.del('/api/product/deletemulti',
                   config,
                   function (result) {
                       notificationService.displaySuccess('Đã xóa ' + result.data + ' bản ghi thành công');
                       search();
                   },
                   function (error) {
                       console.log(error);
                       notificationService.displayError('Xóa không thành công');
                   });
        }



        $scope.$watch("products",
            function (n, o) {
                var checked = $filter("filter")(n, { checked: true });
                if (checked.length) {
                    $scope.selected = checked;
                    $('#btnDeleteAll').removeAttr('disabled', 'disabled');
                } else {
                    $('#btnDeleteAll').attr('disabled', 'disabled');
                }
            }, true);


        function getProducts(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    keyword: $scope.keywords
                }
            };
            apiService.get("/api/product/getall",
                config,
                function (result) {

                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning("Không tìm thấy bản ghi nào");
                    }

                    $scope.products = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log("Load product failed.");
                });
        }

        $scope.getProducts();

    }
})(angular.module("unishop.products"));