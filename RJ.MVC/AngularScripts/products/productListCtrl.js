(function () {
    'use strict';
    angular
        .module('productManagement')
        .controller('productListCtrl', ['productResource', productListCtrl]);
    function productListCtrl(productResource) {
        var vm = this;

        /*This is for url with multiple query string values
        vm.searchCriteria = 'W';
        vm.description = 'Intro';
        productResource.query({ search: vm.searchCriteria, description: vm.description }, function (data) {
            vm.productList = data;
        }); */

        //This is for passing the search params as a Url segment and not as a query string
        //$resource.query method issues an HTTP Get request
        vm.searchCriteria = 'IWCF';
        productResource.query({ search: vm.searchCriteria }, function (data) {
            vm.productList = data;
        });
    }
})();