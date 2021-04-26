﻿using System;
using System.Diagnostics;
using PInvoke;
using static ZenMind.ApplicationWindows.WindowApi.DwmApi;

namespace ZenMind.ApplicationWindows
{
    /// <summary>
    /// Wrapper for ApplicationWindows API calls (User32, DWMApi etc.)
    /// </summary>
    public class WindowWrapper
    {
        public WindowWrapper(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Window handle must be other than IntPtr.Zero", nameof(handle));
            Handle = handle;
        }

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

        public bool Equals(WindowWrapper other) =>
            Handle.ToInt32().Equals(other?.Handle.ToInt32());

        public override int GetHashCode()
        {
            return ((int)Handle).GetHashCode();
        }
    }
}