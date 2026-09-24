using System;
using System.Globalization;

namespace Shutdowntor.Common
{
    public sealed class StartupOptions
    {
        public bool Debug { get; private set; }
        public bool Hide { get; private set; }
        public bool AutoStart { get; private set; }
        public bool HasDateTime { get; private set; }
        public string ActionToken { get; private set; } = "s";
        public DateTime TargetDateTime { get; private set; } = DateTime.Now.AddDays(1);

        public static StartupOptions Parse(string[] args)
        {
            var options = new StartupOptions();
            if (args == null)
            {
                return options;
            }

            foreach (var raw in args)
            {
                if (string.IsNullOrWhiteSpace(raw))
                {
                    continue;
                }

                var arg = raw.Trim();
                if (!arg.StartsWith("/", StringComparison.Ordinal))
                {
                    continue;
                }

                var body = arg.Substring(1);
                var parts = body.Split(new[] { ':' }, 2);
                var key = parts[0].ToLowerInvariant();
                var value = parts.Length > 1 ? parts[1] : string.Empty;

                switch (key)
                {
                    case "debug":
                        options.Debug = true;
                        break;
                    case "hide":
                        options.Hide = true;
                        break;
                    case "auto":
                        options.AutoStart = true;
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            options.ActionToken = value;
                        }
                        break;
                    case "datetime":
                        if (DateTime.TryParseExact(value, Global.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
                        {
                            options.HasDateTime = true;
                            options.TargetDateTime = parsed;
                        }
                        break;
                }
            }

            return options;
        }
    }
}
