using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenuSystem.MenuItems
{
    public class MenuItem : IMenuItem
    {
        public string Title { get; private set; }
        public string SubTitle { get; private set; }
        public Func<string> Func { get; set; }

        public MenuItem(string title, string subtitle)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("Title cannot be null or empty", "title");
            Title = title;
            SubTitle = subtitle ?? string.Empty;
        }
    }
}
