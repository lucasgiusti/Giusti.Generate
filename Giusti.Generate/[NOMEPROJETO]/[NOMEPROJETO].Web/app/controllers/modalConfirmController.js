app.controller('modalConfirmInstanceController', function ($scope, $uibModalInstance, dadosModalConfirm) {
    $scope.dadosModalConfirm = dadosModalConfirm;
    $scope.confirmar = function () {
        $uibModalInstance.close();
    };

    $scope.cancelar = function () {
        $uibModalInstance.dismiss('cancel');
    };
});