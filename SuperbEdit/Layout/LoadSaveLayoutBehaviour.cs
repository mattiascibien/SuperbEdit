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
            serializer = new XmlLayoutSerializer(AssociatedObject);
            Window theWindow = Window.GetWindow(AssociatedObject);

            if (File.Exists(filePath))
                serializer.Deserialize(filePath);

            theWindow.Closed += (sender, e) =>
            {
                serializer.Serialize(filePath);
            };

            base.OnAttached();
        }
    }
}
