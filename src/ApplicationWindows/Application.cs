using System;
using System.Diagnostics;
using Ardalis.GuardClauses;

namespace ZenMind.ApplicationWindows
{
    public sealed class Application
    {
        private readonly Process _process;

        private Application(Process process)
        {
            _process = process;
        }

        public IntPtr MainWindowHandle => _process.MainWindowHandle;
        public Process Process => _process;

        public static Application Launch(string path, string arguments = "")
        {
            Guard.Against.NullOrEmpty(path, nameof(path));
            Guard.Against.Null(arguments, nameof(arguments));
            
            var startInfo = new ProcessStartInfo(path, arguments)
            {
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Minimized
            };

            var process = new Process { StartInfo = startInfo };

            process.Start();

            return new Application(process);
        }

        public void Close()
        {
            _process.Kill();
        }
    }
}