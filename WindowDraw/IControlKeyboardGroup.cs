namespace ParaboxArchipelago.WindowDraw
{
    public interface IControlKeyboardGroup
    {
        public string[] Items { get; }
        public void OnFinalItemConfirm();
    }
}