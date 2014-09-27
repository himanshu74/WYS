using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYS.BusinessLayer.BusinessHelpers;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.DataLayer.DAHelpers;
using log4net;

namespace WYS.BusinessLayer.BusinessObjects
{
    public class UserBo : BaseBo, IUserBo
    {
        #region Private

        private readonly  ILog _logger = LogManager.GetLogger(typeof(UserBo));

        #endregion


        #region User Properties

        public int UserId { get; set; }
        public int RoleId { get; set; }

        public int DomainId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateDeleted { get; set; }
        public DateTime DateModified { get; set; }

        public int IsDeleted { get; set; }
        #endregion


        #region User Implemented Methods

        public bool Save(string username, string password, string email, int roleId, int domainId)
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

        public bool CheckUsername(string username)
        {
            var userDao = DataAccess.UserDao;
            const bool available = true;
            const bool notAvail = false;
            try
            {
                var ds = userDao.CheckUsername(username);

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
                throw;
            }
            return available;
        }

        public string GetPassword(string username)
        {
            var userDao = DataAccess.UserDao;
            String password = null;

            try
            {
                var ds = userDao.GetPassword(username);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var user = MapperBo.ToUserBo(ds.Tables[0].Rows[0]);
                    password = user.Password;
                }
            }
            catch (Exception exception)
            {
                _logger.Error("ERROR IN CLASS =>>> USERBO, METHOD =>>> GetPassword, EXCEPTION MESSAGE =>> " + exception.Message);
 
                throw;
            }
            return password;
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
        public bool Update(string username, string password, int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
