namespace ParaboxArchipelago.GameWindow
{
    public interface IControlKeyboardGroup
    {
        public string[] Items { get; }
        public void OnFinalItemConfirm();
    }
}