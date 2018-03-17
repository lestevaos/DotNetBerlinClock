namespace BerlinClock
{
    public interface ITimeConverter<T>
    {
        T ConvertFrom(Time time);
    }
}
