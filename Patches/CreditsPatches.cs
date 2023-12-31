using System;
using System.Reflection;
using System.Text.RegularExpressions;
using HarmonyLib;
using UnityEngine;

namespace ParaboxArchipelago.Patches
{
    public static class CreditsPatches
    {
        [HarmonyPatch(typeof(Draw), nameof(Draw.ConstructCreditsString))]
        public static class Draw_ConstructCreditsString
        {
            private static void Postfix()
            {
                const string seperator = "MoGi";
                var seperatorIndex = Draw.creditsString.IndexOf(seperator, StringComparison.Ordinal) + seperator.Length;
                
                // ReSharper disable once UseStringInterpolation
                var extraCredits = string.Format("\r\n\r\n\r\n\r\n{0}\r\nWiggel", _("AP_Text_Credits_Randomizer"));
                Draw.creditsString = Draw.creditsString.Insert(seperatorIndex, extraCredits);
                Draw.creditsLines = Regex.Matches(Draw.creditsString, "\n").Count;
            }
        }

        [HarmonyPatch(typeof(Draw), nameof(Draw.DrawText))]
        public static class Draw_DrawText
        {
            public static void Postfix(
                ref string text,
                Color color,
                TextAnchor alignment,
                int fontSize,
                Draw.SquareFont font,
                ref Rect ___drawRect
            )
            {
                if (text.EndsWith("Priscilla Snow"))
                {
                    float num1 = (float) Mathf.Min(Draw.screenWidth, Draw.screenHeight);
                    float num84 = (float)Draw.screenHeight * 0.75f;
                    float height4 = num1 * 0.12f;
                    float y2 = num84 + PauseMenu.GetLanguageStartupOffset(false);
                    
                    float margin = Draw.screenHeight * 0.05f;
                    
                    SetDrawRect(ref ___drawRect, margin, (double)y2, (double)Draw.screenWidth, (double)height4);
                    Draw.DrawText(string.Format(_("AP_Startup_RandomizerBy"), (object)"Wiggel"), color, TextAnchor.LowerLeft, fontSize);
                    SetDrawRect(ref ___drawRect,margin, (double)(y2 + (float)Draw.screenHeight * 0.06f), (double)Draw.screenWidth, (double)height4);
                    Draw.DrawText(string.Format(_("AP_Startup_Version"), ParaboxPluginInfo.PLUGIN_VERSION), color, TextAnchor.LowerLeft, fontSize);
                }
            }
        }
        
        private static void SetDrawRect(ref Rect drawRect, double x, double y, double width, double height)
        {
            drawRect.Set((float) (int) x, (float) (int) y, (float) ((int) (x + width) - (int) x), (float) ((int) (y + height) - (int) y));
        }
        
        private static string _(string s, string language = "") => Localization.Localize(s, language);
    }
}