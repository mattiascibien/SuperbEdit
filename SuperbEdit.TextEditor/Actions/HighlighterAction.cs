using ICSharpCode.AvalonEdit.Highlighting;
using SuperbEdit.Base;
using SuperbEdit.TextEditor.ViewModels;
using SuperbEdit.TextEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.TextEditor.Actions
{
    //This varies from the highlighter action
    public class HighlighterAction : ActionItem
    {
        Lazy<IShell> _shell;
        IHighlightingDefinition _highLighter;


        public HighlighterAction(IHighlightingDefinition highlighter, Lazy<IShell> shell)
            : base(highlighter.Name, "Set highlight to: " + highlighter.Name)
        {
            _highLighter = highlighter;
            //we passs a Lazy IShell so it will not be null at this time
            _shell = shell;
        }



        public override void Execute()
        {
            //TODO: better handle this
            var vm = (_shell.Value.ActiveItem as TextEditorViewModel);

            var view = vm.GetView() as TextEditorView;


            view.ModernTextEditor.SyntaxHighlighting = _highLighter;
        }
    }
}
