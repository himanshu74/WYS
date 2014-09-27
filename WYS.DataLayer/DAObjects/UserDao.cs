using System;
using System.Data;
using System.Data.SqlClient;
using WYS.DataLayer.DAHelpers;
using WYS.DataLayer.DAInterfaces;
using WYS.DataLayer.SqlHelper;
using log4net;

namespace WYS.DataLayer.DAObjects
{
    public class UserDao:BaseDao,IUserDao
    {
        #region Private Members

        private readonly ILog _logger = LogManager.GetLogger(typeof (UserDao));

        #endregion


        #region Implemented Methods

        public bool Save(string username, string password, String email, int domainId, int userRole)
        {
            bool isSaved = false;

            var sp = new SqlParameterHelper(StoredProcedure.RegisterUser, StoredProcedure.RegisterUserParameters);
            sp.DefineSqlParameter("@username",SqlDbType.VarChar, ParameterDirection.Input,username);
            sp.DefineSqlParameter("@password",SqlDbType.VarChar, ParameterDirection.Input, password);
            sp.DefineSqlParameter("@email",SqlDbType.VarChar, ParameterDirection.Input,email );
            sp.DefineSqlParameter("@domain_id",SqlDbType.Int, ParameterDirection.Input,domainId);
            sp.DefineSqlParameter("@role_id", SqlDbType.Int, ParameterDirection.Input, userRole);

            try
            {
                int affectedRows = sp.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    isSaved = true;
                }
            }
            catch (SqlException exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> Save, EXCEPTION MESSAGE =>> "+exception.Message);
                throw new Exception(exception.Message);
            }
            return isSaved;
        }

        public DataSet GetAll()
        {
            DataSet ds;
            var sp = new SqlParameterHelper(StoredProcedure.GetAllUsers, StoredProcedure.GetAllUsersParameters);

            try
            {
                ds = sp.ExecuteDataset();
            }
            catch (SqlException exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> GetAll, EXCEPTION MESSAGE =>> " + exception.Message);
                throw new Exception(exception.Message);
            }
            return ds;
        }

        public DataSet GetById(int userId)
        {
            DataSet ds;
            var sp = new SqlParameterHelper(StoredProcedure.GetUserById, StoredProcedure.GetUserByIdPara);
            sp.DefineSqlParameter("@user_id",SqlDbType.Int, ParameterDirection.Input, userId);

            try
            {
                ds = sp.ExecuteDataset();
            }
            catch(SqlException exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> GetById, EXCEPTION MESSAGE =>>" + exception.Message);
              throw new Exception(exception.Message);
  
            }
            return ds;
        }

        public DataSet CheckUsername(string username)
        {
            DataSet ds;
            var sp = new SqlParameterHelper(StoredProcedure.CheckUsername,StoredProcedure.CheckUsernamePara);
            sp.DefineSqlParameter("@username",SqlDbType.VarChar, ParameterDirection.Input,username);

            try
            {
                ds = sp.ExecuteDataset();
            }
            catch (SqlException exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> CheckUsername, EXCEPTION MESSAGE =>> " + exception.Message);
                throw new Exception(exception.Message);
            }
            return ds;
        }

        public DataSet GetPassword(string username)
        {
            DataSet ds;
            var sp = new SqlParameterHelper(StoredProcedure.GetPassword, StoredProcedure.GetPasswordPara);
            sp.DefineSqlParameter("@username", SqlDbType.VarChar, ParameterDirection.Input, username);
          

            try
            {
                ds = sp.ExecuteDataset();
            }
            catch (SqlException exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> GetPassword, EXCEPTION MESSAGE =>> " + exception.Message);
                throw new Exception(exception.Message);
            }
            return ds;
        }

        public bool UpdateToken(string token, string username)
        {
            bool status = false;
            var sp = new SqlParameterHelper(StoredProcedure.UpdateToken, StoredProcedure.UpdateTokenPara);
            sp.DefineSqlParameter("@token", SqlDbType.VarChar, ParameterDirection.Input,token);
            sp.DefineSqlParameter("@username",SqlDbType.VarChar, ParameterDirection.Input, username);


            try
            {
                int affectedRows = sp.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    status = true;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> UpdateToken, EXCEPTION MESSAGE =>> " + exception.Message);
                 throw new Exception();
            }
            return status;
        }


        public bool Update(string username, string password, int userId, int userRole)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int userId)
        {
            bool isDeleted = false;
            var sp = new SqlParameterHelper(StoredProcedure.DeleteUser, StoredProcedure.DeleteUserParameters);
            sp.DefineSqlParameter("@user_id",SqlDbType.Int, ParameterDirection.Input, userId);

            try
            {
                int affectedRows = sp.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    isDeleted = true;
                }
            }
            catch (SqlException exception)
            {
                _logger.Error("ERROR IN CLASS =>> USERDAO, METHOD =>> Delete, EXCEPTION MESSAGE =>> " + exception.Message);
                throw  new Exception(exception.Message);
            }
            return isDeleted;
        }


        #endregion
    }
}
