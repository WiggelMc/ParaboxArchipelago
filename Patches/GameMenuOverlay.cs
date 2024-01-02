using System.IO;
using ParaboxArchipelago.Patches;
using ParaboxArchipelago.State;
using UnityEngine;

namespace ParaboxArchipelago.Window
{
    public static class GameMenuOverlay
    {
        private static readonly Texture2D ImageApOptions;
        private static readonly Texture2D ImageApOptionsOpen;
        
        static GameMenuOverlay()
        {
            var imageAPOptions = new Texture2D(128, 128);
            var imageAPOptionsBytes = (byte[]) ParaboxResources.ResourceManager.GetObject("imageCog");
            imageAPOptions.LoadImage(imageAPOptionsBytes);
            ImageApOptions = imageAPOptions;
            
            var imageAPOptionsOpen = new Texture2D(128, 128);
            var imageAPOptionsOpenBytes = (byte[]) ParaboxResources.ResourceManager.GetObject("imageBell");
            imageAPOptionsOpen.LoadImage(imageAPOptionsOpenBytes);
            ImageApOptionsOpen = imageAPOptionsOpen;
        }
        
        public static void DrawMenuOverlay()
        {
            var menuState = ParaboxArchipelagoPlugin.MenuState;
            var optionsEnabled = menuState.APOptionsPageEnabled;

            var screenWidth = Draw.screenWidth;
            var screenHeight = Draw.screenHeight;
            
            var size = 100;
            var margin = 20;
            var buttonPos = new Rect(screenWidth-size-margin, screenHeight-size-margin, size, size);
            var buttonImage = menuState.APOptionsPageEnabled ? ImageApOptionsOpen : ImageApOptions;
            var buttonPressed = GUI.Button(buttonPos, buttonImage);

            if (buttonPressed)
            {
                if (menuState.APOptionsPageEnabled)
                {
                    MenuPatches.CloseApOptions();
                }
                else
                {
                    MenuPatches.OpenAPOptions();
                }
                
                ParaboxArchipelagoPlugin.Log.LogInfo("AP OPTIONS OPEN: " + menuState.APOptionsPageEnabled);
            }
            
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