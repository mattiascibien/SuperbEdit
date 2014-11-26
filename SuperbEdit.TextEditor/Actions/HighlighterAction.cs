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
    /// <summary>
    /// Internal action created by the class <see cref="HighlightGroup"/>.
    /// Provides a way to set the current syntax highlight scheme
    /// </summary>
    internal class HighlighterAction : ActionItem
    {
        Lazy<IShell> _shell;
        IHighlightingDefinition _highLighter;


        public HighlighterAction(IHighlightingDefinition highlighter, Lazy<IShell> shell)
            : base(highlighter.Name, "Set highlighter to: " + highlighter.Name)
        {
            _highLighter = highlighter;
            //we passs a Lazy IShell so it will be initialized when we call Execute()
            _shell = shell;
        }



        public override void Execute()
        {
            var vm = (_shell.Value.ActiveItem as TextEditorViewModel);

            if (vm != null)
            {

                var view = vm.GetView() as TextEditorView;


                view.ModernTextEditor.SyntaxHighlighting = _highLighter;
            }
        }
    }
}
