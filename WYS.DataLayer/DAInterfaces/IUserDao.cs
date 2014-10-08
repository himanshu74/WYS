using System;
using System.Data;
using Microsoft.SqlServer.Server;

namespace WYS.DataLayer.DAInterfaces
{
    public interface IUserDao
    {

        bool Save(String email, String password, int domainId, int userRole);
        DataSet GetAll();
        DataSet GetById(int userId);

        DataSet CheckUsername(String email);

        DataSet GetPassword(String email);

        bool UpdateToken(String token, String email);

        bool Update(String email, String password, int userId, int userRole);
        bool Delete(int userId);


    }
}
