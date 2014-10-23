using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WYS.Helpers
{
    public static class AccountVerification
    {
        public const String SignUpConfirmationSubject = "WYS-Account-CONFIRMATION";

        public static String GenerateVerificationCode()
        {
            const string codeBlock = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(
             Enumerable.Repeat(codeBlock, 4)
              .Select(s => s[random.Next(s.Length)])
              .ToArray());
            return code;
        }


        public static String GetVerificationMessage(String code)
        {
            return
                String.Format(
                    "Thanks for Signing up with WYS{0}{1}Please enter the following verification code in your Android Application{2}{3}VERIFICATION CODE : {4} ",
                    Environment.NewLine, Environment.NewLine, Environment.NewLine, Environment.NewLine, code); 
                   
                  
        }
    }
}