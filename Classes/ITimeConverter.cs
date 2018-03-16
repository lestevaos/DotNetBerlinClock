namespace BerlinClock
{
    public interface ITimeConverter<T>
    {
        T ConvertFrom(string time);
    }
}
