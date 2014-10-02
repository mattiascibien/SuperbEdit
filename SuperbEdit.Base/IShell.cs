namespace SuperbEdit.Base
{
    public interface IShell
    {
        void DetachItem(ITab item);

        void NewFile();
        void OpenFile();
        void Save();
        void SaveAs();
        void SaveAll();
        void CloseActiveItem();
        void Exit();

        void Undo();
        void Redo();
        void Cut();
        void Copy();
        void Paste();
    }
}