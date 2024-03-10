namespace ParaboxArchipelago.Generation
{
    public interface IFallbackProvider<T>
    {
        FallbackDictionary<T> Fallbacks { get; }
    }
}