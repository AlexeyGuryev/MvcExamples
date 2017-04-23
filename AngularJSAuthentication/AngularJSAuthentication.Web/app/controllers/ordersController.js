'use strict';
app.controller('ordersController', ['$scope', 'ordersService', function ($scope, ordersService) {
    ordersService.getOrders().then(function (result) {
        $scope.orders = result.data;
    });
}]);