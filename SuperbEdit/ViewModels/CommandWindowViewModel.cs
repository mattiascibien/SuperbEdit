using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SuperbEdit.Base;

namespace SuperbEdit.ViewModels
{
    public class CommandWindowViewModel : Screen
    {
        [Import] private ICommandManager _commandManager;
    }
}
