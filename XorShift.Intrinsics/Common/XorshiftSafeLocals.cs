namespace XorShift.Intrinsics
{
    public sealed class XorshiftSafeLocals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            while (offset < offsetEnd)
            {
                uint t = x ^ (x << 11);
                x = y; y = z; z = w;
                w = w ^ (w >> 19) ^ (t ^ (t >> 8));
                buf[offset++] = (byte) (w & 0xFF);
                buf[offset++] = (byte) ((w >> 8) & 0xFF);
                buf[offset++] = (byte) ((w >> 16) & 0xFF);
                buf[offset++] = (byte) ((w >> 24) & 0xFF);
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }
}