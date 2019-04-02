namespace WindowChromes
{
    /// <summary>
    /// Accent state for Blurrier, ACCENT_ENABLE_ACRYLICBLURBEHIND has been added  
    /// </summary>
    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
        ACCENT_INVALID_STATE = 5
    }

    /* Obsolete
     internal enum AccentState
     {
         ACCENT_DISABLED = 0,
         ACCENT_ENABLE_GRADIENT = 1,
         ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
         ACCENT_ENABLE_BLURBEHIND = 3,
         ACCENT_INVALID_STATE = 4
     }
     */
}
