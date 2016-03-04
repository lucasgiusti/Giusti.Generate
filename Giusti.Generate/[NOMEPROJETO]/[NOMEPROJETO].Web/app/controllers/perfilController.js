app.controller('perfilController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams, UserService) {
    UserService.verificaLogin();

    var mensagemExcluir = 'Deseja realmente excluir o perfil [NOMEPERFIL] ?';
    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Perfil salvo com sucesso' }] });
    var url = 'api/perfil';
    var urlFuncionalidade = 'api/funcionalidade';
    var headerAuth = { headers: { 'Authorization': 'Basic ' + UserService.getUser().token } };

    $scope.heading = 'Perfis';
    $scope.perfis = [];
    $scope.perfil = null;

    //APIs
    $scope.getPerfis = function () {

        $http.get(url, headerAuth).success(function (data) {
            $scope.perfis = data;
            $scope.total = $scope.perfis.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    $scope.getPerfil = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id, headerAuth).success(function (data) {
            $scope.perfil = data;
            $scope.getFuncionalidades();
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.postPerfil = function () {
        $scope.perfil.perfilFuncionalidades = [];
        angular.forEach($scope.funcionalidadesDisponiveis, function (funcionalidade, key) {
            $scope.preencheFuncionalidadesPerfil(funcionalidade);
        });

        $http.post(url + "/", $scope.perfil, headerAuth).success(function (id) {
            $scope.id = id;
            $scope.getPerfil();
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.putPerfil = function () {
        $scope.perfil.perfilFuncionalidades = [];
        angular.forEach($scope.funcionalidadesDisponiveis, function (funcionalidade, key) {
            $scope.preencheFuncionalidadesPerfil(funcionalidade);
        });

        $http.put(url + "/" + $scope.id, JSON.stringify($scope.perfil), headerAuth).success(function (data) {
            $scope.perfil = data;
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    $scope.deletePerfil = function () {

        $http.delete(url + '/' + $scope.perfil.id, headerAuth).success(function (result) {
            toasterAlert.showAlert(result);
            $scope.perfis.splice($scope.perfis.indexOf($scope.perfil), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    $scope.getFuncionalidades = function () {

        $http.get(urlFuncionalidade, headerAuth).success(function (data) {

            $scope.funcionalidadesDisponiveis = data;

            angular.forEach($scope.funcionalidadesDisponiveis, function (funcionalidade, key) {
                $scope.verificaPerfilPossui(funcionalidade);
            });

        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    }

    //Utils
    $scope.addPerfil = function () {
        $scope.perfil = {};
        $scope.getFuncionalidades();
    };

    $scope.openModalDelete = function (perfil) {
        $scope.perfil = perfil;
        $scope.dadosModalConfirm = { 'titulo': 'Excluir', 'mensagem': mensagemExcluir.replace('[NOMEPERFIL]', $scope.perfil.nome) };

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
            $scope.deletePerfil();
        });
    };

    $scope.preencheFuncionalidadesPerfil = function (funcionalidade) {
        if (funcionalidade.perfilPossui) {
            $scope.perfil.perfilFuncionalidades.push({ funcionalidadeId: funcionalidade.id });
        }

        angular.forEach(funcionalidade.funcionalidadesFilho, function (funcionalidade, key) {
            $scope.preencheFuncionalidadesPerfil(funcionalidade);
        });
    }

    $scope.verificaPerfilPossui = function (funcionalidade) {

        funcionalidade.perfilPossui = false;
        angular.forEach($scope.perfil.perfilFuncionalidades, function (perfilFuncionalidade, key) {
            if (perfilFuncionalidade.funcionalidadeId == funcionalidade.id) {
                funcionalidade.perfilPossui = true;
            }
        });

        if (funcionalidade.funcionalidadesFilho) {
            angular.forEach(funcionalidade.funcionalidadesFilho, function (funcionalidade, key) {
                $scope.verificaPerfilPossui(funcionalidade);
            });
        }
    }

    $scope.verificaCheckPaiFilhos = function (funcionalidade) {
        if (funcionalidade.perfilPossui) {
            $scope.marcaFuncionalidadePai($scope.funcionalidadesDisponiveis, funcionalidade.funcionalidadeIdPai);
        }
        else {
            $scope.desmarcaFuncionalidadesFilho(funcionalidade);
        }
    }

    $scope.marcaFuncionalidadePai = function (funcionalidades, funcionalidadeIdPai) {
        angular.forEach(funcionalidades, function (funcionalidade, key) {
            if (funcionalidade.id == funcionalidadeIdPai) {
                funcionalidade.perfilPossui = true;
                $scope.marcaFuncionalidadePai($scope.funcionalidadesDisponiveis, funcionalidade.funcionalidadeIdPai);
            }
            else {
                $scope.marcaFuncionalidadePai(funcionalidade.funcionalidadesFilho, funcionalidadeIdPai);
            }
        });
    }

    $scope.desmarcaFuncionalidadesFilho = function (funcionalidade) {
        funcionalidade.perfilPossui = false;
        angular.forEach(funcionalidade.funcionalidadesFilho, function (funcionalidadeFilho, key) {
            $scope.desmarcaFuncionalidadesFilho(funcionalidadeFilho);
        });
    }

    $scope.total = 0;
    $scope.currentPage = 1;
    $scope.itemPerPage = 5;
    $scope.start = 0;
    $scope.maxSize = 5;
    $scope.pageChanged = function () {
        $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
    };
});