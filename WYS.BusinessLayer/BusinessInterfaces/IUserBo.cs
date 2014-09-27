using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.SqlServer.Server;

namespace WYS.BusinessLayer.BusinessInterfaces
{
    public interface IUserBo
    {
        #region Properties

        int UserId { get; set; }
        int RoleId { get; set; }

        int DomainId { get; set; }
        String Username { get; set; }
        String Password { get; set; }

        String Email { get; set; }

        String Token { get; set; }
        DateTime DateAdded { get; set; }
        DateTime DateDeleted { get; set; }
        DateTime DateModified { get; set; }

        int IsDeleted { get; set; }

        #endregion


        #region Methods

        bool Save(String username, String password, string email, int domainId, int roleId);
        List<IUserBo> Getall();

        IUserBo GetByid(int userId);

        bool CheckUsername(String username);

        String GetPassword(String username);

        bool Delete(int userId);

        bool Update(String username, String password, int userId, int roleId);


        bool UpdateToken(String token, String username);

        #endregion
    }
}
