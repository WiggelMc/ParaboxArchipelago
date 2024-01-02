using System.IO;
using ParaboxArchipelago.Patches;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public static class GameMenuOverlay
    {
        private static readonly Texture2D ImageApOptions;
        
        static GameMenuOverlay()
        {
            var imageAPOptions = new Texture2D(128, 128);
            var imageAPOptionsBytes = (byte[]) ParaboxResources.ResourceManager.GetObject("imageBell");
            imageAPOptions.LoadImage(imageAPOptionsBytes);
            ImageApOptions = imageAPOptions;
        }
        
        public static void DrawMenuOverlay()
        {
            var optionsEnabled = ParaboxArchipelagoPlugin.MenuState.APOptionsPageEnabled;

            GUI.DrawTexture(new Rect(0,0,100,100), ImageApOptions);
            
            if (optionsEnabled)
            {
                DrawOptionsOverlay();
            }
        }

        private static void DrawOptionsOverlay()
        {
            
        }
    }
}