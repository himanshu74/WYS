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

           if (row.Table.Columns.Contains("email"))
               userBo.Email = row["email"].ToString();

           if (row.Table.Columns.Contains("password"))
               userBo.Password = row["password"].ToString();

           if (row.Table.Columns.Contains("role_id"))
               userBo.RoleId = row["role_id"].ToInt32();

           if (row.Table.Columns.Contains("domain_id"))
               userBo.DomainId = row["domain_id"].ToInt32();

           if (row.Table.Columns.Contains("verification_code"))
               userBo.VerificationCode = row["verification_code"].ToString();

           if (row.Table.Columns.Contains("token"))
               userBo.Token = row["token"].ToString();

           if (row.Table.Columns.Contains("date_added"))
               userBo.DateAdded = row["date_added"].ToDateTime();

           if (row.Table.Columns.Contains("date_modified"))
               userBo.DateModified = row["date_modified"].ToDateTime();

           if (row.Table.Columns.Contains("date_deleted"))
               userBo.DateDeleted = row["date_deleted"].ToDateTime();

           if (row.Table.Columns.Contains("is_deleted"))
               userBo.IsDeleted = row["is_deleted"].ToInt32();

           if (row.Table.Columns.Contains("is_verified"))
               userBo.IsVerified = row["is_verified"].ToInt32();

           return userBo;
       } 



       #endregion


   }
}
