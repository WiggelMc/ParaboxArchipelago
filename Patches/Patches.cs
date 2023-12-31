using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Mono.Cecil;

namespace ParaboxArchipelago.Patches
{
    public class Patches
    {
        // List of assemblies to patch
        public static IEnumerable<string> TargetDLLs { get; } = new[] {"UnityEngine.CoreModule.dll"};

        // Patches the assemblies
        public static void Patch(AssemblyDefinition assembly)
        {
            // Patcher code here
            assembly.Modules
                .SelectMany(m => m.Types)
                .Where(t => t.Namespace == "UnityEngine" && t.Name == "Resources")
                .SelectMany(t => t.Methods)
                .Where(m => m.Name == "Load" && !m.HasGenericParameters)
                .Do(m =>
                {
                    
                });
        }
    }
}