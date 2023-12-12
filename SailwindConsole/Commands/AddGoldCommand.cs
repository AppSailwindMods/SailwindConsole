using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class AddGoldCommand : Command
    {
        public override string Name => "addGold";
        public override int MinArgs => 2;
        public override string Usage => "<currency type (0-3)> <amount>";

        public override string Description => "Increase amount of gold";

        public override void OnRun(List<string> args)
        {
            int.TryParse(args[0], out int type);
            int.TryParse(args[1], out int amount);
            if (type < 0 || type > 3)
            {
                ModConsoleLog.Error("Currency type out of range");
                return;
            }
            if (amount >= 0)
            {
                PlayerGold.currency[type] += amount;
                UISoundPlayer.instance.PlayGoldSound();
                ModConsoleLog.Log($"Increased {PlayerGold.GetCurrencyName(type)} by {amount}");
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
