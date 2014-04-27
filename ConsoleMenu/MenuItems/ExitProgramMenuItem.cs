using System;
using System.Threading;

namespace ConsoleMenuSystem.MenuItems
{
    public class ExitProgramMenuItem : MenuItem
    {
        public ExitProgramMenuItem() : base("Exit Program", null)
        {
            Func = () =>
            {
                Console.Clear();
                Console.CursorTop = Console.WindowHeight/2;
                var text = "GOODBYE!";
                Console.Write(new string(' ', (Console.WindowWidth - text.Length)/2));
                Console.WriteLine(text);
                Thread.Sleep(2000);
                Console.Clear();
                Environment.Exit(0);

                return null;// Really just for the compiler
            };
        }
    }
}