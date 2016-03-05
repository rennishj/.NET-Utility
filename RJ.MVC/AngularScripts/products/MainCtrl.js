(function(){
    'use strict';
    angular
        .module('productManagement')
        .controller('mainCtrl', ['userAccount','currentUser', mainCtrl]);

    function mainCtrl(userAccount, currentUser) {
        var vm = this;
        vm.userData = {
            userName: '',
            email: '',
            password:'',
            confirmPassword:''
        };
        vm.isLoggedIn = function () {
            return currentUser.getProfile().isLoggedIn;
        };
        vm.message = '';
        vm.registerUser = function () {
            vm.userData.userName = vm.userData.email;
            vm.userData.confirmPassword = vm.userData.password;
            userAccount.registration.registerUser(vm.userData, function (data) {
                vm.message = 'User created successfully';
                vm.isLoggedIn = true;
            }, registrationError);
        };

        vm.login = function () {
            vm.userData.grant_type = 'password';//grant_type set to password for token based authentication.This means that the client will pass in a username and password
            vm.userData.userName = vm.userData.email;
            userAccount.login.loginUser(vm.userData, loginSuccess, loginError);
        };

        function registrationError(response) {
            if (response.data.modelState) {
                for (var key in response.data.modelState) {
                    vm.message += response.data.modelState[key] + '\r\n';
                }
            }
        }
        function loginSuccess(data) {           
            vm.message = '';
            vm.password = '';            
            currentUser.setProfile(vm.userData.userName, data.access_token);
        }
        function loginError(response) {
            vm.password = '';            
            vm.message = response.statusText + '\r\n';
            if (response.data.exceptionMessage) {
                vm.message += response.data.exceptionMessage;
            }
            if (response.data.error) {
                vm.message += response.data.error;
            }
        }

    }

})();