(function()
{
    'use strict';
    angular
    .module('productManagement')
    .factory('userResource', ['$resource','appSettings', userResource]);

    function userResource($resource, appSettings) {
        return $resource(appSettings.serverPath + 'api/Account/Register', null, {
            'register': {},
            'login': {}
        });
    }
})();

