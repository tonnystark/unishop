(function(app) {
    app.filter("statusFilter",
        function() {
            return function(status) {
                return status ? "Kích hoạt" : "Khoá";
            };
        });
})(angular.module("unishop.common"));