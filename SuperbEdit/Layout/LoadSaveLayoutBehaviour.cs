using SuperbEdit.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace SuperbEdit.Layout
{
    public class LoadSaveLayoutBehaviour : Behavior<DockingManager>
    {
        readonly string filePath = Path.Combine(Folders.UserFolder, "layout.xml");

        XmlLayoutSerializer serializer;

        protected override void OnAttached()
        {
            Window theWindow = Window.GetWindow(AssociatedObject);

           
            theWindow.Loaded += (sender, e) =>
            {
                serializer = new XmlLayoutSerializer(AssociatedObject);
                serializer.LayoutSerializationCallback += (s, args) =>
                {
                    args.Content = args.Content;
                };

                if (File.Exists(filePath))
                    serializer.Deserialize(filePath);


            };

            theWindow.Unloaded += (sender, e) =>
            {
                serializer = new XmlLayoutSerializer(AssociatedObject);
                serializer.Serialize(filePath);
            };

            base.OnAttached();
        }
    }
}
