using System;
using System.ComponentModel.Composition;

namespace SuperbEdit.Base
{
    /// <summary>
    /// Metadata for exporting Tabs
    /// </summary>
    public interface ITabMetadata
    {
        string Name { get; }
    }


    /// <summary>
    /// Attribute to provide metadata for tab exporting
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportTab : ExportAttribute, ITabMetadata
    {
        public ExportTab() : base(typeof (ITab))
        {
        }

        public string Name { get; set; }
    }
}