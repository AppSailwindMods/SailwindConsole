using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    public class GenericCommand : Command
    {
        private string name;
        private int minArgs;
        private Action<List<string>> onRun;
        private string usage;
        private string description;

        public override string Name => name;
        public override int MinArgs => minArgs;
        public override string Usage => usage;
        public override string Description => description;

        public GenericCommand(string name, string description, int minArgs, string usage, Action<List<string>> onRun)
        {
            this.name = name;
            this.onRun = onRun;
            this.minArgs = minArgs;
            this.usage = usage;
            this.description = description;
        }

        public GenericCommand(string name, string description, Action<List<string>> onRun) : this(name, description, 0, "", onRun)
        {
        }

        public override void OnRun(List<string> args)
        {
            onRun?.Invoke(args);
        }
    }
}
