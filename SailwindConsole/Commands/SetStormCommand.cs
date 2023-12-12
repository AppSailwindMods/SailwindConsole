using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class SetStormCommand : Command
    {
        public override string Name => "setStorm";

        public override string Description => "Place a storm at player's location";

        public override void OnRun(List<string> args)
        {
            WeatherStorms.instance.GetCurrentStorm().transform.position = Utilities.PlayerTransform.position;
            ModConsoleLog.Log("Placed storm at player's location!");
        }
    }
}
