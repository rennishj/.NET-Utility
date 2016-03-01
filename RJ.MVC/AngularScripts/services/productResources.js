(function(){
    'use strict';
    angular
        .module('common.services')
        .factory('productResource', ['$resource', 'appSettings', productResource]);    
    function productResource($resource, appSettings) {
        console.log(appSettings.serverPath + 'api/products/:id')
        return $resource(appSettings.serverPath + 'products/all');//: denotes an optional parameter
    }
})();