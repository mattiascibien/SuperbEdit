using Caliburn.Micro;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Interface for providing access to the interface of the program
    /// </summary>
    public interface IShell
    {
        /// <summary>
        /// Returns the active tab
        /// </summary>
        ITab ActiveItem { get; }

        /// <summary>
        /// List of current opened tabs
        /// </summary>
        IObservableCollection<ITab> Items { get; }
        
        
        void DetachItem(ITab item);


        /// <summary>
        /// Closes the application
        /// </summary>
        void Exit();


        /// <summary>
        /// Opens a new tab
        /// </summary>
        /// <param name="tab">a tab requested via TabService</param>
        void OpenTab(ITab tab);


        /// <summary>
        /// Toggles visibility of a Panel
        /// </summary>
        /// <param name="panel">The Panel to show/hide</param>
        void ShowHidePanel(IPanel panel);
    }
}