using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Paper : ILock
    {
        public void Lock()
        {
            Console.WriteLine("Paper is locked");
        }

        public void Unlock()
        {
            Console.WriteLine("Paper is unocked");
        }
    }
}
