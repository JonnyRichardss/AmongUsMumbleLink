namespace mumblelib
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Avatar
    {
        public uint uiVersion;

        public uint uiTick;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fAvatarPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fAvatarFront;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fAvatarTop;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fCameraPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fCameraFront;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] fCameraTop;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string identity;

        public uint context_len;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] context;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2048)]
        public string description;
    }
}