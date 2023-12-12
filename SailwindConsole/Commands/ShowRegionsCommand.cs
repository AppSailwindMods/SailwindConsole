using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class ShowRegionsCommand : Command
    {
        public override string Name => "showRegions";

        public override string Description => "Show all the regions' names";

        public override void OnRun(List<string> args)
        {
            string[] regions = Enum.GetNames(typeof(PortRegion));
            for (int i = 0; i < regions.Length - 1; i++)
            {
                ModConsoleLog.Log($"{i}: {regions[i]}");
            }
        }
    }
}
