using System;

namespace Shutdowntor.Common
{
    public static class ScheduleClock
    {
        public static bool HasReached(DateTime now, DateTime targetDateTime)
            => now >= targetDateTime;

        public static TimeSpan Remaining(DateTime now, DateTime targetDateTime)
        {
            var remaining = targetDateTime - now;
            return remaining < TimeSpan.Zero ? TimeSpan.Zero : remaining;
        }
    }
}
