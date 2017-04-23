angular.module("QuizApp", [])
    .controller("QuizCtrl", function ($scope, $http) {
        $scope.answered = false;
        $scope.title = "загрузка вопроса...";
        $scope.options = [];
        $scope.correctAnswer = false;
        $scope.working = false;

        $scope.answer = function () {
            return $scope.correctAnswer ? 'правильно' : 'неправильно';
        };

        $scope.nextQuestion = function () {
            $scope.working = true;
            $scope.answered = false;
            $scope.title = "загрузка вопроса...";
            $scope.options = [];

            $http.get("/api/trivia").success(function (data, status, headers, config) {
                $scope.options = data.options;
                $scope.title = data.title;
                $scope.answered = false;
                $scope.working = false;
            }).error(function (data, status, headers, config) {
                $scope.title = "Упс... что-то пошло не так";
                $scope.working = false;
            });
        };

        $scope.sendAnswer = function (option) {
            $scope.working = true;
            $scope.answered = true;

            $http.post('/api/trivia', { 'questionId': option.questionId, 'optionId': option.id })
            .success(function (data, status, headers, config) {
                $scope.correctAnswer = data;
                $scope.working = false;
            }).error(function (data, status, headers, config) {
                $scope.title = "Упс.. что-то пошло не так";
                $scope.working = false;
            });
        };
    });