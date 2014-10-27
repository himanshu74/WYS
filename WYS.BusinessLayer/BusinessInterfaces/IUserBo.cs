using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
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
        String Email { get; set; }
        String Password { get; set; }

        String VerificationCode { get; set; }

        String Token { get; set; }
        DateTime DateAdded { get; set; }
        DateTime DateDeleted { get; set; }
        DateTime DateModified { get; set; }

        int IsDeleted { get; set; }

        int IsVerified { get; set; }

        #endregion

        #region Methods

        bool Save(String username, String password, String email, int domainId, int roleId);
        List<IUserBo> Getall();

        IUserBo GetByid(int userId);

        bool CheckUsername(String username);

        IUserBo ValidateUser(String username);

        bool Delete(int userId);

        bool Update(String username, String password,String email, int userId, int roleId);


        bool UpdateToken(String token, String username);

        bool SaveVerificationCode(String code, String username);

        bool VerifyUser(String username, String code);

        bool SetUserVerified(String username);


        #endregion
    }
}
