using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            int th = CLIHelper.GetDays("enter");
            Console.WriteLine(th);

            //CapstoneCLI c = new CapstoneCLI();
            //c.Run();
        }
    }
}
