using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class GodModeCommand : Command
    {
        public override string Name => "godMode";

        public override string Description => "Activate god mode (this prevents your needs from going down)";

        public override void OnRun(List<string> args)
        {
            PlayerNeeds.instance.godMode = !PlayerNeeds.instance.godMode;
            ModConsoleLog.Log($"{(PlayerNeeds.instance.godMode ? "Enabled" : "Disabled")} god mode!");
        }
    }
}
