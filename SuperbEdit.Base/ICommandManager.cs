using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperbEdit.Base
{
    public interface ICommandManager
    {
        List<SuperbCommand> Commands { get; }

        void RegisterCommand(string name, string description, Action action);
        void RegisterCommand(SuperbCommand command);
    }
}
