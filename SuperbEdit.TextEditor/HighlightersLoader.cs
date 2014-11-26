using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SuperbEdit.TextEditor
{
    [Export]
    public class HighlightersLoader
    {
        public List<IHighlightingDefinition> Highlighters
        {
            get;
            set;
        }

        [ImportingConstructor]
        public HighlightersLoader()
        {
            string[] defaultHighlighters = Directory.GetFiles(Folders.DefaultPackagesFolder, "*.xshd", SearchOption.AllDirectories);
            string[] userHighlighters = Directory.GetFiles(Folders.UserFolder, "*.xshd", SearchOption.AllDirectories);

            IEnumerable<string> highlighters = defaultHighlighters.Concat(userHighlighters);
            highlighters = highlighters.OrderBy((h) => h);

            Highlighters = new List<IHighlightingDefinition>();

            foreach (var item in highlighters)
            {
                using (XmlTextReader reader = new XmlTextReader(item))
                {
                    var highlighter = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    Highlighters.Add(highlighter);
                }
            }

        }
    }
}
