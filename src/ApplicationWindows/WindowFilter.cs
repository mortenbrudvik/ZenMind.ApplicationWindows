using ZenMind.ApplicationWindows.SystemApi;

namespace ZenMind.ApplicationWindows
{
    internal static class WindowFilter
    {
        internal static bool NormalWindow(WindowApi window)
        {
            if (IsHiddenWindowStoreApp(window,  window.ClassName)) return false;

            return !window.Styles.IsToolWindow && window.IsVisible;
        }

        private static bool IsHiddenWindowStoreApp(WindowApi window, string className) 
            => (className == "ApplicationFrameWindow" || className == "Windows.UI.Core.CoreWindow") && window.IsCloaked;
    }
}