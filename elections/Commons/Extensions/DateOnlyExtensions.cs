namespace elections.Commons.Extensions
{
    public static class DateOnlyExtensions
    {
        public static long ToUnixTimestamp(this DateOnly dateOnly)
        {
            var dateTime = dateOnly.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }
    }
}
