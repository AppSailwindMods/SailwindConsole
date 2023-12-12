using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class ShowPortsCommand : Command
    {
        public override string Name => "showPorts";

        public override string Description => "Show all the ports' names";

        public override void OnRun(List<string> args)
        {
            foreach (Port port in Port.ports)
            {
                if (port)
                {
                    ModConsoleLog.Log($"Port: {port.GetPortName()}");
                }
            }
        }
    }
}
