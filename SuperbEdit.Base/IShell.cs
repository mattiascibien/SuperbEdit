using Caliburn.Micro;

namespace SuperbEdit.Base
{
    public interface IShell
    {
        ITab ActiveItem { get; }
        IObservableCollection<ITab> Items { get; }
        void DetachItem(ITab item);

        void Exit();


        void OpenTab(ITab tab);

        void ShowHidePanel(IPanel panel);
    }
}