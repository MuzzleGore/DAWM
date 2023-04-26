using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class User : BaseEntity
    {

        public string Email { get; set; }
        public string PasswordHash { get; set; }
    
        public Role Role { get; set; }
    }
}
