namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnrolled2Step2Locals : Xorshift
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

                    y = z; z = w;
                    w = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    *(pbuf++) = w;

                    x = y; y = z; z = w;
                    w = w ^ (w >> 19) ^ (ty ^ (ty >> 8));
                    *(pbuf++) = w;
                }
            }
            _x = x; _y = y; _z = z; _w = w;
        }
    }
}