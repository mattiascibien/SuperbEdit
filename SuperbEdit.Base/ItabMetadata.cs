using System;
using System.ComponentModel.Composition;

namespace SuperbEdit.Base
{
    public interface ITabMetadata
    {
        bool IsFallback { get; }
        string Name { get; }
    }

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