using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ParaboxArchipelago.Patches
{
    public static class ResourcesPatches
    {
        public static class Resources_Load
        {
            public static void Patch(Harmony harmony)
            {
                var methods = typeof(Resources).GetMethods()
                    .Where(
                        i => i.Name == "Load"
                             && i.ReturnType == typeof(Object)
                             && i.GetParameters().Select(p => p.ParameterType).SequenceEqual(new []{typeof(string)})
                    );
                
                var type = typeof(Resources_Load);
                var prefix = type.GetMethod(nameof(Prefix));
                var postfix = type.GetMethod(nameof(Postfix));
                foreach (var method in methods)
                {
                    harmony.Patch(method, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
                }
            }
            
            public static bool Prefix(ref Object __result, string path)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD CALLED " + path);
                switch (path)
                {
                    case "levels/hub":
                        ParaboxArchipelagoPlugin.Log.LogInfo("LOAD HUB " + ParaboxResources.hub);
                        __result = new TextAsset(ParaboxResources.hub);
                        return false;
                    default:
                        return true;
                }
            }
            
            public static void Postfix(ref Object __result, string path)
            {
                switch (path)
                {
                    case "localization":
                    {
                        var originalLocal = ((__result as TextAsset)!).text;
                        var newLocal = originalLocal + "\n" + ParaboxResources.local;
                        ParaboxArchipelagoPlugin.Log.LogInfo("LOAD LOCAL " + newLocal);
                        __result = new TextAsset(newLocal);
                        break;
                    }
                    case "puzzle_data":
                        var originalPuzzleData = ((__result as TextAsset)!).text;
                        var newPuzzleData = originalPuzzleData.Replace("a1", "a1.1Fool");
                        ParaboxArchipelagoPlugin.Log.LogInfo("LOAD PUZZLE DATA " + newPuzzleData);
                        __result = new TextAsset(newPuzzleData);
                        break;
                }
            }
        }
        
        
        [HarmonyPatch(typeof(LoadLevel), "DoHubModifications")]
        public static class LoadLevel_DoHubModifications
        {
            /*
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
            {
                var unlockScenePrefix = "*clear_count_";
                var index = 0;
                foreach (var instruction in instructions)
                {
                    if (il.ILOffset == 0x0427)
                    {
                        var endLabel = il.DefineLabel();

                        var blockVar = "block";
                        yield return new CodeInstruction(OpCodes.Ldloc_S, blockVar);
                        yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Block), nameof(Block.unlockerScene)));
                        yield return new CodeInstruction(OpCodes.Ldstr, unlockScenePrefix);
                        yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(string), nameof(string.StartsWith), new []{ typeof(string), typeof(string) }));
                        var startsWithPrefixVar = il.DeclareLocal(typeof(bool));
                        yield return new CodeInstruction(OpCodes.Stloc_S, startsWithPrefixVar);


                        yield return new CodeInstruction(OpCodes.Ldloc_S, startsWithPrefixVar);
                        yield return new CodeInstruction(OpCodes.Brfalse_S, endLabel);


                        yield return new CodeInstruction(OpCodes.Nop);


                        yield return new CodeInstruction(OpCodes.Ldloc_S, blockVar);
                        yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(Block), nameof(Block.unlockerScene)));
                        yield return new CodeInstruction(OpCodes.Ldc_I4_S, (sbyte)unlockScenePrefix.Length);
                        yield return new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(string), nameof(string.Substring), new []{ typeof(string), typeof(int) }));
                        var varRequired = il.DeclareLocal(typeof(int));
                        yield return new CodeInstruction(OpCodes.Ldloca_S, varRequired);
                        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(int), nameof(int.TryParse), new []{ typeof(string), typeof(int) }));
                        var varParsed = il.DeclareLocal(typeof(bool));
                        yield return new CodeInstruction(OpCodes.Stloc_S, varParsed);


                        yield return new CodeInstruction(OpCodes.Ldloc_S, varParsed);
                        var varParsed2 = il.DeclareLocal(typeof(bool));
                        yield return new CodeInstruction(OpCodes.Stloc_S, varParsed2);


                        yield return new CodeInstruction(OpCodes.Ldloc_S, varParsed2);
                        yield return new CodeInstruction(OpCodes.Brfalse_S, endLabel);


                        yield return new CodeInstruction(OpCodes.Nop);

                        yield return new CodeInstruction(OpCodes.Ldloc_S, varRequired);
                        var varNum1 = "num1";
                        yield return new CodeInstruction(OpCodes.Stloc_S, varNum1);

                        yield return new CodeInstruction(OpCodes.Nop);
                        var endLabelPoint = new CodeInstruction(OpCodes.Nop);
                        endLabelPoint.labels.Add(endLabel);
                        yield return endLabelPoint;


                        index = 0x0463;
                    }
                    else if (il.ILOffset is > 0x0427 and < 0x0463)
                    {

                    }
                    else
                    {
                        yield return instruction;
                        index += instruction.opcode.Size;
                    }
                }
            }
            */
            
            public static void Postfix()
            {
                var unlockScenePrefix = "*clear_count_";
                var alreadyPlayedAnimation = false;
                for (int index1 = World.blocks.Count - 1; index1 >= 0; --index1)
                {
                    Block block = World.blocks[index1];
                    /*
                    var num1 = 0;
                    if (block.unlockerScene.StartsWith("*clear_count_"))
                    {
                        var parsed = int.TryParse(block.unlockerScene.Substring(13), out var required);
                        if (parsed)
                        {
                            num1 = required;
                        }
                    }
                    */
                    if (block.SubLevel == null && block.unlockerScene != null && block.unlockerScene.StartsWith(unlockScenePrefix))
                    {
                        var parsed = int.TryParse(block.unlockerScene.Substring(unlockScenePrefix.Length), out var required);
                        if (parsed)
                        {
                            block.numRequired = required;
                            block.numSolved = Math.Min(block.numSolvedUncapped, required);
                            if (World.wallUnlockAnimPlayed.ContainsKey(block.OuterLevel.hubAreaName) && !alreadyPlayedAnimation || SaveFile.Current.UnlockPuzzles || !World.hubLoaded)
                            {
                                block.Unlocking = true;
                                World.unlocking = true;
                                if (alreadyPlayedAnimation)
                                    block.UnlockingSuppressSound = true;
                                alreadyPlayedAnimation = true;
                            }
                            else
                            {
                                block.OuterLevel.blocks[block.xpos, block.ypos] = (Block) null;
                                block.OuterLevel.blockList.Remove(block);
                                World.blocks.RemoveAt(index1);
                            }
                            World.wallUnlockAnimPlayed[block.OuterLevel.hubAreaName] = true;
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Resources), nameof(Resources.Load), typeof(string), typeof(Type))]
        public static class Resources_Load_Type
        {
            public static bool Prefix(ref Object __result, string path, Type systemTypeInstance)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD CALLED " + path + " TYPE " + systemTypeInstance);
                return true;
            }
            
            public static void Postfix(ref Object __result, string path, Type systemTypeInstance)
            {
                
            }
        }
    }
}