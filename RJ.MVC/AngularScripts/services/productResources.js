(function(){
    'use strict';
    angular
        .module('common.services')
        .factory('productResource', ['$resource', 'appSettings', productResource]);    
    function productResource($resource, appSettings) {
        return $resource(appSettings.serverPath + 'api/product/:id', null, {
            'update': {method:'PUT'}
        });
    }
    
})();

/*As a side note,the module you are creating the service on should have the ngResource module 
  as a dependency,in order for you to use the $resource service in the service you are creating
  
  $resource(url,value for Default Parameters,set of custom actions);
  This is the usage from angular documentation page $resource(url, [paramDefaults], [actions], options);(At least for v1.5.1)
  */
