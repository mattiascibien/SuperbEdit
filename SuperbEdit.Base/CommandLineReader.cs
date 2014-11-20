using Mono.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperbEdit.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportCommandLineOption : ExportAttribute
    {
        public ExportCommandLineOption()
            : base(typeof(ICommandLineOption))
        {
        }
    }

    public interface ICommandLineOption
    {
        string ShortCommand { get; }
        string Command { get; }
        Action<string> Action { get; }
    }

    [Export]
    public class CommandLineReader
    {
        OptionSet optionSet;

        [ImportingConstructor]
        public CommandLineReader([ImportMany] IEnumerable<ICommandLineOption> cmdLineOptions)
        {
            optionSet = new OptionSet();
            foreach (var option in cmdLineOptions)
            {
                optionSet.Add(option.ShortCommand + "|" + option.Command + "", option.Action);
            }
        }


        public void ExecuteCommandLine()
        {
            List<string> extra;
            try
            {
                //Parses and exectures command line
                extra = optionSet.Parse(Environment.GetCommandLineArgs());
            }
            catch (OptionException e)
            {
                Console.Write("SuperbEdit: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `SuperbEdit --help' for more information.");
                return;
            }
        }


        [ExportCommandLineOption]
        public class HelpCommandLineOption : ICommandLineOption
        {
            public void WriteToConsole(string message)
            {
                AttachConsole(-1);
                Console.WriteLine(message);
            }

            [DllImport("Kernel32.dll")]
            public static extern bool AttachConsole(int processId);

            [Import]
            Lazy<CommandLineReader> cmdLineReader;

            [Import]
            Lazy<IShell> shell;

            public string ShortCommand
            {
                get { return "h"; }
            }

            public string Command
            {
                get { return "help"; }
            }

            public Action<string> Action
            {
                get
                {
                    return new Action<string>((str) =>
                    {

                        StringBuilder outputBuilder = new StringBuilder("");
                        outputBuilder.AppendLine("Usage: SuperbEdit [OPTIONS]+ message");
                        outputBuilder.AppendLine("Superbe Text Editor.");
                        outputBuilder.AppendLine();
                        outputBuilder.AppendLine("Options:");

                        StringWriter optionsWriter = new StringWriter(outputBuilder);

                        cmdLineReader.Value.optionSet.WriteOptionDescriptions(optionsWriter);

                        WriteToConsole(optionsWriter.ToString());
                    });
                }
            }
        }
    }
}
