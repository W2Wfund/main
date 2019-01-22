using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2W.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var marketing = new Marketing)
            using (var client = new ServiceReference1.Service1Client())
            {
                client.PayInvestPercents(DateTime.Today, 5, "taskm");
            }
            

        }
    }
}
