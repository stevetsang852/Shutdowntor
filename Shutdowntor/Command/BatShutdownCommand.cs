using Shutdowntor.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Command
{
    public class BatShutdownCommand : ICommand
    {
        public void Execute()
        {
            string bat = "shutdown.bat";
            TextWriter textWriter = new TextWriter(bat);
            textWriter.IfExistsReplace = true;
            textWriter.WriteLine(CmdCommand.ShutdownCmd);
            System.Diagnostics.Process.Start(textWriter.FileInfo.FullName);
        }
    }
}
