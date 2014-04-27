using System;

namespace ConsoleMenuSystem.MenuItems
{
    public interface IMenuItem
    {
        string Title { get; }
        string SubTitle { get; }
        Func<string> Func { get; set; }
    }
}