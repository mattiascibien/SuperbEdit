using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            get
            {
                return false;

            }
        }
    }
}
