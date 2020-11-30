using Shutdowntor.Control;

namespace Shutdowntor.Command
{
    public class RebootCommand : ICommand
    {
        public void Execute() => ComputerController.Instance.InvokeMethodWin32Shutdown("2");
    }
}
