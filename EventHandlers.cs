using System.Collections.Generic;

using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;

namespace SCP1162_EXI_2._0
{
    public class EventHandlers
    {
        private Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        internal void OnItemDropped(ItemDroppedEventArgs ev)
        {
            if (Vector3.Distance(ev.Player.Position, Map.GetRandomSpawnPoint(RoleType.Scp173)) <= 8.2f)
            {
                if (plugin.Config.UseHints)
                    ev.Player.ShowHint(plugin.Config.ItemDropMessage, plugin.Config.ItemDropMessageDuration);
                else
                    ev.Player.ClearBroadcasts();
                    ev.Player.Broadcast(plugin.Config.ItemDropMessageDuration, plugin.Config.ItemDropMessage);
                if (!plugin.Config.CanSpawnCorpses)
                {
                    ev.Pickup.itemId = plugin.Config.Chances[Random.Range(0, plugin.Config.Chances.Count)];
                    return;
                }
                int r = Random.Range(0, 14);
                if (r <= 13)
                    ev.Pickup.itemId = plugin.Config.Chances[Random.Range(0, plugin.Config.Chances.Count)];
                else
                {
                    ev.Pickup.itemId = ItemType.None;
                    int roleid = Random.Range(0, 12);
                    switch (roleid)
                    {
                        case 2:
                            roleid = 16;
                            break;
                        case 7:
                            roleid = 17;
                            break;
                        case 14:
                            roleid = 11;
                            break;
                    }
                    SpawnCorpse(ev.Player, roleid);
                }
            }
        }
        private void SpawnCorpse(Player player, int role)
            => player.GameObject.GetComponent<RagdollManager>().SpawnRagdoll(player.Position + Vector3.up * 5f, Quaternion.identity, Vector3.zero, role, new PlayerStats.HitInfo(1000f, player.UserId, DamageTypes.Falldown, player.Id), false, "Corpse", "Corpse", 0);
    }
}
