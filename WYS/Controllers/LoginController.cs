using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.BusinessLayer.BusinessObjects;
using WYS.Helpers;

namespace WYS.Controllers
{
    public class LoginController : BaseController
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof (LoginController));

      /*  [HttpGet]
        public Object Login(String username, String password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Username or Password is not provided") };
                throw new HttpResponseException(message);
            }

            String token = null;
            try
            {
                IUserBo userBo = new UserBo();
                String pass = userBo.ValidateUser( username);

                if (PasswordManager.ValidatePassword(password, pass))
                {
                     token = TokenManager.GetToken();
                    userBo.UpdateToken(token, username);
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
               _logger.Error("FRONT LAYER =>> CONTROLLER =>> LOGIN,  METHOD =>> LOGIN, EXCEPTION MESSAGE: "+exception.Message);
                HttpResponseHelper.GetInternalServerErrorResponse(exception);
            }

            return token;


        }*/







    }
}
