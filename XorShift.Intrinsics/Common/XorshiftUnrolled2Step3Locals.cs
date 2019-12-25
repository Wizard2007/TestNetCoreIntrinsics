namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnrolled2Step3Locals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 8; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            uint x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint tx = x ^ (x << 11);
                    uint ty = y ^ (y << 11);
                    x = z;
                    y = w;
                    *(pbuf++) = z = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = w = z ^ (z >> 19) ^ (ty ^ (ty >> 8));
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }
}