using System;
using EvPlayer = Exiled.Events.Handlers.Player;
using Exiled.API.Features;

namespace SCP1162_EXI_2._0
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "scp1162";
        public override string Name => "SCP1162";
        public override string Author => "xRoier";
        public EventHandlers EventHandlers;
        public override Version Version { get; } = new Version(2, 1, 4);
        public override Version RequiredExiledVersion { get; } = new Version(2, 13, 0);
        
        public override void OnEnabled()
        {
            EventHandlers = new EventHandlers(this);
            EvPlayer.ItemDropped += EventHandlers.OnItemDropped;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            EvPlayer.ItemDropped -= EventHandlers.OnItemDropped;
            EventHandlers = null;
            base.OnDisabled();
        }
    }
}
