(function(){
    'use strict';
    angular
        .module('common.services')
        .factory('productResource', ['$resource', 'appSettings','currentUser', productResource]);    
    function productResource($resource, appSettings, currentUser) {
        return $resource(appSettings.serverPath + 'api/product/:id', null, {
            'get': {
                headers: {'Authorization' : 'Bearer ' + currentUser.getProfile().token}
            },
            'save':{
                headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
            },
            'query':{
                headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
            },
            'update': { method: 'PUT', headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token } }
        });
    }
    
})();

/*As a side note,the module you are creating the service on should have the ngResource module 
  as a dependency,in order for you to use the $resource service in the service you are creating
  
  $resource(url,value for Default Parameters,set of custom actions);
  This is the usage from angular documentation page $resource(url, [paramDefaults], [actions], options);(At least for v1.5.1)
  */
