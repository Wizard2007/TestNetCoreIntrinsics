namespace TestApp
{
    public sealed class XorshiftUnrolled64 : Xorshift
    {
        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        public override int FillBufferMultipleRequired { get { return 32; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            ulong x = _x, y = _y, z = _z, w = _w;
            fixed (byte* pbytes = buf)
            {
                ulong* pbuf = (ulong*) (pbytes + offset);
                ulong* pend = (ulong*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    ulong tx = x ^ (x << 11);
                    ulong ty = y ^ (y << 11);
                    ulong tz = z ^ (z << 11);
                    ulong tw = w ^ (w << 11);
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