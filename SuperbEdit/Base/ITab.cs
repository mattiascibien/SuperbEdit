using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public interface ITab : IScreen
    {
        bool HasChanges { get; set; }

        void Save();

        void SaveAs();

        void Undo();

        void Redo();
    }
}
