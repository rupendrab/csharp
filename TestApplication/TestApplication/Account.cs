using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Account
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<string> Roles { get; set; }

        public override string ToString()
        {
            return Email + ", " + Active + ", " + CreatedDate + ", " + string.Join(":", Roles);
        }
    }
}
