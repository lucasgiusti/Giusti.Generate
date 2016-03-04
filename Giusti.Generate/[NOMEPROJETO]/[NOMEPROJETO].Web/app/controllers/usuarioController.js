app.controller('usuarioController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService) {
    UserService.verificaLogin();

    var mensagemExcluir = 'Deseja realmente excluir o usuário [NOMEUSUARIO] ?';
    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Usuário salvo com sucesso' }] });
    var url = 'api/usuario';
    var urlPerfil = 'api/perfil';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };

    $scope.heading = 'Usuários';
    $scope.usuarios = [];
    $scope.usuario = null;

    //APIs
    $scope.getUsuarios = function () {

        $http.get(url, headerAuth).success(function (data) {
            $scope.usuarios = data;
            $scope.total = $scope.usuarios.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    $scope.getUsuario = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id, headerAuth).success(function (data) {
            $scope.usuario = data;
            $scope.getPerfis();
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.postUsuario = function () {
        $scope.preenchePerfisUsuario();

        $http.post(url, JSON.stringify($scope.usuario), headerAuth).success(function (id) {
            $scope.id = id;
            $scope.getUsuario();
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.putUsuario = function () {
        $scope.preenchePerfisUsuario();

        $http.put(url + '/' + $scope.id, JSON.stringify($scope.usuario), headerAuth).success(function (data) {
            $scope.usuario = data;
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.deleteUsuario = function () {

        $http.delete(url + '/' + $scope.usuario.id, headerAuth).success(function (result) {
            toasterAlert.showAlert(result);
            $scope.usuarios.splice($scope.usuarios.indexOf($scope.usuario), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    $scope.getPerfis = function () {

        $http.get(urlPerfil, headerAuth).success(function (data) {
            $scope.perfisDisponiveis = data;

            angular.forEach(data, function (perfil, key) {

                perfil.usuarioPossui = false;
                angular.forEach($scope.usuario.perfis, function (perfilUsuario, key) {
                    if (perfilUsuario.perfilId == perfil.id) {
                        perfil.usuarioPossui = true;
                    }
                });
            });



        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    }

    //Utils
    $scope.addUsuario = function () {
        $scope.usuario = { ativo: 1 };
        $scope.getPerfis();
    };

    $scope.openModalDelete = function (usuario) {
        $scope.usuario = usuario;
        $scope.dadosModalConfirm = { 'titulo': 'Excluir', 'mensagem': mensagemExcluir.replace('[NOMEUSUARIO]', $scope.usuario.nome) };

        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'app/templates/modalConfirm.html',
            controller: 'modalConfirmInstanceController',
            resolve: {
                dadosModalConfirm: function () {
                    return $scope.dadosModalConfirm;
                }
            }
        });

        modalInstance.result.then(function () {
            $scope.deleteUsuario();
        });
    };

    $scope.preenchePerfisUsuario = function () {
        $scope.usuario.perfis = [];
        angular.forEach($scope.perfisDisponiveis, function (perfil, key) {
            if (perfil.usuarioPossui) {
                $scope.usuario.perfis.push({ perfilId: perfil.id });
            }
        });
    }

    //PAGINATION
    $scope.total = 0;
    $scope.currentPage = 1;
    $scope.itemPerPage = 5;
    $scope.start = 0;
    $scope.maxSize = 5;
    $scope.pageChanged = function () {
        $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
    };
});
