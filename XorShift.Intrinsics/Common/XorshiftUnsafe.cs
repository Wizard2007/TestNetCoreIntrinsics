namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnsafe : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed (byte* pbytes = buf)
            {
                uint* pbuf = (uint*) (pbytes + offset);
                uint* pend = (uint*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    uint t = _x ^ (_x << 11);
                    _x = _y; _y = _z; _z = _w;
                    _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = _w;
                }
            }
        }
    }
}