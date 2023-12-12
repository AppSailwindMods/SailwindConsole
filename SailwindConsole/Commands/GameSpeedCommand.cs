using SailwindConsole.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class GameSpeedCommand : Command
    {
        public override string Name => "setGamespeed";
        public override int MinArgs => 1;
        public override string Usage => "<speed>";

        public override string Description => "Set the speed of the game";

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out var speed);
            if (speed > 0)
            {
                Time.timeScale = speed;
                Time.fixedDeltaTime = speed * GameData.initialTimeStep;
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
