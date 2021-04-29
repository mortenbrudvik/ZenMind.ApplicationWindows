using System;
using System.Diagnostics;
using Ardalis.GuardClauses;
using PInvoke;
using static ZenMind.ApplicationWindows.SystemApi.DwmApi;

namespace ZenMind.ApplicationWindows.SystemApi
{
    /// <summary>
    /// Wrapper for ApplicationWindows API calls (User32, DWMApi etc.)
    /// </summary>
    internal sealed class WindowApi
    {
        public WindowApi(IntPtr handle) => Handle = Guard.Against.Default(handle, nameof(handle));

        public IntPtr Handle { get; }
        public string Title => User32.GetWindowText(Handle);
        public bool IsVisible => User32.IsWindowVisible(Handle);
        public string ClassName => User32.GetClassName(Handle);
        public WindowStyles Styles => new WindowStyles(Handle);

        public string ProcessName => Process.GetProcessById(ProcessId).ProcessName.Trim();
        public int ProcessId
        {
            get
            {
                User32.GetWindowThreadProcessId(Handle, out var processId);
                return processId;
            }
        }

        public bool IsCloaked
        {
            get
            {
                
                DwmGetWindowAttribute(Handle, DWMWA_CLOAKED, out var cloaked, sizeof(int));
                return cloaked != 0;
            }
        }

        public override bool Equals(object obj) =>
            obj is Window window && Equals(window);

        public bool Equals(WindowApi other) =>
            Handle.ToInt32().Equals(other?.Handle.ToInt32());

        public override int GetHashCode()
        {
            return ((int)Handle).GetHashCode();
        }
    }
}