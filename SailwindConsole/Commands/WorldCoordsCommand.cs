using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class WorldCoordsCommand : Command
    {
        public override string Name => "getWorldCoords";

        public override string Description => "Get your position in Unity coordinates";

        public override void OnRun(List<string> args)
        {
            Vector3 position = Utilities.PlayerTransform.position;
            ModConsoleLog.Log($"Your current world position");
            ModConsoleLog.Log($"X: {position.x}");
            ModConsoleLog.Log($"Y: {position.y}");
            ModConsoleLog.Log($"Z: {position.z}");
        }
    }
}
