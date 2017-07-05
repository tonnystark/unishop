var cart = {
    init: function() {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function() {
        $("#frmPayment")
            .validate({
                rules: {
                    name: "required",
                    address: "required",
                    email: {
                        required: true,
                        email: true
                    },
                    phone: {
                        required: true,
                        number: true
                    }
                },
                messages: {
                    name: "Yêu cầu nhập tên",
                    address: "Yêu cầu nhập địa chỉ",
                    email: {
                        required: "Bạn cần nhập email",
                        email: "Định dạng email chưa đúng"
                    },
                    phone: {
                        required: "Số điện thoại được yêu cầu",
                        number: "Số điện thoại phải là số."
                    }
                }
            });

        $(".btnDeleteItem")
            .off("click")
            .on("click",
                function(e) {
                    e.preventDefault();
                    var productId = parseInt($(this).data("id"));
                    cart.deleteItem(productId);
                });


        $(".txtQuantity")
            .off("keyup")
            .on("keyup",
                function() {
                    var productId = parseInt($(this).data("id"));
                    var quantity = parseInt($(this).val());
                    var price = parseInt($(this).data("price"));

                    if (isNaN(quantity) == false) {
                        var amount = quantity * price;
                        $("#amount_" + productId).text(numeral(amount).format("0,0"));
                    } else {
                        $("#amount_" + productId).text(0);
                    }

                    $("#lblTotalOrder").text(numeral(cart.getTotalOrder()).format("0,0"));

                    cart.updateAll();
                });

        $("#btnContinue")
            .off("click")
            .on("click",
                function(e) {
                    e.preventDefault();
                    window.location.href = "/";
                });
        $("#btnCheckout")
            .off("click")
            .on("click",
                function(e) {
                    e.preventDefault();
                    $("#divCheckout").show();
                });
        $("#btnDeleteAll")
            .off("click")
            .on("click",
                function(e) {
                    e.preventDefault();
                    cart.deleteAll();
                });
        $("#chkUserLoginInfo")
            .off("click")
            .on("click",
                function() {
                    if ($(this).prop("checked"))
                        cart.getLoginUser();
                    else {
                        $("#txtName").val("");
                        $("#txtAddress").val("");
                        $("#txtEmail").val("");
                        $("#txtPhone").val("");
                    }
                });
        $("#btnCreateOrder")
            .off("click")
            .on("click",
                function(e) {
                    e.preventDefault();
                    var isValid = $("#frmPayment").valid();
                    if (isValid)
                        cart.createOrder();
                });
    },

    loadData: function() {
        $.ajax({
            url: "/ShoppingCart/GetAll",
            type: "GET",
            dataType: "json",
            success: function(res) {
                if (res.status) {
                    var template = $("#cartTemplate").html();
                    var html = "";
                    var data = res.data;
                    $.each(data,
                        function(i, item) {
                            html += Mustache.render(template,
                            {
                                ProductId: item.ProductId,
                                Quantity: item.Quantity,
                                ProductName: item.Product.Name,
                                Price: item.Product.Price,
                                Image: item.Product.Image,
                                PriceF: numeral(item.Product.Price).format("0,0"),
                                Amount: numeral(item.Quantity * item.Product.Price).format("0,0")
                            });
                        });
                    $("#cartBody").html(html);

                    if (html == "") {
                        $("#cartContent").html("Không có sản phẩm nào");
                    }
                    if (isNaN(cart.getTotalOrder()) == false)
                        $("#lblTotalOrder").text(numeral(cart.getTotalOrder()).format("0,0"));
                    else
                        $("#lblTotalOrder").text(0);
                    cart.registerEvent();
                }
            }
        });
    },
    deleteItem: function(productId) {
        $.ajax({
            url: "/ShoppingCart/DeleteItem",
            data: {
                productId: productId
            },
            type: "POST",
            dataType: "json",
            success: function(res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    getTotalOrder: function() {
        var lstAmount = $(".amount");
        var total = 0;
        $.each(lstAmount,
            function(i, item) {
                total += parseFloat($(item).text().replace(/,/g, ""));
            });
        return total;
    },
    deleteAll: function() {
        $.ajax({
            url: "/ShoppingCart/DeleteAll",
            type: "POST",
            dataType: "json",
            success: function(res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    getLoginUser: function() {
        $.ajax({
            url: "/ShoppingCart/GetUser",
            type: "Get",
            dataType: "json",
            success: function(res) {
                if (res.status) {
                    var user = res.data;
                    $("#txtName").val(user.FullName);
                    $("#txtAddress").val(user.Address);
                    $("#txtEmail").val(user.Email);
                    $("#txtPhone").val(user.PhoneNumber);
                }
            }
        });
    },
    updateAll: function() {
        var lstCart = [];
        $.each($(".txtQuantity"),
            function(i, item) {
                lstCart.push({
                    ProductId: $(item).data("id"),
                    Quantity: $(item).val()
                });
            });
        $.ajax({
            url: "/ShoppingCart/Update",
            data: {
                cartData: JSON.stringify(lstCart)
            },
            type: "POST",
            dataType: "json",
            success: function(res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    createOrder: function() {
        var order = {
            CustomerName: $("#txtName").val(),
            CustomerAddress: $("#txtAddress").val(),
            CustomerEmail: $("#txtEmail").val(),
            CustomerMobile: $("#txtPhone").val(),
            CustomerMessage: $("#txtMessage").val(),
            PaymentMethod: "Thanh toán tiền mặt",
            Status: false
        };
        $.ajax({
            url: "/ShoppingCart/CreateOrder",
            data: {
                orderViewModel: JSON.stringify(order)
            },
            type: "POST",
            dataType: "json",
            success: function(res) {
                if (res.status) {
                    console.log("create order ok");
                    $("#divCheckout").hide();
                    cart.deleteAll();
                    setTimeout(function() {
                            $("#cartContent").html("Cảm ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ sớm nhất.");
                        },
                        2000);
                }
            }
        });
    }
};
cart.init();