using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    public abstract class ActionItem : IActionItem
    {

        public ActionItem()
        {

        }

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract void Execute();
    }

    public interface IActionItemMetadata
    {
        string Menu { get; }
        string Owner { get; }
        int Order { get; }
        bool RegisterInCommandWindow { get; }
    }


    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportActionMetadata : ExportAttribute, IActionItemMetadata
    {
        public ExportActionMetadata()
            : base(typeof(ActionItem))
        {

        } 

        public string Menu { get; set; }

        public string Owner { get; set; }

        public int Order { get; set; }

        public bool RegisterInCommandWindow { get; set; }
    }
}
