using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutdowntor.Command
{
    public class CmdCommand
    {
        public static readonly string ShutdownCmd = "shutdown -s -t 5";
        public static readonly string RebootCmd = "shutdown -r -t 5";
    }
}
