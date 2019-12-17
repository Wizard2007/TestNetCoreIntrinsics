namespace TestApp
{
    public class XorshiftUnrolled64Ex : Xorshift
    {
        public override int FillBufferMultipleRequired => 32;

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
            _x = (uint)x; _y = (uint)y; _z = (uint)z; _w = (uint)w;
        }
    }
}