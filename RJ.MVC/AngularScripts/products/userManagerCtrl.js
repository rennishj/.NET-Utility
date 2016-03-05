(function(){
    'use strict';
    angular
        .module('productManagement')
        .controller('userManagerCtrl', ['userResource',userManagerCtrl]);

    function userManagerCtrl(userResource) {
        var vm = this;
        vm.user = {};
        vm.user.bearerToken = '';
        vm.message = '';
        vm.register = function () {
            userResource.save({}, function (data) {

            }, registrationError);
        };

        vm.login = function () {

        };

        function registrationError(response) {
            vm.message = 'Error occurred while registering the user';
        }
    }

})();