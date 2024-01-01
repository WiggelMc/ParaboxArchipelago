using HarmonyLib;
using UnityEngine;

namespace ParaboxArchipelago.Patches
{
    public static class DrawPatches
    {
        [HarmonyPatch(typeof(Draw), nameof(Draw.DrawText))]
        public static class Draw_DrawText
        {
            public static void Prefix(string text, ref int fontSize)
            {
                if (ParaboxArchipelagoPlugin.MethodState.IsMethodRunning(nameof(Draw_DrawPortal)))
                {
                    fontSize = Mathf.RoundToInt(fontSize * GetFontScale(text));
                }
            }

            private static float GetFontScale(string text)
            {
                var length = text.Length;
                if (length <= 2)
                    return 1;
                return 2.5f / length;
            }
        }
        
        [HarmonyPatch(typeof(Draw), "DrawPortal")]
        public static class Draw_DrawPortal
        {
            [HarmonyPriority(-100_000)]
            public static void Prefix()
            {
                ParaboxArchipelagoPlugin.MethodState.StartMethod(nameof(Draw_DrawPortal));
            }
            
            [HarmonyPriority(100_000)]
            public static void Postfix()
            {
                ParaboxArchipelagoPlugin.MethodState.EndMethod(nameof(Draw_DrawPortal));
            }
        }
    }
}