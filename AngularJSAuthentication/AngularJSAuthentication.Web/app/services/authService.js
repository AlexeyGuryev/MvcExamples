'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
    var serviceBase = 'http://localhost:58722/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ''
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http
            .post(serviceBase + 'api/account/register', registration)
            .then(function (response) {
                return response;
            });
    };

    var _login = function (loginData) {
        var data = "grant_type=password&username=" +
            loginData.userName + "&password=" + loginData.password + '&client_id=ngAuthApp';

        var deffered = $q.defer();

        $http.post(serviceBase + 'token', data, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (response) {
            localStorageService.set('authorizationData', {
                token: response.access_token,
                userName: loginData.userName,
                refreshToken: response.refresh_token
            });

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

            deffered.resolve(response);
        }).error(function (err, status) {
            _logOut();
            deffered.reject(err);
        });

        return deffered.promise;
    };

    var _logOut = function () {
        localStorageService.remove('authorizationData');
        _authentication.isAuth = false;
        _authentication.userName = '';
    };

    var _fillAuthData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }
    }

    var _refreshToken = function () {
        var deffered = $q.defer();
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=ngAuthApp";

            localStorageService.remove('authorizationData');

            $http.post(serviceBase + 'token', data,
                { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function (response) {
                    localStorageService.set('authorizationData', {
                        token: response.access_token,
                        userName: response.userName,
                        refreshToken: response.refresh_token
                    });                    
                    deffered.resolve(response);
            }).error(function (err, status) {
                _logOut();
                deffered.reject(err);
            });
        }
    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.refreshToken = _refreshToken;

    return authServiceFactory;
}]);