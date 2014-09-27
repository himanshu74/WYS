using System;

namespace WYS.DataLayer.DAHelpers
{
   public static class StoredProcedure
    {
       public  const String RegisterUser= "register_user";
       public const int RegisterUserParameters = 5;

       public const String DeleteUser = "delete_user";
       public const int DeleteUserParameters = 1;

       public const String GetAllUsers = "get_all_users";
       public const int GetAllUsersParameters = 0;

       public const String GetUserById = "get_user_by_id";
       public const int GetUserByIdPara = 1;

       public const String CheckUsername = "get_user_by_username";
       public const int CheckUsernamePara = 1;

       public const String GetPassword = "get_user_password";
       public const int GetPasswordPara = 1;

       public const String UpdateToken = "update_token";
       public const int UpdateTokenPara = 2;

    }
}
