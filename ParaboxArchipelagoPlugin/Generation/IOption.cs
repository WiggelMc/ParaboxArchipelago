namespace ParaboxArchipelago.Generation
{
    public interface IOption<T>
    {
        bool Is(params T[] values);
    }
}