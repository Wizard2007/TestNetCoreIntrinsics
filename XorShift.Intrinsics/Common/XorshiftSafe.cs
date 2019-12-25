namespace XorShift.Intrinsics
{
    public sealed class XorshiftSafe : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            while (offset < offsetEnd)
            {
                uint t = _x ^ (_x << 11);
                _x = _y; _y = _z; _z = _w;
                _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                buf[offset++] = (byte) (_w & 0xFF);
                buf[offset++] = (byte) ((_w >> 8) & 0xFF);
                buf[offset++] = (byte) ((_w >> 16) & 0xFF);
                buf[offset++] = (byte) ((_w >> 24) & 0xFF);
            }
        }
    }
}