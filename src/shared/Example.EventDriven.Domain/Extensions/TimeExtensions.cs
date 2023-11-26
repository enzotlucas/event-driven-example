namespace Example.EventDriven.Domain.Extensions
{
    public static class TimeExtensions
    {
        public static int AsMiliseconds(this int timeInSeconds)
        {
            return timeInSeconds * 1000;
        }
    }
}
