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

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"                                                                                ____ ___      .__                                      ");
            Console.WriteLine(@"                                                                               |    |   \____ |__| ____  ___________  ____             ");
            Console.WriteLine(@"                                                                               |    |   /    \|  |/ ___\/  _ \_  __ \/    \            ");
            Console.WriteLine(@"                                                                               |    |  /   |  \  \  \__(  <_> )  | \/   |  \           ");
            Console.WriteLine(@"              ,,))))))));,                                                     |______/|___|  /__|\___  >____/|__|  |___|  /           ");
            Console.WriteLine(@"           __)))))))))))))),                                                                \/        \/                 \/            ");
            Console.WriteLine(@"\|/       -\(((((''''((((((((.                                                 _______          __  .__                     .__        ");
            Console.WriteLine(@"-*-==//////((''  .     `)))))),                                                \      \ _____ _/  |_|__| ____   ____ _____  |  |       ");
            Console.WriteLine(@"/|\      ))| o    ;-.    '(((((                                  ,(,           /   |   \\__  \\   __\  |/  _ \ /    \\__  \ |  |       ");
            Console.WriteLine(@"         ( `|    /  )    ;))))'                               ,_))^;(~        /    |    \/ __ \|  | |  (  <_> )   |  \/ __ \|  |__     ");
            Console.WriteLine(@"            |   |   |   ,))((((_     _____------~~~-.        %,;(;(>';'~      \____|__  (____  /__| |__|\____/|___|  (____  /____/     ");
            Console.WriteLine(@"            o_);   ;    )))(((` ~---~  `::           \      %%~~)(v;(`('~             \/     \/                    \/     \/           ");
            Console.WriteLine(@"                  ;    ''''````         `:       `:::|\,__,%%    );`'; ~                                                               ");
            Console.WriteLine(@"                 |   _                )     /      `:|`----'     `-'          __________               __                              ");
            Console.WriteLine(@"           ______/\/~    |                 /        /                         \______   \_____ _______|  | __                          ");
            Console.WriteLine(@"         /~;;.____/;;'  /          ___--,-(   `;;;/                            |     ___/\__  \\_  __ \  |/ /                          ");
            Console.WriteLine(@"        / //  _;______;'------~~~~~    /;;/\    /                              |    |     / __ \|  | \/    <                           ");
            Console.WriteLine(@"       //  | |                        / ;   \;;,\                              |____|    (____  /__|  |__|_ \                          ");
            Console.WriteLine(@"      (<_  | ;                      /',/-----'  _>                                            \/           \/                          ");
            Console.WriteLine(@"       \_| ||_                     //~;~~~~~~~~~                              __________              .__          __                  ");
            Console.WriteLine(@"           `\_|                   (,~~                                        \______   \ ____   ____ |__| _______/  |________ ___.__. ");
            Console.WriteLine(@"                                   \~\                                         |       _// __ \ / ___\|  |/  ___/\   __\_  __ <   |  | ");
            Console.WriteLine(@"                                    ~~                                         |    |   \  ___// /_/  >  |\___ \  |  |  |  | \/\___  | ");
            Console.WriteLine(@"                                                                               |____|_  /\___  >___  /|__/____  > |__|  |__|   / ____| ");
            Console.WriteLine(@"                                                                                      \/     \/_____/         \/               \/      ");
            Console.ResetColor();

            CapstoneCLI c = new CapstoneCLI();
            c.Run(); 
        }
    }
}
//int th = CLIHelper.GetDays("enter");
//Console.WriteLine(th);