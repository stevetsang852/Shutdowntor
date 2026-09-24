using Shutdowntor.Common;
using Xunit;

namespace Shutdowntor.Tests
{
    public class ActionParserTests
    {
        [Theory]
        [InlineData("s", ActionParser.Shutdown)]
        [InlineData("sd", ActionParser.Shutdown)]
        [InlineData("shutdown", ActionParser.Shutdown)]
        [InlineData("S", ActionParser.Shutdown)]
        [InlineData("r", ActionParser.Reboot)]
        [InlineData("rb", ActionParser.Reboot)]
        [InlineData("reboot", ActionParser.Reboot)]
        [InlineData("", ActionParser.Shutdown)]
        [InlineData(null, ActionParser.Shutdown)]
        [InlineData("unknown", ActionParser.Shutdown)]
        public void ToActionName_maps_known_tokens(string input, string expected)
        {
            Assert.Equal(expected, ActionParser.ToActionName(input));
        }
    }
}
