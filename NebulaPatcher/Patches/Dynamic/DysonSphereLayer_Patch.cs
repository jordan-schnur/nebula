﻿using HarmonyLib;
using NebulaAPI;
using NebulaModel.Packets.Universe;
using NebulaWorld;
using System.Collections.Generic;
using UnityEngine;

namespace NebulaPatcher.Patches.Dynamic
{
    [HarmonyPatch(typeof(DysonSphereLayer))]
    internal class DysonSphereLayer_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSphereLayer.NewDysonNode))]
        public static bool NewDysonNode_Prefix(DysonSphereLayer __instance, int __result, int protoId, Vector3 pos)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that user added node to the dyson plan
            if (!Multiplayer.Session.DysonSpheres.IsIncomingRequest)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSphereAddNodePacket(__instance.starData.index, __instance.id, protoId, new Float3(pos)));
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSphereLayer.NewDysonFrame))]
        public static bool NewDysonFrame_Prefix(DysonSphereLayer __instance, int __result, int protoId, int nodeAId, int nodeBId, bool euler)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that user added frame to the dyson plan
            if (!Multiplayer.Session.DysonSpheres.IsIncomingRequest)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSphereAddFramePacket(__instance.starData.index, __instance.id, protoId, nodeAId, nodeBId, euler));
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSphereLayer.RemoveDysonFrame))]
        public static bool RemoveDysonFrame_Prefix(DysonSphereLayer __instance, int frameId)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that user removed frame from the dyson plan
            if (!Multiplayer.Session.DysonSpheres.IsIncomingRequest)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSphereRemoveFramePacket(__instance.starData.index, __instance.id, frameId));
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSphereLayer.RemoveDysonNode))]
        public static bool RemoveDysonNode_Prefix(DysonSphereLayer __instance, int nodeId)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that user removed node from the dyson plan
            if (!Multiplayer.Session.DysonSpheres.IsIncomingRequest)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSphereRemoveNodePacket(__instance.starData.index, __instance.id, nodeId));
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSphereLayer.NewDysonShell))]
        public static bool NewDysonShell_Prefix(DysonSphereLayer __instance, int protoId, List<int> nodeIds)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that user removed node from the dyson plan
            if (!Multiplayer.Session.DysonSpheres.IsIncomingRequest)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSphereAddShellPacket(__instance.starData.index, __instance.id, protoId, nodeIds));
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(DysonSphereLayer.RemoveDysonShell))]
        public static bool RemoveDysonShell_Prefix(DysonSphereLayer __instance, int shellId)
        {
            if (!Multiplayer.IsActive)
            {
                return true;
            }
            //Notify others that user removed node from the dyson plan
            if (!Multiplayer.Session.DysonSpheres.IsIncomingRequest)
            {
                Multiplayer.Session.Network.SendPacket(new DysonSphereRemoveShellPacket(__instance.starData.index, __instance.id, shellId));
            }
            return true;
        }
    }
}
