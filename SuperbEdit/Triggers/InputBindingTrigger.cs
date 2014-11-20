using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Linq;

namespace SuperbEdit.Triggers
{
    internal class InputBindingTrigger : TriggerBase<FrameworkElement>, ICommand
    {
        public static readonly DependencyProperty InputBindingProperty =
          DependencyProperty.Register("InputBinding", typeof(InputBinding)
            , typeof(InputBindingTrigger)
            , new UIPropertyMetadata(null));

        public InputBinding InputBinding
        {
            get { return (InputBinding)GetValue(InputBindingProperty); }
            set { SetValue(InputBindingProperty, value); }
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            // action is anyway blocked by Caliburn at the invoke level
            return true;
        }

        public void Execute(object parameter)
        {
            InvokeActions(parameter);
        }

        protected override void OnAttached()
        {
            if (InputBinding != null)
            {
                InputBinding.Command = this;
                AssociatedObject.Loaded += delegate
                {
                    var window = GetWindow(AssociatedObject);
                    window.InputBindings.Add(InputBinding);
                };
            }
            base.OnAttached();
        }

        private Window GetWindow(FrameworkElement frameworkElement)
        {
            //Limit actions to global window
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            //Original code from http://stackoverflow.com/questions/4181310/how-can-i-bind-key-gestures-in-caliburn-micro
            //Does not work for MenuItems
            //if (frameworkElement is Window)
            //    return frameworkElement as Window;

            //var parent = VisualTreeHelper.GetParent(frameworkElement) as FrameworkElement;
            //Debug.Assert(parent != null);

            //return GetWindow(parent);
        }

    }
}