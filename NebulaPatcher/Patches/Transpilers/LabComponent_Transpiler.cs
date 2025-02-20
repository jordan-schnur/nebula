﻿using HarmonyLib;
using NebulaWorld;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NebulaPatcher.Patches.Transpilers
{
    [HarmonyPatch(typeof(LabComponent))]
    internal class LabComponent_Transpiler
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(LabComponent.InternalUpdateResearch))]
        static IEnumerable<CodeInstruction> InternalUpdateResearch_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            //Change: if (ts.hashUploaded >= ts.hashNeeded)
            //To:     if (ts.hashUploaded >= ts.hashNeeded && (!Multiplayer.IsActive || Multiplayer.Session.LocalPlayer.IsHost))
            try
            {
                CodeMatcher matcher = new CodeMatcher(instructions)
                    .MatchForward(true,
                        new CodeMatch(i => i.IsLdarg()),
                        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(TechState), nameof(TechState.hashUploaded))),
                        new CodeMatch(i => i.IsLdarg()),
                        new CodeMatch(OpCodes.Ldfld, AccessTools.Field(typeof(TechState), nameof(TechState.hashNeeded))),
                        new CodeMatch(OpCodes.Blt) //IL 339
                    );
                object label = matcher.Instruction.operand;
                matcher.Advance(1)
                    .InsertAndAdvance(HarmonyLib.Transpilers.EmitDelegate<Func<bool>>(() =>
                    {
                        return !Multiplayer.IsActive || Multiplayer.Session.LocalPlayer.IsHost;
                    }))
                    .InsertAndAdvance(new CodeInstruction(OpCodes.Brfalse_S, label));
                return matcher.InstructionEnumeration();
            }
            catch
            {
                NebulaModel.Logger.Log.Error("LabComponent.InternalUpdateResearch_Transpiler failed. Mod version not compatible with game version.");
                return instructions;
            }
        }
    }
}
