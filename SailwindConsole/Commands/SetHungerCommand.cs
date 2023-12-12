using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class SetHungerCommand : Command
    {
        public override string Name => "setHunger";
        public override int MinArgs => 1;
        public override string Usage => "<amount>";

        public override string Description => "Set your hunger";

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out float amount);
            if (amount > 0)
            {
                PlayerNeeds.food = amount;
                UISoundPlayer.instance.PlayOpenSound();
                ModConsoleLog.Log($"Set player hunger to {amount}");
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
