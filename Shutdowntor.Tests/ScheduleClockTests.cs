using System;
using Shutdowntor.Common;
using Xunit;

namespace Shutdowntor.Tests
{
    public class ScheduleClockTests
    {
        [Fact]
        public void HasReached_is_false_before_target()
        {
            var target = new DateTime(2026, 9, 24, 18, 0, 0);
            Assert.False(ScheduleClock.HasReached(target.AddMinutes(-5), target));
        }

        [Fact]
        public void HasReached_is_true_at_or_after_target()
        {
            var target = new DateTime(2026, 9, 24, 18, 0, 0);
            Assert.True(ScheduleClock.HasReached(target, target));
            Assert.True(ScheduleClock.HasReached(target.AddHours(2), target));
        }

        [Fact]
        public void Remaining_does_not_go_negative()
        {
            var target = new DateTime(2026, 9, 24, 18, 0, 0);
            Assert.Equal(TimeSpan.Zero, ScheduleClock.Remaining(target.AddMinutes(1), target));
            Assert.Equal(TimeSpan.FromMinutes(3), ScheduleClock.Remaining(target.AddMinutes(-3), target));
        }
    }
}
