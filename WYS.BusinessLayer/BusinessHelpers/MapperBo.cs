using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYS.BusinessLayer.BusinessObjects;

namespace WYS.BusinessLayer.BusinessHelpers
{
   public static class MapperBo
   {

       #region Mapping Data from UserDao to UserBo

       public static UserBo ToUserBo(DataRow row)
       {
           var userBo = new UserBo();
           if (row.Table.Columns.Contains("user_id"))
               userBo.UserId = row["user_id"].ToInt32();

           if (row.Table.Columns.Contains("username"))
               userBo.Username = row["username"].ToString();

           if (row.Table.Columns.Contains("password"))
               userBo.Password = row["password"].ToString();

           if (row.Table.Columns.Contains("role_id"))
               userBo.RoleId = row["role_id"].ToInt32();

           if (row.Table.Columns.Contains("domain_id"))
               userBo.DomainId = row["domain_id"].ToInt32();

           if (row.Table.Columns.Contains(""))
               userBo.Email = row["email"].ToString();

           if (row.Table.Columns.Contains(""))
               userBo.Token = row["token"].ToString();

           if (row.Table.Columns.Contains(""))
               userBo.DateAdded = row["date"].ToDateTime();

           if (row.Table.Columns.Contains(""))
               userBo.DateModified = row["date_modified"].ToDateTime();

           if (row.Table.Columns.Contains(""))
               userBo.DateDeleted = row["date_deleted"].ToDateTime();

           if (row.Table.Columns.Contains(""))
               userBo.IsDeleted = row["is_deleted"].ToInt32();

           return userBo;
       } 



       #endregion


   }
}
