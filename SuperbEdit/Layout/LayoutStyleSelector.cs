using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SuperbEdit.Layout
{
    public class LayoutStyleSelector : StyleSelector
    {
        public Style AnchorableStyle
        {
            get;
            set;
        }


        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is IPanel)
                return AnchorableStyle;

            return base.SelectStyle(item, container);
        }
    }
}
