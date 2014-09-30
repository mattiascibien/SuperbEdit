using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    [Export(typeof(ICommandManager))]
    public class CommandManager : ICommandManager
    {
        private List<SuperbCommand> _commands;
        public List<SuperbCommand> Commands
        {
            get { return _commands; }
        }

        public CommandManager()
        {
            _commands = new List<SuperbCommand>();
        }

        public void RegisterCommand(string name, string description, Action action)
        {
            SuperbCommand command = new SuperbCommand()
            {
                CommandName = name,
                CommandDescription = description,
                Action = action
            };
        }

        public void RegisterCommand(SuperbCommand command)
        {
            _commands.Add(command);
        }
    }
}
