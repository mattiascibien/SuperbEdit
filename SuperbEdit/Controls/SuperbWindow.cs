using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SuperbEdit.Controls
{
    public class SuperbWindow : Window
    {
        public static readonly DependencyProperty GlobalInputBindingsProperty =
            DependencyProperty.Register(
            "GlobalInputBindings", typeof(InputBindingCollection), typeof(Window), new FrameworkPropertyMetadata(null,
      FrameworkPropertyMetadataOptions.AffectsRender,
      new PropertyChangedCallback(GlobalInputBindingsChanged)));

        private static void GlobalInputBindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window win = d as Window;
            win.InputBindings.Clear();
            win.InputBindings.AddRange(e.NewValue as InputBindingCollection);
        }

        public InputBindingCollection GlobalInputBindings
        {
            get { return (InputBindingCollection)GetValue(GlobalInputBindingsProperty); }
            set
            {
                SetValue(GlobalInputBindingsProperty, value);

            }
        }
    }
}
