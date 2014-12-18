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
using System.Xml.Linq;

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

        private Dictionary<string, IHighlightingDefinition> ByExtension;

        [ImportingConstructor]
        public HighlightersLoader()
        {
            ByExtension = new Dictionary<string, IHighlightingDefinition>();
            string[] defaultHighlighters = Directory.GetFiles(Folders.DefaultPackagesFolder, "*.xshd", SearchOption.AllDirectories);
            string[] userHighlighters = Directory.GetFiles(Folders.UserFolder, "*.xshd", SearchOption.AllDirectories);

            IEnumerable<string> highlighters = defaultHighlighters.Concat(userHighlighters);
            highlighters = highlighters.OrderBy((h) => h);

            Highlighters = new List<IHighlightingDefinition>();

            foreach (var item in highlighters)
            {
                IHighlightingDefinition highlighter = null;
                XDocument doc = XDocument.Load(item);
                string[] exts = doc.Root.Attribute("extensions").Value.Split(';');

                using (XmlReader reader = doc.CreateReader())
                {
                    highlighter = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    Highlighters.Add(highlighter);

                }

                foreach (var ext in exts)
                {
                    ByExtension.Add(ext, highlighter);
                }
            }

        }

        /// <summary>
        /// Gets the highlighter associated with a particoular extension
        /// </summary>
        /// <param name="extension">the file extension (Ex: ".boo")</param>
        /// <returns></returns>
        public IHighlightingDefinition GetForExtension(string extension)
        {
            if (ByExtension.ContainsKey(extension))
                return ByExtension[extension];
            else
                return null;
        }
    }
}
