using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYS.DataLayer.DAInterfaces;
using WYS.DataLayer.DAObjects;

namespace WYS.DataLayer.DAHelpers
{
    public static class DataAccess
    {
        public static IUserDao UserDao
        {
            get { return new UserDao();}
        }


    }
}
