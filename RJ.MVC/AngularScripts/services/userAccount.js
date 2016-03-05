(function()
{
  'use strict';
  angular
    .module('productManagement')
    .factory('userAccount', ['$resource', 'appSettings', userAccount]);

  function userAccount($resource,appSettings) {
      return {
          registration: $resource(appSettings.serverPath + 'api/Account/Register',null,{
              'registerUser': {
                  method:'POST'
              }
          }),// /Token url comes from the busilt in code asp.net generates with individual useraccount selected from the visula studio template
          login: $resource(appSettings.serverPath + '/Token', null, {
              'loginUser': {
                  method: 'POST',
                  //for login,this has to be set
                  headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                  //by default,$resouce service converts the request body into json and we dont want that.We want that to be url encoded,hence the following function
                  transformRequest: function (data, headersGetter) {
                      var str = [];
                      for (var d in data) {                          
                          str.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
                      }                      
                      return str.join('&');
                  }
              }
          })
      };
  }
})();

