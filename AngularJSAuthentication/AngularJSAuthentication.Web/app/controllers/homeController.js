'use strict';
app.controller('homeController', ['$scope', 'authService', function ($scope, authService) {
    $scope.isAuth = authService.authentication.isAuth;
}]);