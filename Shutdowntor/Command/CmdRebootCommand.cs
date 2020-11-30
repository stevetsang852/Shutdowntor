using Shutdowntor.Control;

namespace Shutdowntor.Command
{
    public class CmdRebootCommand : ICommand
    {
        public void Execute() => ComputerController.Instance.ExecuteCMDCommand(CmdCommand.RebootCmd);
    }
}
