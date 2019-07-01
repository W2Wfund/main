using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.TaskEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new ServiceReference1.Service1Client())
            {
                client.SendEmail(5, "taskm");
            }
        }
    }
}
