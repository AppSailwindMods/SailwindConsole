using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class SetSleepCommand : Command
    {
        public override string Name => "setSleep";
        public override int MinArgs => 1;
        public override string Usage => "<amount>";

        public override string Description => "Set your sleep";

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out float amount);
            if (amount > 0)
            {
                PlayerNeeds.sleep = amount;
                UISoundPlayer.instance.PlayOpenSound();
                ModConsoleLog.Log($"Set player sleep to {amount}");
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
