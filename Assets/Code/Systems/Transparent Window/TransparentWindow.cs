using UnityEngine;
using System.Runtime.InteropServices;

public static class TransparentWindow
{
    #region APICalls
    
    [DllImport("user32.dll", EntryPoint = "SetWindowLongA")]
    static extern int SetWindowLong(int hwnd, int nIndex, long dwNewLong);
    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(int hWnd, int nCmdShow);
    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(int hwnd, int crKey, byte bAlpha, int dwFlags);
    [DllImport("user32.dll", EntryPoint = "GetActiveWindow")]
    private static extern int GetActiveWindow();
    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    private static extern long GetWindowLong(int hwnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern int SetWindowPos(int hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags); 
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    
    [DllImport("Dwmapi.dll", EntryPoint = "DwmExtendFrameIntoClientArea")]
    private static extern uint DwmExtendFrameIntoClientArea(int hwnd, ref MARGINS margins);

    #endregion

    #region Constants
    
    // Window Styles
    private const int GWL_STYLE = -16;
    private const int GWL_EXSTYLE = -20;
    
    // Window Style Attributes
    private const int WS_CAPTION = 0X00C00000;
    private const int WS_HSCROLL = 0X00100000;
    private const int WS_VSCROLL = 0X00200000;
    private const int WS_SYSMENU = 0X00080000;
    private const int WS_MAXIMIZE = 0X01000000;
    private const int WS_SIZEBOX = 0X00040000;
    
    // Extended Window Style Attributes
    private const int WS_EX_LAYERED = 0X00080000;
    private const int WS_EX_TRANSPARENT = 0X00000020;
    
    // Layered Window Attributes
    private const int LWA_ALPHA = 0X00000002;
    private const int LWA_COLORKEY = 0X00000001;
    
    // Z Axis Position Handle
    private const int HWND_TOPMOST = -1;
    
    // Window Position Flags
    private const int SWP_FRAMECHANGED = 0X00000020;
    private const int SWP_SHOWWINDOW = 0X00000040;
    
    // Show Window Commands
    private const int SW_SHOWMAXIMIZED = 3;
    
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Init()
    {
#if !UNITY_EDITOR
        SetWindowTransparent();
#endif
    }

    private static void SetWindowTransparent()
    {
        int handle = GetActiveWindow();
        int fWidth = Screen.width;
        int fHeight = Screen.height;

        // Remove title bar
        long lCurStyle = GetWindowLong(handle, GWL_STYLE);
        lCurStyle &= ~(WS_CAPTION | WS_HSCROLL | WS_VSCROLL | WS_SYSMENU);
        lCurStyle &= WS_MAXIMIZE;
        lCurStyle |= WS_SIZEBOX;
        SetWindowLong(handle, GWL_STYLE, lCurStyle);
        
        // Transparent windows with click through
        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(handle, ref margins);
        SetWindowLong(handle, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        SetLayeredWindowAttributes(handle, 0, 0, LWA_ALPHA | LWA_COLORKEY);

        SetWindowPos(handle, HWND_TOPMOST, 0, 0, fWidth, fHeight, SWP_FRAMECHANGED | SWP_SHOWWINDOW);
        ShowWindowAsync(handle, SW_SHOWMAXIMIZED);
    }
}