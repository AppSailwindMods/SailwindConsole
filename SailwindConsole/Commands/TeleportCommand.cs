using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class TeleportCommand : Command
    {
        public override string Name => "teleport";
        public override int MinArgs => 1;
        public override string Usage => "<port name>";

        public override string Description => "Teleport to a port (do not pause for a while after teleporting)";

        public override void OnRun(List<string> args)
        {
            string portName = string.Join(" ", args);
            bool teleported = false;
            foreach (Port port in Port.ports)
            {
                if (port && port.GetPortName().ToLower() == portName.ToLower())
                {
                    port.teleportPlayer = true;
                    teleported = true;
                    break;
                }
            }
            if (!teleported)
            {
                ModConsoleLog.Error("Invalid port name");
            }
            else
            {
                ModConsoleLog.Log("Teleported! Please be sure to not pause the game for a few moments!");
            }
        }
    }
}
