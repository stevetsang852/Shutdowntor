using Shutdowntor.Command;
using Xunit;

namespace Shutdowntor.Tests
{
    public class CmdCommandTests
    {
        [Fact]
        public void Shutdown_and_reboot_commands_are_distinct()
        {
            Assert.Contains("-s", CmdCommand.ShutdownCmd);
            Assert.Contains("-r", CmdCommand.RebootCmd);
            Assert.NotEqual(CmdCommand.ShutdownCmd, CmdCommand.RebootCmd);
        }
    }
}
