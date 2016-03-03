(function () {
    angular
        .module('productManagement')
        .controller('productEditCtrl', ['productResource', productEditCtrl]);
    function productEditCtrl(productResource) {
        var vm = this;
        vm.product = {};
        vm.message = '';

        productResource.get({ id: 5 }, function (data) {
            vm.product = data;
            vm.originalProduct = angular.copy(data);
        });

        if (vm.product && vm.product.productId) {
            vm.title = 'Edit Product';
        }
        else
        {
            vm.title = 'New Product';
        }

        vm.submit = function () {
                productResource.save({product:vm.product},function(data){
                    vm.message = 'Product saved'
                });
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.product = angular.copy(vm.originalProduct);
            vm.message = '';
        };
    }
        
})();