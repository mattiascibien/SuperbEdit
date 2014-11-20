using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SuperbEdit.Converters
{
    public class StringToKeyBinding : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is string)
            {
                string bindingString = value as string;

                if (!string.IsNullOrEmpty(bindingString))
                {
                    KeyBinding keybind = new KeyBinding();

                    string[] values = bindingString.Split('+');

                    for (int i = 0; i < values.Length; i++)
                    {
                        if(i == values.Length - 1)
                        {
                            KeyConverter k = new KeyConverter();
                            keybind.Key = (Key)k.ConvertFromString(values[i]);
                        }
                        else
                        {
                            ModifierKeysConverter conv = new ModifierKeysConverter();
                            keybind.Modifiers = keybind.Modifiers | (ModifierKeys)conv.ConvertFromString(values[i]);
                        }
                    }

                    return keybind;
                }

                return null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
