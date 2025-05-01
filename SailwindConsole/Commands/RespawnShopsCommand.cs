using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Commands
{
    internal class RespawnShopsCommand : Command
    {
        public override string Name => "respawnShops";

        public override string Description => "Respawn all nearby shops";

        public override void OnRun(List<string> args)
        {
            foreach (var item in GameObject.FindObjectsOfType<ShopItemSpawner>())
            {
                item.SetPrivateField("respawnTimer", 0);
                item.SetPrivateField("timer", 0);
            }
            ModConsoleLog.Log($"Respawn all items in nearby shops");
        }
    }
}
