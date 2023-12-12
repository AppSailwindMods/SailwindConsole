using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class LatLongCommand : Command
    {
        public override string Name => "getLatLong";

        public override string Description => "Get your longitude and latitude";

        public override void OnRun(List<string> args)
        {
            Vector3 position = FloatingOriginManager.instance.GetGlobeCoords(Utilities.PlayerTransform);
            ModConsoleLog.Log($"Your current world position");
            ModConsoleLog.Log($"Longitude: {position.x}");
            ModConsoleLog.Log($"Latitude: {position.z}");
        }
    }
}
