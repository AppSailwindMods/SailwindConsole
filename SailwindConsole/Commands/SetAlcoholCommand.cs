﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Commands
{
    internal class SetAlcoholCommand : Command
    {
        public override string Name => "setAlcohol";
        public override int MinArgs => 1;
        public override string Usage => "<amount>";

        public override string Description => "Set your alcohol level";

        public override void OnRun(List<string> args)
        {
            float.TryParse(args[0], out float amount);
            if (amount >= 0)
            {
                PlayerNeeds.alcohol = amount;
                Refs.playerMouthCol.PlayDrinkSound();
                ModConsoleLog.Log($"Set player alcohol to {amount}");
            }
            else
            {
                ModConsoleLog.Error("Cannot have a value below 0!");
            }
        }
    }
}
