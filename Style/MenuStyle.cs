using UnityEngine;

namespace ParaboxArchipelago.Style
{
    public static class MenuStyle
    {
        public static readonly Texture2D InvisibleTexture;
        public static readonly GUIStyle InvisibleStyle;
        public static readonly Texture2D DragRectTexture;
        public static readonly GUIStyle DragRectStyle;
        public static readonly Texture2D DragWindowTexture;
        public static readonly GUIStyle DragWindowStyle;
        
        static MenuStyle()
        {
            var invisibleTexture = new Texture2D(1,1);
            invisibleTexture.SetPixel(1,1,new Color(0,0,0,0));
            InvisibleTexture = invisibleTexture;
            InvisibleStyle = GUIStyle.none;
            
            var dragRectTexture = new Texture2D(1,1);
            dragRectTexture.SetPixel(1,1,new Color(1,1,1,0.2f));
            DragRectTexture = dragRectTexture;
            DragRectStyle = new GUIStyle
            {
                border = new RectOffset(),
                normal = new GUIStyleState
                {
                    background = dragRectTexture
                }
            };
            
            var dragWindowTexture = new Texture2D(1,1);
            dragWindowTexture.SetPixel(1,1,new Color(0,0,0,0.2f));
            DragWindowTexture = dragWindowTexture;
            DragWindowStyle = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    background = dragWindowTexture
                }
            };
        }
    }
}