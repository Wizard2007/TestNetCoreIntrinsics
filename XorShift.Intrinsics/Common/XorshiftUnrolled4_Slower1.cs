namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnrolled4_Slower1 : Xorshift
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
                    uint tx = x ^ (x << 11); tx ^= tx >> 8;
                    uint ty = y ^ (y << 11); ty ^= ty >> 8;
                    uint tz = z ^ (z << 11); tz ^= tz >> 8;
                    uint tw = w ^ (w << 11); tw ^= tw >> 8;
                    *(pbuf++) = x = w ^ (w >> 19) ^ tx;
                    *(pbuf++) = y = x ^ (x >> 19) ^ ty;
                    *(pbuf++) = z = y ^ (y >> 19) ^ tz;
                    *(pbuf++) = w = z ^ (z >> 19) ^ tw;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }
}