using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using HarmonyLib.Tools;
using ParaboxInjectionLib;
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
                        //ParaboxArchipelagoPlugin.Log.LogInfo("LOAD HUB " + ParaboxResources.hub);
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
            
            public static CodeInstruction LogInstruction(CodeInstruction instruction, int sourceInstructionOffset)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo($"IL_{sourceInstructionOffset:x4}: {instruction}");
                return instruction;
            }
            
            public interface IInjectionParameter
            {
                public IEnumerable<CodeInstruction> Load();
            }
            
            public class LocalVarParam : IInjectionParameter
            {
                private readonly int _localIndex;

                public LocalVarParam(LocalVariableInfo localVariable)
                {
                    _localIndex = localVariable.LocalIndex;
                }

                public IEnumerable<CodeInstruction> Load()
                {
                    yield return new CodeInstruction(OpCodes.Ldloc, _localIndex);
                }
            }
            
            public class LocalVarRefParam : IInjectionParameter
            {
                private readonly int _localIndex;

                public LocalVarRefParam(LocalVariableInfo localVariable)
                {
                    _localIndex = localVariable.LocalIndex;
                }

                public IEnumerable<CodeInstruction> Load()
                {
                    yield return new CodeInstruction(OpCodes.Ldloca, _localIndex);
                }
            }
            
            public static IEnumerable<CodeInstruction> InjectMethod(CodeInstruction nextInstruction, int sourceInstructionOffset, MethodInfo method, IInjectionParameter[] parameters)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo("IL_INJECT_START");
                var beforeInstruction = new CodeInstruction(OpCodes.Nop)
                {
                    labels = nextInstruction.labels.ToList()
                };
                yield return LogInstruction(beforeInstruction, sourceInstructionOffset);
                yield return LogInstruction(new CodeInstruction(OpCodes.Nop), sourceInstructionOffset);
                foreach (var parameter in parameters)
                {
                    foreach (var loadInstruction in parameter.Load())
                    {
                        yield return LogInstruction(loadInstruction, sourceInstructionOffset);
                    }
                }
                yield return LogInstruction(new CodeInstruction(OpCodes.Callvirt, method), sourceInstructionOffset);
                yield return LogInstruction(new CodeInstruction(OpCodes.Nop), sourceInstructionOffset);
                ParaboxArchipelagoPlugin.Log.LogInfo("IL_INJECT_END");

                nextInstruction.labels.Clear();
                yield return LogInstruction(nextInstruction, sourceInstructionOffset);
            }
            
            
            
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il, MethodBase original)
            {
                var localVariables = original.GetMethodBody()!.LocalVariables;
                foreach (var variable in localVariables)
                {
                    ParaboxArchipelagoPlugin.Log.LogInfo("VAR: " + variable + " : " + variable.LocalType?.Name);
                }

                var blockVar = localVariables[19]; //block
                var requiredVar = localVariables[23]; //num1
                var solvedVar = localVariables[24]; //num2
                
                var sourceInstructionOffset = 0;

                yield return new CodeInstruction(
                    OpCodes.Callvirt,
                    AccessTools.Method(typeof(LoadLevelInjections), nameof(LoadLevelInjections.TestInjection))
                );
                
                foreach (var instruction in instructions)
                {
                    if (sourceInstructionOffset == 0x0174)
                    {
                        var loadWorldLevelCompletionCounts = InjectMethod(
                            instruction,
                            sourceInstructionOffset,
                            AccessTools.Method(
                                typeof(LoadLevelInjections),
                                nameof(LoadLevelInjections.LoadWorldLevelCompletionCounts)
                            ),
                            new IInjectionParameter[]
                            {
                                new LocalVarParam(blockVar),
                                new LocalVarRefParam(solvedVar),
                                new LocalVarRefParam(requiredVar)
                            }
                        );

                        foreach (var loadWorldLevelCompletionCountsInstruction in loadWorldLevelCompletionCounts)
                        {
                            yield return loadWorldLevelCompletionCountsInstruction;
                        }
                    }
                    else
                    {
                        yield return LogInstruction(instruction, sourceInstructionOffset);
                    }
                    sourceInstructionOffset++;
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
        
        [HarmonyPatch(typeof(LoadLevelInjections), nameof(LoadLevelInjections.TestInjection))]
        public static class LoadLevelInjections_TestInjection
        {
            public static void Prefix()
            {
                
            }
        }
        
        [HarmonyPatch(typeof(LoadLevelInjections), nameof(LoadLevelInjections.LoadWorldLevelCompletionCounts))]
        public static class LoadLevelInjections_LoadWorldLevelCompletionCounts
        {
            public static void Postfix(ref object block, ref int solvedUncapped, ref int required)
            {
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD INJECTION RUN");
                var blockVar = (Block) block;
                ParaboxArchipelagoPlugin.Log.LogInfo("LOAD INJECTION CALLED: " + blockVar.OuterLevel.hubAreaName);
                ParaboxArchipelagoPlugin.Log.LogInfo("OLD EXIT: " + blockVar.numSolved + " / " + blockVar.numRequired + "  : " + blockVar.OuterLevel.hubAreaName);
                
                var unlockScenePrefix = "*clear_count_";
                if (blockVar.unlockerScene .StartsWith(unlockScenePrefix))
                {
                    var parsed = int.TryParse(blockVar.unlockerScene.Substring(unlockScenePrefix.Length), out var newRequired);
                    if (parsed)
                    {
                        ParaboxArchipelagoPlugin.Log.LogInfo("EXIT: " + blockVar.numSolved + " / " + blockVar.numRequired + "  : " + blockVar.OuterLevel.hubAreaName);
                        required = newRequired;
                    }
                }
            }
        }
    }
}