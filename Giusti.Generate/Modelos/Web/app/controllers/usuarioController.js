app.controller('usuarioController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams) {

    var mensagemExcluir = 'Deseja realmente excluir o usuario [NOMEUSUARIO] ?';
    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Usuário salvo com sucesso.' }] });
    $scope.heading = 'Usuários';
    $scope.usuarios = [];
    $scope.usuario = null;

    //GET API
    var url = 'api/usuario';
    $scope.getUsuarios = function () {

        $http.get(url).success(function (data) {
            $scope.usuarios = data;
            $scope.total = $scope.usuarios.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    // GET API
    $scope.getUsuario = function () {
        if (!angular.isUndefined($routeParams.id)) {
            $scope.id = $routeParams.id;
        }

        $http.get(url + '/' + $scope.id).success(function (data) {
            $scope.usuario = data;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    //POST API
    $scope.postUsuario = function () {

        $http.post(url, JSON.stringify($scope.usuario)).success(function (id) {
            $scope.id = id;
            $scope.getUsuario();
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    //PUT API
    $scope.putUsuario = function () {

        $http.put(url + '/' + $scope.id, JSON.stringify($scope.usuario)).success(function (data) {
            $scope.usuario = data;
            toasterAlert.showAlert(mensagemSalvo);
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        });
    };

    //DELETE API
    $scope.deleteUsuario = function () {

        $http.delete(url + '/' + $scope.usuario.codigo).success(function (result) {
            toasterAlert.showAlert(result);
            toasterAlert.usuarios.splice($scope.usuarios.indexOf($scope.usuario), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    //ADD USUARIO
    $scope.addUsuario = function () {
        $scope.usuario = { id: null };
    };

    //MODAL DELETE
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
