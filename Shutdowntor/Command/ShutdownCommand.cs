using Shutdowntor.Control;

namespace Shutdowntor.Command
{
    public class ShutdownCommand : ICommand
    {
        public void Execute() => ComputerController.Instance.InvokeMethodWin32Shutdown("5");
    }
}
