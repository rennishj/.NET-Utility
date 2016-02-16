using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using RJ.Poco;

namespace RJ.DAL
{
   public class MenuAccess
    {
       public SqlConnection Connection
       {
           get
           {
               return ConnectionHelper.ConnectionHelper.GetConnectionString();
           }
       }       
       public async Task<List<MenuItem>> GetAllMenuItems()
       {
           using (var con = this.Connection)
           {
               await con.OpenAsync();
               var menuItems = await con.QueryAsync<MenuItem>("select MenuITemId,ParentId,DisplayName,Url,MenuItemTypeId from dbo.MenuItem");
               return menuItems.ToList();
           }
       }
    }
}
