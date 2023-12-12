using SailwindConsole.Patches;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SailwindModdingHelper;

namespace SailwindConsole.Commands
{
    internal class SetWindSpeedCommand : Command
    {
        public override string Name => "setWindSpeed";
        public override int MinArgs => 1;
        public override string Usage => "<wind speed (Storm, Gale, Breeze, Calm)>";

        public override string Description => "Set wind speed based on predefined values (Storm, Gale, Breeze, Calm)";

        static Dictionary<string, int> windSpeeds = new Dictionary<string, int>()
        {
            { "storm", 40 },
            { "gale", 20 },
            { "breeze", 10 },
            { "calm", 5 },
        };

        public override void OnRun(List<string> args)
        {
            if(!windSpeeds.TryGetValue(args[0].ToLower(), out int windSpeed))
            {
                ModConsoleLog.Error("Not a valid wind predefined value!");
            }
            else
            {
                GameData.wind.SetPrivateField("currentWindTarget", Wind.currentBaseWind.normalized * windSpeed);
                GameData.wind.SetPrivateField("timer", 90);
                ModConsoleLog.Log("Wind speed set!");
            }
        }
    }
}
