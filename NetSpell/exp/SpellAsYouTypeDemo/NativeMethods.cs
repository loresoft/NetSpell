using System;
using System.Runtime.InteropServices;

namespace SpellAsYouTypeDemo
{
    /// <summary>
    /// Summary description for NativeMethods.
    /// </summary>
    internal sealed class NativeMethods
    {

        private NativeMethods()
        {
        }

        // Windows Messages 
        internal const int WM_SETREDRAW				= 0x000B; 

        internal const int WM_PAINT					= 0x000F;
        internal const int WM_ERASEBKGND			= 0x0014;
		
        internal const int WM_NOTIFY				= 0x004E;
		
        internal const int WM_HSCROLL				= 0x0114;
        internal const int WM_VSCROLL				= 0x0115;

        internal const int WM_CAPTURECHANGED		= 0x0215;

        internal const int WM_USER					= 0x0400;
		
        // Edit Control the API call
        internal const int EM_POSFROMCHAR = 0x00D6;	
        internal const int EM_CHARFROMPOS = 0x00D7;	

        // Win API declaration
        [DllImport("user32.dll", EntryPoint="SendMessage", CharSet=CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam); 
 
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public int fErase;
            public RECT rcPaint;
            public int fRestore;
            public int fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]  
            public byte[]rgbReserved;
        }
    }
}
