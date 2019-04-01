using System.Runtime.InteropServices;

namespace WindowChromes
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public AccentFlags AccentFlags;
        public uint GradientColor;
        public int AnimationId;
    }
}
