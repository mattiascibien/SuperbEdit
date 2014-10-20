using System;
using System.ComponentModel.Composition;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Metadata for exporting Tabs
    /// </summary>
    public interface ITabMetadata
    {
        bool IsFallback { get; }
        string Name { get; }
    }


    /// <summary>
    /// Attribute to provide metadata for tab exporting
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportTabMetadata : ExportAttribute, ITabMetadata
    {
        public ExportTabMetadata() : base(typeof (ITab))
        {
        }

        public string Name { get; set; }

        public bool IsFallback
        {
            get { return false; }
        }
    }
}