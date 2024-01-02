using Archipelago.MultiClient.Net;
using JetBrains.Annotations;

namespace ParaboxArchipelago.State
{
    public class APState
    {
        [CanBeNull] public ArchipelagoSession Session { get; set; } = null;
    }
}