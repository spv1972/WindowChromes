﻿using System.Diagnostics.CodeAnalysis;

namespace WindowChromes
{
    /// <summary>
    /// DWMNCRENDERINGPOLICY. DWMNCRP_*
    /// </summary>
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    internal enum DWMNCRP
    {
        USEWINDOWSTYLE=0,
        DISABLED=1,
        ENABLED,
        //LAST
    }
}
