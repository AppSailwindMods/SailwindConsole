using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class SetGoldCommand : Command
    {
        public override string Name => "setGold";
        public override int MinArgs => 2;
        public override string Usage => "<currency type (0-3)> <amount>";

        public override string Description => "Set amount of gold you have";

        public override void OnRun(List<string> args)
        {
            int.TryParse(args[0], out int type);
            int.TryParse(args[1], out int amount);
            if(type < 0 || type > 3)
            {
                ModConsoleLog.Error("Currency type out of range");
                return;
            }
            if (amount >= 0)
            {
                PlayerGold.currency[type] = amount;
                UISoundPlayer.instance.PlayGoldSound();
                ModConsoleLog.Log($"Set {PlayerGold.GetCurrencyName(type)} to {amount}");
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
