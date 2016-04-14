"use strict";
dashboardModule.factory("dashboardService", function ($resource) {
    return {
        getProducts: function (productName) {
            var params = { productName: productName };
            return $resource("/api/products/:productName", { productName: "@productName" }).query(params);
        }
    };
});

