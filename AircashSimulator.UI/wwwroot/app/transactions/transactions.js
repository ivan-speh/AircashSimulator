var transactionsModule = angular.module('transactions', []);

app.config(function ($stateProvider) {
    $stateProvider
        .state('app.transactions', {
            data: {
                pageTitle: 'Transactions'
            },
            url: "/transactions",
            controller: 'TransactionsCtrl',
            templateUrl: 'app/transactions/transactions.html'
        });
});

transactionsModule.service("transactionsService", ['$http', '$q', 'handleResponseService', 'config', '$rootScope',
    function ($http, $q, handleResponseService, config, $rootScope)
    {
        return ({
            GetCoupons: GetCoupons,
            GetTransactionSums: GetTransactionSums,
            CancleCoupon: CancleCoupon
        });

        function GetCoupons(filter) {
            var request = $http({
                method: 'GET',
                url: config.baseUrl + "/coupon/GetCoupons",
                params: {
                    pageNumber: filter.pageNumber,
                    pageSize: filter.pageSize,
                    startDate: new Date(filter.startDate).toISOString(),
                    endDate: new Date(filter.endDate).toISOString(),
                    statusId: filter.statusId,
                    serialNumber: filter.serialNumber
                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function GetTransactionSums(startDate, endDate) {
            var request = $http({
                method: 'GET',
                url: config.baseUrl + "/coupon/GetTransactionSums",
                params: {
                    startDate: new Date(startDate).toISOString(),
                    endDate: new Date(endDate).toISOString()
                }
            });
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }

        function CancleCoupon(transactionId) {
            var request = $http({
                method: 'POST',
                url: config.baseUrl + "/coupon/CancelCoupon",
                data: {
                    transactionId: transactionId
                }
            });
            request.then(function (response) {
                if (response.status == 200) {
                    $rootScope.showGritter(
                        $rootScope.translations.gritter_success_title,
                        $rootScope.translations.coupon_canceled_success_message);
                }
            });
            
            return (request.then(handleResponseService.handleSuccess, handleResponseService.handleError));
        }
    }
]);

transactionsModule.controller("TransactionsCtrl",
    ['$scope', 'transactionsService', '$state', '$filter', '$http', 'JwtParser', '$uibModal', '$rootScope',
        function ($scope, transactionsService, $state, $filter, $http, JwtParser, $uibModal, $rootScope)
    {
        $scope.Currency = JwtParser.getProperty('currency');
        var date = new Date();
        date.setHours(0, 0, 0, 0);
        $scope.filter =
        {
            pageSize: 10,
            pageNumber: 1,
            startDate: date,
            endDate: date
        };
        $scope.coupons = [];
        $scope.panelStyle = "";
        $scope.transactionReports = {};

        $.fn.datepicker.defaults.format = "yyyy-mm-dd";
        $.fn.datepicker.defaults.weekStart = 1;

        $scope.Refresh = function () {
            $scope.filter.pageNumber = 1;
            transactionsService.GetCoupons($scope.filter)
                .then(function (response) {
                    $scope.coupons = response;
                });
            transactionsService.GetTransactionSums($scope.filter.startDate, $scope.filter.endDate)
                .then(function (response) {
                    $scope.transactionReports = response;
                });
        }

        $scope.Refresh();

        $scope.TogglePanel = function () {
            if (panelStyle == "")
                panelStyle = "display:none;";
            else
                panelStyle = "";
        }

        $scope.ShowMore = function () {
            $scope.filter.pageNumber += 1;
            transactionsService.GetCoupons($scope.filter)
                .then(function (response) {
                    $scope.coupons = $scope.coupons.concat(response);
                });
        }

        $scope.setStartDate = function (dateString) {
            let date = new Date(dateString);
            date.setHours(0, 0, 0, 0);
            $scope.filter.startDate = date;
        }

        $scope.setEndDate = function (dateString) {
            let date = new Date(dateString);
            date.setHours(0, 0, 0, 0);
            $scope.filter.endDate = date;
        }

        $scope.OpenCancleCouponModal = function (transactionId, serialNumber) {
            var modal = $uibModal.open({
                templateUrl: 'app/transactions/cancelCouponModal.html',
                controller: 'CancelCouponModal',
                backdrop: 'static',
                size: 'sm',
                animation: false,
                resolve: {
                    transactionsService: function () {
                        return transactionsService;
                    },
                    transactionId: function () {
                        return transactionId;
                    },
                    serialNumber: function () {
                        return serialNumber;
                    },
                    refreshTable: function () {
                        return $scope.Refresh;
                    }
                }
            });
        }
    }
]);

dashboardModule.controller('CancelCouponModal',
    function ($scope, $uibModalInstance, transactionsService, transactionId, serialNumber, refreshTable, $rootScope) {
        $scope.serialNumber = serialNumber;
        $scope.transactionId = transactionId;

        $scope.CancleCoupon = function (transactionId) {
            $scope.busy = true;
            transactionsService.CancleCoupon(transactionId)
                .then(function () {
                    refreshTable();
                    $scope.cancel();
                    $scope.busy = false;
                }, function () {
                    $scope.cancel();
                    $scope.busy = false;
                });
        }

        $scope.cancel = function () {
            $uibModalInstance.close();
        };
    }
);

transactionsModule.
    filter('toLocaleTimeString', function () {
        return function (date) {
            var offset = new Date().getTimezoneOffset();
            return new Date(new Date(date).getTime() - offset * 60000);
        };
    });

transactionsModule.
    filter('statusToLocale', function () {
        return function (status, scope) {
            return status == "1" ?
                scope.translations.created_status_label : 
                scope.translations.canceled_status_label;
        };
    });

transactionsModule.
    filter('isDisabled', function () {
        return function (status) {
            return status == "2";
        };
    });