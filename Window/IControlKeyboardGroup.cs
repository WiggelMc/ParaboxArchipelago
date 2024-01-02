using System.Collections.Generic;

namespace ParaboxArchipelago.Patches
{
    public interface IControlKeyboardGroup
    {
        public string[] Items { get; }
        public void OnFinalItemConfirm();
    }
}