using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WYS.BusinessLayer.BusinessInterfaces;
using WYS.BusinessLayer.BusinessObjects;
using WYS.DTOS;
using WYS.Helpers;



namespace WYS.Controllers
{
    public class UserController : BaseController
    {



        [HttpPost]
        public void Post([FromBody] UserDto userDto)
        {

            if (userDto != null)
            {
                try
                {
                    var encryptedPassword  = PasswordManager.CreateHash(userDto.Password);


                    IUserBo userBo = new UserBo();
                    userBo.Save(userDto.Email, encryptedPassword, userDto.DomainId, userDto.RoleId);
                }
                catch (Exception exception)
                {
                    var message = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent(exception.Message)
                    };

                    throw new HttpResponseException(message);
                }

            }
            else
            {
                
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
           
        }







        [HttpGet]
        public void Get()
        {
          

        }

        [HttpDelete]
        public void Delete(int id)
        {
            
        }
    }
}
