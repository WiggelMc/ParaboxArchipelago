using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ParaboxInjectionLib
{
    public static class LoadLevelInjections
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void LoadWorldLevelCompletionCounts(object block, ref int solvedUncapped, ref int required)
        {
            Debug.Log("LOAD COUNT CALLED");
        }
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void TestInjection()
        {
            
        }
    }
}