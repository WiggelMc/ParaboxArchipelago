using HarmonyLib;
using UnityEngine;

namespace ParaboxArchipelago.Patches
{
    public static class DrawPatches
    {
        [HarmonyPatch(typeof(Draw), nameof(Draw.DrawText))]
        public static class Draw_DrawText
        {
            public static void Prefix(
                string text,
                Color color,
                TextAnchor alignment,
                ref int fontSize,
                Draw.SquareFont font)
            {
                //ParaboxArchipelagoPlugin.Log.LogInfo("DRAW TEXT " + text);
                if (ParaboxArchipelagoPlugin.State.IsMethodRunning(nameof(Draw_DrawPortal)))
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
            public static void Prefix(
                float centerX,
                float centerY,
                float width,
                float height,
                Floor floor
            )
            {
                //var name = Hub.puzzleData[floor.SceneName].referenceName;
                //ParaboxArchipelagoPlugin.Log.LogInfo("DRAW PORTAL START " + floor.SceneName + " " + name);
                ParaboxArchipelagoPlugin.State.StartMethod(nameof(Draw_DrawPortal));
            }
            
            [HarmonyPriority(100_000)]
            public static void Postfix(
                float centerX,
                float centerY,
                float width,
                float height,
                Floor floor
            )
            {
                //var name = Hub.puzzleData[floor.SceneName].referenceName;
                ParaboxArchipelagoPlugin.State.EndMethod(nameof(Draw_DrawPortal));
                //ParaboxArchipelagoPlugin.Log.LogInfo("DRAW PORTAL END " + floor.SceneName + " " + name);
            }
        }
    }
}