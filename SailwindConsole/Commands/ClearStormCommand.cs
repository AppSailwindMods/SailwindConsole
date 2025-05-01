using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class ClearStormCommand : Command
    {
        public override string Name => "clearStorm";

        public override string Description => "Clears Storm";

        public override void OnRun(List<string> args)
        {
            WeatherStorms.instance.FindClosestStorm();
            WeatherStorms.instance.GetCurrentStorm().transform.Translate(Wind.currentWind.normalized * WeatherStorms.instance.GetCurrentStorm().GetRadius()*5);
            WeatherStorms.instance.ApplyStorm();
            ModConsoleLog.Log("Cleared Storm!");
        }
    }
}
