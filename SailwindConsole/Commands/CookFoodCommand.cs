using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class CookFoodCommand : Command
    {
        public override string Name => "cookFood";

        public override string Description => "Cook held food";

        public override void OnRun(List<string> args)
        {
            GoPointer goPointer = GameObject.FindObjectOfType<GoPointer>();
            PickupableItem heldItem = goPointer.GetHeldItem();

            if (!heldItem)
            {
                ModConsoleLog.Error("Not holding anything!");
                return;
            }

            ShipItemFood shipItem = heldItem.GetComponent<ShipItemFood>();
            if (!shipItem)
            {
                ModConsoleLog.Error("Item is not a food item!");
                return;
            }

            if (!shipItem.sold)
            {
                ModConsoleLog.Error("Item has not been bought!");
                return;
            }

            CookableFood cookableFood = heldItem.GetComponent<CookableFood>();

            if (!cookableFood)
            {
                ModConsoleLog.Error("Item held is not cookable food!");
                return;
            }

            cookableFood.SetPrivateField("currentHeat", 1.25f);
            shipItem.amount = 1.2f;
            cookableFood.UpdateMaterial();
            ModConsoleLog.Log("Cooked held food!");
        }
    }
}
