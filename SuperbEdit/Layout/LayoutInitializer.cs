using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Layout;

namespace SuperbEdit.Layout
{
    /// <summary>
    /// Class for providing layout initialization 
    /// </summary>
    internal class LayoutInitializer : ILayoutUpdateStrategy
    {
        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {

        }

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {

        }

        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            //TODO: add logic for setting the right panel position
            switch ((anchorableToShow.Content as Panel).DefaultPosition)
            {
                case PanelPosition.Left:
                    break;
                case PanelPosition.Right:
                    break;
                case PanelPosition.Bottom:
                    break;
                default:
                    break;
            }

            return false;
        }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            return false;
        }
    }
}
