using System;

namespace Shutdowntor.Common
{
    public static class ActionParser
    {
        public const string Shutdown = "Shutdown";
        public const string Reboot = "Reboot";

        public static string ToActionName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Shutdown;
            }

            switch (value.Trim().ToLowerInvariant())
            {
                case "r":
                case "rb":
                case "reboot":
                    return Reboot;
                case "s":
                case "sd":
                case "shutdown":
                default:
                    return Shutdown;
            }
        }

        public static bool IsAutoStartToken(string value)
            => !string.IsNullOrWhiteSpace(value);
    }
}
