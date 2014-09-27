using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYS.DTOS
{
    public class UserDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        public int DomainId { get; set; }

        public String Username { get; set; }
        public String Password { get; set; }

        public String Email { get; set; }
        public int IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateDeleted { get; set; }

    }
}
