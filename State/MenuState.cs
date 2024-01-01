namespace ParaboxArchipelago.State
{
    public class MenuState
    {
        public string ConnectSlotInput { get; set; } = "";
        public string ConnectAddressInput { get; set; } = "";
        public string ConnectPasswordInput { get; set; } = "";
        public bool IsInTextField { get; set; } = false;
    }
}