using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class HelpCommand : Command
    {
        public override string Name => "help";

        public override string Description => "List all commands available";

        private List<Command> commands;

        public HelpCommand(List<Command> commands)
        {
            this.commands = commands;
        }

        public override void OnRun(List<string> args)
        {
            ModConsoleLog.Write("Reminder: arguments in <> means its required, whilst [] means its optional!");
            foreach (var command in commands)
            {
                ModConsoleLog.Write($"<color=blue>{command.Name}</color>");
                if (!string.IsNullOrEmpty(command.Usage))
                {
                    ModConsoleLog.Write($"Usage: {command.Usage}");
                }
                ModConsoleLog.Write($"Description: {command.Description}");
                ModConsoleLog.WriteNewLine();
            }
            ModConsole.MoveScrollToEnd();
        }
    }
}
