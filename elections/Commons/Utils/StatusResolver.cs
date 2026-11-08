using elections.Commons.Constants;
using static elections.Commons.Constants.Constant.Election;

namespace elections.Commons.Utils
{
    public static class StatusResolver
    {
        public static string? GetStatus(int? status)
        {
            return status switch
            {
                1 => Status.Elected,
                2 => Status.Runoff,
                3 => Status.NotElected,
                _ => null
            };
        }

        public static string GetValidStatus(int status)
        {
            var resolvedStatus = GetStatus(status);

            return resolvedStatus ?? throw new InvalidOperationException($"Invalid status number {status}");
        }
    }
}
