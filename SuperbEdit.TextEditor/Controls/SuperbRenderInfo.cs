using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SuperbEdit.TextEditor.Controls
{
    public class SuperbRenderInfo
    {
        public FormattedText BoxText { get; set; }
        public FormattedText LineNumbers { get; set; }
        public Point RenderPoint { get; set; }
    }
}
