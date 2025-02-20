﻿using NebulaAPI;
using NebulaModel.Networking;
using NebulaModel.Packets;
using NebulaModel.Packets.Factory;
using NebulaWorld;

namespace NebulaNetwork.PacketProcessors.Factory.Storage
{
    [RegisterPacketProcessor]
    internal class StorageSyncSetBansProcessor : PacketProcessor<StorageSyncSetBansPacket>
    {
        public override void ProcessPacket(StorageSyncSetBansPacket packet, NebulaConnection conn)
        {
            StorageComponent storage = null;
            StorageComponent[] pool = GameMain.galaxy.PlanetById(packet.PlanetId)?.factory?.factoryStorage?.storagePool;
            if (pool != null && packet.StorageIndex != -1 && packet.StorageIndex < pool.Length)
            {
                storage = pool[packet.StorageIndex];
            }

            if (storage != null)
            {
                using (Multiplayer.Session.Storage.IsIncomingRequest.On())
                {
                    storage.SetBans(packet.Bans);
                }
            }
        }
    }
}