using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class AddReputation : Command
    {
        public override string Name => "addReputation";
        public override int MinArgs => 2;
        public override string Usage => "<region (0-2)> <amount>";

        public override string Description => "Add reputation in a certain region";

        public override void OnRun(List<string> args)
        {
            if (Enum.TryParse(args[0], out PortRegion region))
            {
                int.TryParse(args[1], out int reputation);
                if (reputation >= 0)
                {
                    PlayerReputation.ChangeReputation(reputation, region);
                    ModConsoleLog.Log($"Added {reputation} reputation to {region}");
                }
                else
                {
                    ModConsoleLog.Error("Cannot have a value below 0!");
                }
            }
            else
            {
                ModConsoleLog.Error("Invalid region value!");
            }
        }
    }
}
