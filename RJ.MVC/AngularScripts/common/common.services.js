(function(){
    //ngResource is the module containing the $resource service
    'use strict';
    angular
		.module('common.services', ['ngResource'])
		.constant('appSettings', {
		    serverPath: 'http://localhost:3312/'
		});
})();