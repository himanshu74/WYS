using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using WYS.BusinessLayer.BusinessHelpers;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.DataLayer.DAHelpers;
using log4net;
using WYS.DataLayer.DAObjects;

namespace WYS.BusinessLayer.BusinessObjects
{
    public class UserBo : BaseBo, IUserBo
    {
        #region Private

        private readonly ILog _logger = LogManager.GetLogger(typeof(UserBo));

        #endregion


        #region User Properties

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public int DomainId { get; set; }

        public String Username { get; set; }
        public string Password { get; set; }
        public String Email { get; set; }

        public String VerificationCode { get; set; }
        public String Token { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateDeleted { get; set; }
        public DateTime DateModified { get; set; }

        public int IsDeleted { get; set; }
        public int IsVerified { get; set; }

        #endregion


        #region User Implemented Methods

        public bool Save(String username, String password, String email, int domainId, int roleId)
        {
            bool isSaved = false;
            var userDao = DataAccess.UserDao;

            try
            {
                var status = userDao.Save(username, password, email, domainId, roleId);
                if (status)
                {
                    isSaved = true;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> SAVE, EXCEPTION MESSAGE =>> " + exception.Message);
                throw;
            }

            return isSaved;
        }

        public List<IUserBo> Getall()
        {
            var users = new List<IUserBo>();
            var userDao = DataAccess.UserDao;

            try
            {
                var ds = userDao.GetAll();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var user = MapperBo.ToUserBo(ds.Tables[0].Rows[i]);
                        users.Add(user);
                    }
                }

            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> Getall, EXCEPTION MESSAGE =>> " + exception.Message);
                throw;
            }
            return users;
        }

        public IUserBo GetByid(int userId)
        {
            var userDao = DataAccess.UserDao;
            IUserBo user = null;
            try
            {
                var ds = userDao.GetById(userId);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    user = MapperBo.ToUserBo(ds.Tables[0].Rows[0]);
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> GetByid, EXCEPTION MESSAGE =>> " + exception.Message);
                throw;
            }

            return user;
        }

        public bool CheckUsername(String username)
        {
            var userDao = DataAccess.UserDao;
            const bool available = true;
            const bool notAvail = false;
            try
            {
                var ds = userDao.GetUserByUsername(username);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var user = MapperBo.ToUserBo(ds.Tables[0].Rows[0]);
                    if (user.Username.Trim().ToLower().Equals(username.Trim().ToLower()))
                    {
                        return notAvail;
                    }

                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> CheckUsername, EXCEPTION MESSAGE =>> " + exception.Message);

                if (exception.Message.Equals("no user exists with this username"))
                {
                    return available;
                }
                return notAvail;
            }
            return available;
        }

        public IUserBo ValidateUser(String username)
        {
            var userDao = DataAccess.UserDao;
            UserBo user = null;

            try
            {
                var ds = userDao.GetUserByUsername(username);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                     user = MapperBo.ToUserBo(ds.Tables[0].Rows[0]);
                   
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> GetPassword, EXCEPTION MESSAGE =>> " + exception.Message);

                throw;
            }
            return user;
        }

        public bool Delete(int userId)
        {
            var status = false;
            var userDao = DataAccess.UserDao;

            try
            {
                status = userDao.Delete(userId);

            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> Delete, EXCEPTION MESSAGE =>> " + exception.Message);
                throw;
            }
            return status;
        }

        public bool UpdateToken(String token, String username)
        {
            var userDao = DataAccess.UserDao;
            bool status = false;
            try
            {
                if (userDao.UpdateToken(token, username))
                {
                    status = true;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> UpdateToken, EXCEPTION MESSAGE =>> " + exception.Message);

                throw;
            }
            return status;
        }
        public bool Update(String username, String password, String email, int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public bool SaveVerificationCode(String code, String username)
        {
            bool status = false;
            var userDao = DataAccess.UserDao;

            try
            {
                if (userDao.SaveVerificationCode(code, username))
                {
                    status = true;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> SaveVerifictionCode, EXCEPTION MESSAGE =>> " + exception.Message);
                throw;
            }

            return status;
        }

        public bool VerifyUser(String username, String code)
        {
            var userDao = DataAccess.UserDao;
            const bool verified = true;
            const bool notVerified = false;

            try
            {
                var ds = userDao.GetUserByUsername(username);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var user = MapperBo.ToUserBo(ds.Tables[0].Rows[0]);
                    if (user.VerificationCode.Trim().ToLower().Equals(code.Trim().ToLower()))
                    {
                        return verified;
                    }

                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> VerifyUser, EXCEPTION MESSAGE =>> " + exception.Message);

                if (exception.Message.Equals("user doesnt exists"))
                {
                    return notVerified;
                }
                return notVerified;
            }
            return notVerified;
        }

        public bool SetUserVerified(string username)
        {
            var userDao = DataAccess.UserDao;
            bool isUpdated = false;


            try
            {
                if (userDao.SetUserVerified(username))
                {
                    isUpdated = true;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> SetUserVerified, EXCEPTION MESSAGE =>> " +
                              exception.Message);

            }


            return isUpdated;
        }


        #endregion
    }
}
