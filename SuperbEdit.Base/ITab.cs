using Caliburn.Micro;

namespace SuperbEdit.Base
{
    public interface ITab : IScreen
    {
        bool HasChanges { get; set; }

        bool Save();

        bool SaveAs();

        void Undo();

        void Redo();

        void Cut();

        void Copy();

        void Paste();


        void SetFile(string filePath);
    }
}