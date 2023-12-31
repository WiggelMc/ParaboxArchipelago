using System;
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
                var extraCredits = string.Format("\r\n\r\n\r\n\r\n{0}\r\nWiggel", Localization.Localize("AP_Text_Credits_Randomizer", ""));
                Draw.creditsString = Draw.creditsString.Insert(seperatorIndex, extraCredits);
                Draw.creditsLines = Regex.Matches(Draw.creditsString, "\n").Count;
            }
        }
    }
}