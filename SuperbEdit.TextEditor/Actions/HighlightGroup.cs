using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.TextEditor.Actions
{
    [ExportAction(Menu = "View", Order = 2, Owner = "TextEditor", RegisterInCommandWindow = false)]
    public class HighlightGroup : GroupItem
    {

        [ImportingConstructor]
        public HighlightGroup([Import] HighlightersLoader _highlightsLoader, [Import] Lazy<IShell> shell)
            : base(null, "Syntax Highlighting")
        {
            foreach (var highlighter in _highlightsLoader.Highlighters)
            {
                HighlighterAction action = new HighlighterAction(highlighter, shell);

                (Items as List<IActionItem>).Add(action);
            }
        }
    }
}
