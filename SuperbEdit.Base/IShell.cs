using System.Collections;
using System.Collections.Generic;
using Caliburn.Micro;

namespace SuperbEdit.Base
{
    public interface IShell
    {
        void DetachItem(ITab item);

        void Exit();


        void OpenTab(ITab tab);

        ITab ActiveItem { get; }
        IObservableCollection<ITab> Items { get; }

    }
}