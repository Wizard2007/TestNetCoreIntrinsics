namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnrolled4Locals : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 16; } }

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
                    uint tz = z ^ (z << 11);
                    uint tw = w ^ (w << 11);
                    *(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                    *(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }
}