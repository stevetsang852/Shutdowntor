using Shutdowntor.Control;

namespace Shutdowntor.Command
{
    public class CmdShutdownCommand : ICommand
    {
        public void Execute() => ComputerController.Instance.ExecuteCMDCommand(CmdCommand.ShutdownCmd);
    }
}
