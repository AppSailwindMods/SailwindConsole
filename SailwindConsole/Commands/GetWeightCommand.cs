using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class GetWeightCommand : Command
    {
        public override string Name => "getWeight";

        public override string Description => "Get weight of held item";

        public override void OnRun(List<string> args)
        {
            GoPointer goPointer = GameObject.FindObjectOfType<GoPointer>();
            PickupableItem heldItem = goPointer.GetHeldItem();
            if (heldItem)
            {
                ShipItem shipItem = heldItem.GetComponent<ShipItem>();
                if (shipItem)
                {
                    ModConsoleLog.Log($"Held Item Weight: {shipItem.mass}!");
                }
                else
                {
                    ModConsoleLog.Log("Item held has no mass!");
                }
            }
            else
            {
                ModConsoleLog.Log("Not holding anything!");
            }
        }
    }
}
