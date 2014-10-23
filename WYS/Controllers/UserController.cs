using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.BusinessLayer.BusinessObjects;
using WYS.DTOS;
using WYS.Helpers;



namespace WYS.Controllers
{
    public class UserController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserController));
        private static int Available = 0;
        private static int NotAvailable = 1;

        #region POST
        [HttpPost]
        public void Post([FromBody] UserDto userDto)
        {

            if (userDto != null)
            {
                try
                {
                    var encryptedPassword = PasswordManager.CreateHash(userDto.Password);


                    IUserBo userBo = new UserBo();
                    if (userBo.Save(userDto.Username, encryptedPassword, userDto.Email, userDto.DomainId, userDto.RoleId))
                    {
                        var verCode = AccountVerification.GenerateVerificationCode();
                        if (userBo.SaveVerificationCode(verCode, userDto.Username))
                        {
                            try
                            {
                                EmailManager.SendEmail(userDto.Email, AccountVerification.SignUpConfirmationSubject, AccountVerification.GetVerificationMessage(verCode));

                            }
                            catch (Exception exception)
                            {
                                var message = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                                {
                                    Content = new StringContent("Error Sending Verification Email")
                                };
                                Logger.Error("API LAYER: ERROR IN CLASS: UserController, METHOD: POST =>> EXCEPTION MESSAGE: " + exception.Message);
                                throw new HttpResponseException(message);
                            }

                        }
                    }
                }
                catch (Exception exception)
                {
                    var message = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent(exception.Message)
                    };
                    Logger.Error("API LAYER: ERROR IN CLASS: UserController, METHOD: POST =>> EXCEPTION MESSAGE: " + exception.Message);
                    throw new HttpResponseException(message);
                }

            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

        #endregion


        #region GET

        [HttpGet]
        public List<UserDto> Get()
        {
            List<UserDto> list;
            try
            {
                var userBo = new UserBo();
                var users = userBo.Getall();
                list = MapperApi.ToUserDtos(users);
            }
            catch (Exception exception)
            {
                var message = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent(exception.Message)
                };
                Logger.Error("API LAYER: ERROR IN CLASS: UserController, METHOD: GET =>> EXCEPTION MESSAGE: " + exception.Message);

                throw new HttpResponseException(message);
            }

            return list;
        }

        [HttpGet]
        public Object CheckUsername(string username)
        {
            if(String.IsNullOrEmpty(username))
           {
               var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Username is not provided") };
               throw new HttpResponseException(responseMessage);
           }
           else
           {
               Object status;
               try
               {
                   var userBo = new UserBo();
                   status = userBo.CheckUsername(username) ? Available : NotAvailable;
               }
               catch(Exception exception)
               {
                   var message = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                   {
                       Content = new StringContent(exception.Message)
                   };
                   Logger.Error("API LAYER: ERROR IN CLASS: UserController, METHOD: GET =>> EXCEPTION MESSAGE: " + exception.Message);
                   throw new HttpResponseException(message);
               }


               return status;
           }

        }



        #endregion


        [HttpDelete]
        public void Delete(int id)
        {

        }
    }
}
