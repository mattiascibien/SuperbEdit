using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SuperbEdit.Base
{
    public class GroupItem : ActionItem
    {
        public GroupItem(IEnumerable<Lazy<IActionItem, IActionItemMetadata>> possibilechildren, string name)
            : base(name, null)
        {
            Items = new List<IActionItem>();
            if (possibilechildren != null)
            {
                Items = possibilechildren.Where(x => x.Metadata.Menu == name).OrderBy(x => x.Metadata.Order).Select(x => x.Value).ToList();
            }

        }

        public override void Execute()
        {
            
        }
    }


    /// <summary>
    /// Abstract class providing base functionality for tabs
    /// </summary>
    public abstract class ActionItem : IActionItem
    {

        protected ActionItem(string name, string description, string keyBind)
            : this(name, description)
        {
            Shortcut = KeybindingConfig.RetrieveKeyBind(keyBind);
        }

        protected ActionItem(string name, string description)
        {
            Name = name;
            Description = description;
            IsSeparator = false;
            Items = null;
            Shortcut = "";
        }

        public string Shortcut { get; private set; }

        /// <summary>
        /// Name (text) of the action
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Detaled desctiprion of the action
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Methods providing the logic for the action execution
        /// </summary>
        public abstract void Execute();


        public bool IsSeparator { get; protected set; }


        public IEnumerable<IActionItem> Items
        {
            get;
            set;
        }



        //ICommand use
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.Execute();
        }
    }

    public interface IActionItemMetadata
    {
        string Menu { get; }
        string Owner { get; }
        int Order { get; }

        /// <summary>
        /// Determines if the action should be available in the command window
        /// </summary>
        bool RegisterInCommandWindow { get; }
    }


    /// <summary>
    /// Null action providing sepratators
    /// </summary>
    [Export(typeof (SeparatorItem))]
    public class SeparatorItem : ActionItem
    {
        public SeparatorItem() : base("", "")
        {
            IsSeparator = true;
        }

        public override void Execute()
        {
        }
    }


    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportAction : ExportAttribute, IActionItemMetadata
    {
        public ExportAction()
            : base(typeof (IActionItem))
        {
        }

        public string Menu { get; set; }

        public string Owner { get; set; }

        public int Order { get; set; }

        public bool RegisterInCommandWindow { get; set; }
    }
}