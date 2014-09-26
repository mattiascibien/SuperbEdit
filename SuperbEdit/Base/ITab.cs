using AurelienRibon.Ui.SyntaxHighlightBox;
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

        bool Save();

        bool SaveAs();

        void Undo();

        void Redo();

        void Cut();

        void Copy();

        void Paste();

        void SetHighlighter(IHighlighter highlighter);
    }
}
