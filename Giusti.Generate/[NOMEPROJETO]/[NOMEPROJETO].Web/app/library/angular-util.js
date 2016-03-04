app.filter('inicioPaginacao', function () {
    return function (input, start) {
        if (!angular.isArray(input)) {
            return [];
        }
        start = +start; //parse to int
        return input.slice(start);
    };
});

app.filter('SimNao', function () {
    return function (input) {
        if (input == true) {
            return 'Sim';
        }
        else if (input == false) {
            return 'Não';
        }
    };
});

app.directive('toasterTemplateHtml', [function () {
    return {
        template: "<div ng-show='directiveData'><div ng-repeat='item in directiveData.Messages'>{{item.Message}}</div></div>"
    };
}])

app.factory('toasterAlert', function (toaster) {
    return {
        showAlert: function (alert) {
            try {
                var alert = JSON.parse(alert);
                this.show(alert);
            } catch (e) {
                var alert = { Sucess: "error", Messages: [{ Message: alert }] };
                this.show(alert);
            }
        },
        show: function(alert){
        toaster.pop({
            type: alert.Success ? "info" : "error",
            body: 'toaster-template-html',
            bodyOutputType: 'directive',
            directiveData: alert
            });
        }
    }
});