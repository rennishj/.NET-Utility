(function () {
    angular
        .module('productManagement')
        .controller('productEditCtrl', ['productResource', productEditCtrl]);
    function productEditCtrl(productResource) {        
        var vm = this;
        vm.product = {};
        vm.message = '';

        productResource.get({ id: 0 }, function (data) {
            vm.product = data;
            console.log(vm.product);
            vm.originalProduct = angular.copy(data);
        });

        if (vm.product && vm.product.productId) {
            vm.title = 'Edit Product';
        }
        else
        {
            vm.title = 'New Product';
        }
        //$resource.save issues an HTTP POST request and sends the data in the request Body
        vm.submit = function () {
            vm.message = '';
            if (vm.product.productId) {
                vm.product.$update({ id: vm.product.productId }, function (data) {
                    vm.message = 'Product Updated Successfully';
                })
            }
            else {
                vm.product.$save(function (data) {
                    vm.originalProduct = angular.copy(data);
                    vm.message = data.message;
                });
            }
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.product = angular.copy(vm.originalProduct);
            vm.message = '';
        };
    }
        
})();