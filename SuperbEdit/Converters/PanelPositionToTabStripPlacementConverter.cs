using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SuperbEdit.Converters
{
    public class PanelPositionToTabStripPlacementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is PanelPosition)
            {

                switch ((PanelPosition)value)
                {
                    case PanelPosition.Left:
                        return Dock.Left;
                    case PanelPosition.Right:
                        return Dock.Right;
                    case PanelPosition.Bottom:
                        return Dock.Bottom;
                    default:
                        return Dock.Top;
                }
            }

            return Dock.Top;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Dock)
            {

                switch ((Dock)value)
                {
                    case Dock.Left:
                        return PanelPosition.Left;
                    case Dock.Right:
                        return PanelPosition.Right;
                    case Dock.Bottom:
                        return PanelPosition.Bottom;
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
