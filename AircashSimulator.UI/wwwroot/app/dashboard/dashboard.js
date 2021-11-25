var dashboardModule = angular.module('dashboard', []);

app.config(function ($stateProvider) {
    $stateProvider
        .state('app.dashboard', {
            data: {
                pageTitle: 'Dashboard'
            },
            url: "/dashboard",
            controller: 'DashboardCtrl',
            templateUrl: 'app/dashboard/dashboard.html'
        });
});

dashboardModule.controller("DashboardCtrl",
    ['$scope', '$state', '$filter', '$http', 'JwtParser', '$uibModal', '$rootScope',
        function ($scope, dashboardService, $state, $filter, $http, JwtParser, $uibModal, $rootScope)
    {
        //$scope.Currency = JwtParser.getProperty('currency');
        
    }
]);