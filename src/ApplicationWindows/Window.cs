using System;
using System.Collections.Generic;
using ZenMind.ApplicationWindows.SystemApi;

namespace ZenMind.ApplicationWindows
{
    public sealed class Window
    {
        private readonly WindowApi _windowApi;
        private static readonly WindowFactory _factory = new WindowFactory();

        private Window(WindowApi windowApi) => _windowApi = windowApi;

        public static Window Create(IntPtr windowHandle) =>
            new Window(_factory.Create(windowHandle));
        
        public static IEnumerable<Window> GetWindows()
        {
            return _factory.GetWindows(WindowFilter.NormalWindow)
                .Map(windowApi => new Window(windowApi));
        }
    }
}