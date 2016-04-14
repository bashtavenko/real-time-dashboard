"use strict";
dashboardModule.controller("DashboardController", function($scope, dashboardService) {
    $scope.events = [
        { time: "10:48", fatalCount: "90" },
        { time: "10:49", fatalCount: "10" },
        { time: "10:50", fatalCount: "20" }
    ];

    $scope.getProducts = function() {
        dashboardService.getProducts($scope.productName)
            .$promise.then(
                function(response) {
                    $scope.products = response;
                }
            );
    };
});

