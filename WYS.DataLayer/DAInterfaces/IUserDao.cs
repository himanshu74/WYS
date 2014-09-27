using System;
using System.Data;
using Microsoft.SqlServer.Server;

namespace WYS.DataLayer.DAInterfaces
{
    public interface IUserDao
    {

        bool Save(String username, String password,String email, int domainId, int userRole);
        DataSet GetAll();
        DataSet GetById(int userId);

        DataSet CheckUsername(String username);

        DataSet GetPassword(String username);

        bool UpdateToken(String token, String username);

        bool Update(String username, String password, int userId, int userRole);
        bool Delete(int userId);


    }
}
