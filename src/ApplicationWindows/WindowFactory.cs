using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using PInvoke;
using ZenMind.ApplicationWindows.SystemApi;

namespace ZenMind.ApplicationWindows
{
    internal sealed class WindowFactory 
    {
        public WindowApi Create(IntPtr handle)
        {
            Guard.Against.Default(handle, nameof(handle));

            return new WindowApi(handle);
        }

        public IEnumerable<WindowApi> GetWindows(Predicate<WindowApi> match)
        {
            Guard.Against.Null(match, nameof(match));

            var windows = new List<WindowApi>();
            User32.EnumWindows((handle, _) =>
            {
                var window = new WindowApi(handle);
                if(match(window))
                    windows.Add(window);

                return true;
            }, IntPtr.Zero);

            return windows;
        }
    }
}