'use strict';
app.factory('ordersService', ['$http', function ($http) {
    var serviceBase = 'http://localhost:58722/';
    var ordersServiceFactory = {};

    var _getOrders = function () {
        return $http.get(serviceBase + 'api/orders').then(function (results) {
            return results;
        }/*, function (err) {
            alert(err);
        }*/);
    };

    ordersServiceFactory.getOrders = _getOrders;
    return ordersServiceFactory;
}]);