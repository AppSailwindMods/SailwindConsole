using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class SetWaveHeightCommand : Command
    {
        public override string Name => "setWaveHeight";
        public override int MinArgs => 1;

        public override string Usage => "<wave height>";

        public override string Description => "Set wave height";

        public override void OnRun(List<string> args)
        {
            int.TryParse(args[0], out var height);
            if (height >= 0)
            {
                WavesInertia wavesInertia = SaveLoadManager.instance.GetPrivateField<WavesInertia>("wavesInertia");
                wavesInertia.currentMagnitude = height;
                ModConsoleLog.Log($"Set wave height to {height}");
            }
            else
            {
                ModConsoleLog.Error("Value cannot be below 0!");
            }
        }
    }
}
