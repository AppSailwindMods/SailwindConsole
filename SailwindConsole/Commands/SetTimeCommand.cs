using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class SetTimeCommand : Command
    {
        public override string Name => "setTime";
        public override int MinArgs => 1;
        public override string Usage => "<time>";

        public override string Description => "Set the time of day, you can go above 24 hours to skip days";

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out float time);
            if (time > 0)
            {
                Sun.sun.globalTime = time;
                UISoundPlayer.instance.PlayOpenSound();
                ModConsoleLog.Log($"Time set to: {time}");
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
