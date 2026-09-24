using System;
using Shutdowntor.Common;
using Xunit;

namespace Shutdowntor.Tests
{
    public class StartupOptionsTests
    {
        [Fact]
        public void Parse_defaults_when_no_args()
        {
            var options = StartupOptions.Parse(new string[0]);
            Assert.False(options.Debug);
            Assert.False(options.Hide);
            Assert.False(options.AutoStart);
            Assert.False(options.HasDateTime);
        }

        [Fact]
        public void Parse_flags_and_auto_reboot()
        {
            var options = StartupOptions.Parse(new[] { "/debug", "/hide", "/auto:r" });
            Assert.True(options.Debug);
            Assert.True(options.Hide);
            Assert.True(options.AutoStart);
            Assert.Equal("r", options.ActionToken);
            Assert.Equal(ActionParser.Reboot, ActionParser.ToActionName(options.ActionToken));
        }

        [Fact]
        public void Parse_datetime_when_format_matches()
        {
            var options = StartupOptions.Parse(new[] { "/datetime:20260924180000" });
            Assert.True(options.HasDateTime);
            Assert.Equal(new DateTime(2026, 9, 24, 18, 0, 0), options.TargetDateTime);
        }

        [Fact]
        public void Parse_ignores_invalid_datetime()
        {
            var options = StartupOptions.Parse(new[] { "/datetime:not-a-date" });
            Assert.False(options.HasDateTime);
        }

        [Fact]
        public void Parse_auto_without_value_still_starts()
        {
            var options = StartupOptions.Parse(new[] { "/auto" });
            Assert.True(options.AutoStart);
            Assert.Equal("s", options.ActionToken);
        }
    }
}
