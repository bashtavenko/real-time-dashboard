"use strict";
dashboardModule.controller("DashboardController", function($scope, $rootScope) {

    $scope.trafficData = new wijmo.collections.ObservableArray();
    $scope.perMinute = 0;
    var connection = $.connection.dashboardHub;

    // SignalR calls this 
    connection.client.addBeat = function (data) {
        var maxLength = 20;
        while ($scope.trafficData.length >= maxLength) {
            $scope.trafficData.splice(0, 1);
        }
        $scope.trafficData.push(data);

        $rootScope.$apply(function () {
            // Because Angular doesn't know it changed
            $scope.perMinute = data.perMinute; 
        });
    };

    $.connection.hub.start().done(function () {});
});

