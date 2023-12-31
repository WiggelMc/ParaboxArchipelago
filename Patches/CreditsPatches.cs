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
                Draw.SquareFont font)
            {
                if (text.EndsWith("Priscilla Snow"))
                {
                    float num1 = (float) Mathf.Min(Draw.screenWidth, Draw.screenHeight);
                    float num84 = (float)Draw.screenHeight * 0.82f;
                    float height4 = num1 * 0.12f;
                    float y2 = num84 + PauseMenu.GetLanguageStartupOffset(false);
                    SetDrawRect(0, (double)y2, (double)Draw.screenWidth, (double)height4);
                    Draw.DrawText(string.Format(_("Startup_AGameBy"), (object)"Patrick Traynor"), color, TextAnchor.LowerLeft, fontSize);
                    SetDrawRect(0, (double)(y2 + (float)Draw.screenHeight * 0.06f), (double)Draw.screenWidth, (double)height4);
                    Draw.DrawText(string.Format(_("Startup_WithSoundBy"), (object)"Priscilla Snowd"), color, TextAnchor.LowerLeft, fontSize);
                }
            }
            
            private static readonly MethodInfo SetDrawRectMethod = typeof(Draw).GetMethod("SetDrawRect");
            private static void SetDrawRect(double x, double y, double width, double height)
            {
                typeof(Draw).GetMethod("SetDrawRect")?.Invoke(null, new object[] {x, y, width, height});
            }
        }
        
        private static string _(string s, string language = "") => Localization.Localize(s, language);
    }
}