app.controller('esqueciSenhaController', function ($scope, $http, toasterAlert, UserService, $location) {

    var mensagemSalvo = 'Nova senha gerada com sucesso e enviada para [EMAIL]';
    var url = 'api/esquecisenha';

    $scope.heading = 'Esqueci Minha Senha';
    $scope.usuario = {};

    //APIs
    $scope.postGeraNovaSenha = function () {

        $http.post(url, $scope.usuario).success(function (data) {
            toasterAlert.showAlert(JSON.stringify({ Success: "info", Messages: [{ Message: mensagemSalvo.replace('[EMAIL]', $scope.usuario.email) }] }));
            $scope.usuario = {};
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.cookieDestroy = function () {
        UserService.setUser(null);
        $scope.$emit('atualizaHeaderEmit', null);
    };
});