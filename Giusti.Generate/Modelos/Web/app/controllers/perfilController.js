app.controller('perfilController', function ($scope, $http, $window, toasterAlert, $location, $uibModal, $routeParams) {

    var mensagemExcluir = 'Deseja realmente excluir o perfil [NOMEPERFIL] ?';
    var mensagemSalvo = JSON.stringify({ Success: "info", Messages: [{ Message: 'Perfil salvo com sucesso.' }] });
    $scope.heading = 'Perfis';
    $scope.perfis = [];
    $scope.perfil = null;

    //GET API
    var url = 'api/perfil';
    $scope.getPerfis = function () {

        $http.get(url).success(function (data) {
            $scope.perfis = data;
            $scope.total = $scope.perfis.length;
        }).error(function (jqxhr, textStatus) {
            toasterAlert.showAlert(jqxhr.message);
        })
    };

    //DELETE API
    $scope.deletePerfil = function () {

        $http.delete(url + '/' + $scope.perfil.id).success(function (result) {
            toasterAlert.showAlert(result);
            $scope.perfis.splice($scope.perfis.indexOf($scope.perfil), 1);
        }).error(function (result) {
            toasterAlert.showAlert(result);
        });
    };

    //MODAL DELETE
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