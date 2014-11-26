using Caliburn.Micro;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Interface for providing an editor for a file
    /// </summary>
    public interface ITab : IScreen
    {
        /// <summary>
        /// The string representation of the file content
        /// </summary>
        /// 
        string FileContent { get; set; }

        /// <summary>
        /// True if the current file has differences from the one stored in the disc.
        /// Determines the visibility of the '*' in the tab heade
        /// </summary>
        bool HasChanges { get; set; }

        /// <summary>
        /// Saves the file
        /// </summary>
        /// <returns></returns>
        bool Save();


        /// <summary>
        /// Saves the file with a new name
        /// </summary>
        /// <returns></returns>
        bool SaveAs();

        /// <summary>
        /// Undoes last change
        /// </summary>
        void Undo();

        /// <summary>
        /// Redoes the last change
        /// </summary>
        void Redo();

        /// <summary>
        /// Cut
        /// </summary>
        void Cut();


        /// <summary>
        /// Copy
        /// </summary>
        void Copy();


        /// <summary>
        /// Paste
        /// </summary>
        void Paste();


        /// <summary>
        /// Sets the current file path
        /// </summary>
        /// <param name="filePath">the file path</param>
        void SetFile(string filePath);


        void RegisterCommands();


        void CloseItem(Tab item);

        void DetachItem(Tab item);
    }
}