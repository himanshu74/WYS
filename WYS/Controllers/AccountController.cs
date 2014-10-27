using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using log4net;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.BusinessLayer.BusinessObjects;
using WYS.Helpers;

namespace WYS.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(AccountController));
        private const int Verified = 1;
        private const int NotVerified = 0;



        [HttpGet]
        public Object VerifyAccount(String username, String code)
        {
            if (String.IsNullOrEmpty(username))
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Username cannot be empty") };
                throw new HttpResponseException(message);
            }
            else if (String.IsNullOrEmpty(code))
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Verification Code cannot be empty")
                };
                throw new HttpResponseException(message);
            }
            else
            {
                Object status;
                try
                {
                    var userBo = new UserBo();
                    if (userBo.VerifyUser(username, code))
                    {
                        status = userBo.SetUserVerified(username) ? Verified : NotVerified;

                    }
                    else
                    {
                        status = NotVerified;
                    }

                }
                catch (Exception exception)
                {
                    var message = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent(exception.Message)
                    };
                    _logger.Error("API LAYER: ERROR IN CLASS: UserController, METHOD: GET =>> EXCEPTION MESSAGE: " + exception.Message);
                    throw new HttpResponseException(message);
                }

                return status;
            }



        }



        [HttpGet]
        public UserBo Login(String username, String password)
        {

           
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Username or Password is not provided") };
                throw new HttpResponseException(message);
            }

            try
            {
                IUserBo userBo = new UserBo();
                IUserBo user = userBo.ValidateUser(username);

                if (PasswordManager.ValidatePassword(password, user.Password))
                {
                    if (user.IsVerified == Verified)
                    {
                        String token = TokenManager.GetToken();

                        if (userBo.UpdateToken(token, username))
                        {
                            user.Token = token;
                            user.Password = password;
                            return  (UserBo) user;
                            
                        }
                        
                    }

                    else
                    {
                        return ((UserBo) user);
                    }

                }
                else
                {
                    var message = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("Username or Password is Incorrect")
                    };
                    throw new HttpResponseException(message);
                }

            }
            catch (Exception exception)
            {
                _logger.Error("FRONT LAYER =>> CONTROLLER =>> LOGIN,  METHOD =>> LOGIN, EXCEPTION MESSAGE: " + exception.Message);
                HttpResponseHelper.GetInternalServerErrorResponse(exception);
            }

            return null;

        }






    }
}
