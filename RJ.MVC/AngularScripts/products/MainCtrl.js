(function(){
    'use strict';
    angular
        .module('productManagement')
        .controller('mainCtrl', ['userAccount', mainCtrl]);

    function mainCtrl(userAccount) {
        var vm = this;
        vm.userData = {
            userName: '',
            email: '',
            password:'',
            confirmPassword:''
        };        
        vm.userData.bearerToken = '';
        vm.isLoggedIn = false;
        vm.message = '';
        vm.registerUser = function () {
            vm.userData.userName = vm.userData.email;
            vm.userData.confirmPassword = vm.userData.password;
            userAccount.registerUser(vm.userData, function (data) {
                vm.message = 'User created successfully';
                vm.isLoggedIn = true;
            }, registrationError);
        };

        vm.login = function () {
            //if success
            vm.isLoggedIn = true;
        };

        function registrationError(response) {
            if (response.data.modelState) {
                for (var key in response.data.modelState) {
                    vm.message += response.data.modelState[key] + '\r\n';
                }
            }
        }
    }

})();