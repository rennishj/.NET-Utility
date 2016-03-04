(function () {
    angular
        .module('productManagement')
        .controller('productEditCtrl', ['productResource', productEditCtrl]);
    function productEditCtrl(productResource) {        
        var vm = this;
        vm.product = {};
        vm.message = '';
        //get ,query and save are all defined on the Resource class in angular.Resource.get([parameters],success,error);
        //get returns a single object and query returns an array of objects
        productResource.get({ id: 0 }, function (data) {
            vm.product = data;
            console.log(vm.product);
            vm.originalProduct = angular.copy(data);
        }, function (response) {
            vm.message = response.responseMessage;
            if (response.data.exceptionMessage) {
                vm.message += response.data.exceptionMessage;
            }
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
                }, function (response) {
                    vm.message = response.statusText + '\r\n';
                });
            }
            else {
                vm.product.$save(function (data) {
                    vm.originalProduct = angular.copy(data);
                    vm.message = data.message;
                }, function (response) {
                    vm.message = response.statusText + '\r\n';
                    if (response.data.modelState) {
                        for (var key in response.data.modelState) {
                            vm.message += response.data.modelState[key] + '\r\n';
                        }
                    }
                    if (response.data.exceptionMessage) {
                        vm.message += response.data.exceptionMessage;
                    }
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