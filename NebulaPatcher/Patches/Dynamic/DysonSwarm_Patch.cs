﻿using HarmonyLib;
using NebulaModel.Packets.Universe;
using NebulaWorld;
using UnityEngine;

namespace NebulaPatcher.Patches.Dynamic
{
    [HarmonyPatch(typeof(DysonSwarm))]
    internal class DysonSwarm_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSwarm.NewOrbit))]
        public static bool NewOrbit_Prefix(DysonSwarm __instance, int __result, float radius, Quaternion rotation)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that orbit for Dyson Swarm was created
            if (!Multiplayer.Session.DysonSpheres.IncomingDysonSwarmPacket)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSwarmAddOrbitPacket(__instance.starData.index, radius, rotation));
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSwarm.RemoveOrbit))]
        public static bool RemoveOrbit_Prefix(DysonSwarm __instance, int orbitId)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that orbit for Dyson Swarm was deleted
            if (!Multiplayer.Session.DysonSpheres.IncomingDysonSwarmPacket)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSwarmRemoveOrbitPacket(__instance.starData.index, orbitId));
            }
            return true;
        }
    }
}
