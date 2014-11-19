using System.Collections.Generic;
namespace SuperbEdit.Base
{
    /// <summary>
    /// Interface for providing executable action including
    /// menu items and command window actions
    /// </summary>
    public interface IActionItem
    {
        string Name { get; }
        string Description { get; }
        bool IsSeparator { get; }
        string Shortcut { get; }

        IEnumerable<IActionItem> Items { get; }


        void Execute();
    }
}