using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class CurrentTimeCommand : Command
    {
        public override string Name => "currentTime";

        public override string Description => "Get both current local time and global time";

        public override void OnRun(List<string> args)
        {
            ModConsoleLog.Log($"Local Time: {Sun.sun.localTime:F2}");
            ModConsoleLog.Log($"Global Time: {Sun.sun.globalTime:F2}");
            UISoundPlayer.instance.PlayOpenSound();
        }
    }
}
