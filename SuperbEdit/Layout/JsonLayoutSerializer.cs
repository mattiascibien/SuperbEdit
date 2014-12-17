using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace SuperbEdit.Layout
{
    /// <summary>
    /// Serializer for layout state
    /// </summary>
    internal class JsonLayoutSerializer : LayoutSerializer
    {
        public JsonLayoutSerializer(DockingManager manager)
            : base(manager)
        { 
        
        }

        public void Serialize(JsonWriter writer)
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, Manager.Layout);
        }
        public void Serialize(System.IO.TextWriter writer)
        {
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, Manager.Layout);
        }

        public void Serialize(string filepath)
        {
            using (var stream = new StreamWriter(filepath))
                Serialize(stream);
        }

        public void Deserialize(System.IO.TextReader reader)
        {
            try
            {
                StartDeserialization();
                var serializer = new JsonSerializer();
                var layout = serializer.Deserialize(reader, typeof(LayoutRoot)) as LayoutRoot;
                FixupLayout(layout);
                Manager.Layout = layout;
            }
            finally
            {
                EndDeserialization();
            }
        }

        public void Deserialize(JsonReader reader)
        {
            try
            {
                StartDeserialization();
                var serializer = new JsonSerializer();
                var layout = serializer.Deserialize(reader, typeof(LayoutRoot)) as LayoutRoot;
                FixupLayout(layout);
                Manager.Layout = layout;
            }
            finally
            {
                EndDeserialization();
            }
        }

        public void Deserialize(string filepath)
        {
            using (var stream = new StreamReader(filepath))
                Deserialize(stream);
        }

    }
}
