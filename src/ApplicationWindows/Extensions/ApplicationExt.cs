using System;
using System.Diagnostics;
using System.Threading;

namespace ZenMind.ApplicationWindows.Extensions
{
    public static class ApplicationExt
    {
        public static Application WaitUntilHandleIsAvailable(this Application application, int timeoutInSeconds = 2)
        {
            var stopwatch = new Stopwatch();
            bool HasNotTimedOut() => (int) Math.Floor(stopwatch.ElapsedMilliseconds / 1000d) < timeoutInSeconds;

            while (application.MainWindowHandle == IntPtr.Zero && HasNotTimedOut())
            {
                Thread.Sleep(20);
            }

            return application;
        }
    }
}