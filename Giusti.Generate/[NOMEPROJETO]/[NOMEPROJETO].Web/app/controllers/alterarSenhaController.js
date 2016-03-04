app.controller('alterarSenhaController', function ($scope, $http, toasterAlert, UserService, $location) {
    UserService.verificaLogin();

    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Senha alterada com sucesso' }] });
    var url = 'api/alterarsenha';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };

    $scope.heading = 'Alterar Senha';
    $scope.usuario = {};
    $scope.usuarioLogado = UserService.getUser();

    //APIs
    $scope.putAlteraSenha = function () {
        $scope.usuario.email = $scope.usuarioLogado.email;
        $http.put(url + '/' + $scope.usuarioLogado.id, JSON.stringify($scope.usuario), headerAuth).success(function (data) {
            toasterAlert.showAlert(mensagemSalvo);
            $scope.usuario = {};
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };
});