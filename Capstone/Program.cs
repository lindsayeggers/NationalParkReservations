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
            Console.WriteLine(@"              ,,))))))));,                                                 ");
            Console.WriteLine(@"           __)))))))))))))),                                               ");
            Console.WriteLine(@"\|/       -\(((((''''((((((((.                                             ");
            Console.WriteLine(@"-*-==//////((''  .     `)))))),                                            ");
            Console.WriteLine(@"/|\      ))| o    ;-.    '(((((                                  ,(,       ");
            Console.WriteLine(@"         ( `|    /  )    ;))))'                               ,_))^;(~     ");
            Console.WriteLine(@"            |   |   |   ,))((((_     _____------~~~-.        %,;(;(>';'~   ");
            Console.WriteLine(@"            o_);   ;    )))(((` ~---~  `::           \      %%~~)(v;(`('~  ");
            Console.WriteLine(@"                  ;    ''''````         `:       `:::|\,__,%%    );`'; ~   ");
            Console.WriteLine(@"                 |   _                )     /      `:|`----'     `-'       ");
            Console.WriteLine(@"           ______/\/~    |                 /        /                      ");
            Console.WriteLine(@"         /~;;.____/;;'  /          ___--,-(   `;;;/                        ");
            Console.WriteLine(@"        / //  _;______;'------~~~~~    /;;/\    /                          ");
            Console.WriteLine(@"       //  | |                        / ;   \;;,\                          ");
            Console.WriteLine(@"      (<_  | ;                      /',/-----'  _>                         ");
            Console.WriteLine(@"       \_| ||_                     //~;~~~~~~~~~                           ");
            Console.WriteLine(@"           `\_|                   (,~~                                     ");
            Console.WriteLine(@"                                   \~\                                     ");
            Console.WriteLine(@"                                    ~~                                     ");
            Console.WriteLine(@"                                                                           ");

            CapstoneCLI c = new CapstoneCLI();
            c.Run();
        }
    }
}
//int th = CLIHelper.GetDays("enter");
//Console.WriteLine(th);