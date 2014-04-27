using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleMenuSystem.MenuItems;
using ConsoleMenuSystem.StringHelpers;

namespace ConsoleMenuSystem
{
    internal class ConsoleMenu
    {
        private readonly List<IMenuItem> _menuItems;
        private readonly string _indents;

        /// <summary>
        /// </summary>
        /// <param name="menuItems">The list of menuitems to show and execute upon choosing</param>
        /// <param name="indentMenu">set to a value larger than zero to indent the menuitems that amount of spaces</param>
        public ConsoleMenu(ICollection<IMenuItem> menuItems, int indentMenu = 0)
        {
            if (menuItems == null || menuItems.Count == 0)
                throw new ArgumentException("There must be menuitems to show");
            var builder = new StringBuilder();
            for (var i = 0; i < indentMenu; i++)
            {
                builder.Append(" ");
            }
            _indents = builder.ToString();
            _menuItems = menuItems.ToList();

            Console.CancelKeyPress += (sender, args) => Environment.Exit(0);
        }

        private void DisplayMenuItemWithIndex(IMenuItem menuItem, int index)
        {
            if (menuItem == null) throw new ArgumentException("Menuitem cannot be null", "menuItem");

            Console.WriteLine("{0}{1}:\t{2}", _indents, index, menuItem.Title);
        }

        /// <summary>
        ///     Will display the menuitems supplied to the constructor
        /// </summary>
        public void DisplayMenu()
        {
            MainLoopRun();
        }

        private void DisplayMenuItems()
        {
            for (var i = 0; i < _menuItems.Count; i++)
            {
                DisplayMenuItemWithIndex(_menuItems[i], i);
            }
        }

        private void MainLoopRun()
        {
            var builder = new StringBuilderExtended();
            Console.Clear();
            var errorState = true; //start out as true to ensure that we do not make starlines.
            var start = true;
            while (true)
            {
                var proposedHeight = GetConsoleWindowHeight(builder.LineCount);
                if (proposedHeight > Console.LargestWindowHeight)
                    proposedHeight = Console.LargestWindowHeight;
                Console.WindowHeight = proposedHeight;

                //Determine printcolor depending on errorstate
                if (!errorState)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    //Display any errormessage from ealier run in red
                    Console.ForegroundColor = ConsoleColor.Red;
                    errorState = false;
                }

                //Print messages
                if (!start) Console.WriteLine(CreateStarLine());
                if (!start) Console.WriteLine(string.Empty);
                Console.WriteLine("{0}{1}", _indents, builder.ToString());
                if (!start) Console.WriteLine(string.Empty);
                if (!start) Console.WriteLine(CreateStarLine());
                start = false;

                //Reset color
                Console.ForegroundColor = ConsoleColor.Gray;

                builder.Clear();

                Console.WriteLine("{0}MENU...", _indents);
                Console.WriteLine(String.Empty);
                //Display menu
                DisplayMenuItems();

                //Show user interactive selection  
                Console.WriteLine(string.Empty);
                Console.WriteLine("{0}Please make a selection...", _indents);
                Console.WriteLine(string.Empty);
                Console.CursorLeft = _indents.Count();
                var selection = Console.ReadLine();

                if (selection == null)
                {
                    builder.AppendLine("CTRL + C pressed. Goodbye");
                    Console.Clear();
                    continue; //If special chars are used then we do not weant to continue
                }

                Console.WriteLine(string.Empty);
                var selector = -1;
                var success = int.TryParse(selection, out selector);

                //Determine what to do depending on the parsing
                if (!success) //Invalid input
                {
                    builder.AppendLine("Only integer input allowed to select a menuitem... Please try again");
                    errorState = true;
                    Console.Clear();
                    continue;
                }

                if (selector < 0 || selector >= _menuItems.Count) //Out of range
                {
                    builder.AppendLine(string.Format("Selection must be between 0 and {0}... Please try again",
                        _menuItems.Count - 1));
                    errorState = true;
                    Console.Clear();
                    continue;
                }

                //Execute menuitem action since all checks went well
                var result = _menuItems[selector].Func.Invoke();

                if (!string.IsNullOrEmpty(result))
                {
                    builder.Append(result);
                }
                errorState = false;
                Console.Clear();
            }
        }

        private int GetConsoleWindowHeight(int append = 0)
        {
            var menuHeight = _menuItems.Count + 12;
            return menuHeight + append;
        }

        private static string CreateStarLine()
        {
            var width = Console.WindowWidth;
            var builder = new StringBuilder(width);
            for (var i = 0; i < width; i++)
            {
                builder.Append("*");
            }
            return builder.ToString();
        }
    }
}