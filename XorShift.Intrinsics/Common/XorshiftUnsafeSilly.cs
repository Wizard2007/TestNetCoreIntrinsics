namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnsafeSilly : Xorshift
    {
        public override int FillBufferMultipleRequired { get { return 4; } }

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed (byte* pbytes = buf)
            {
                byte* pbuf = pbytes + offset;
                byte* pend = pbytes + offsetEnd;
                while (pbuf < pend)
                {
                    uint t = _x ^ (_x << 11);
                    _x = _y; _y = _z; _z = _w;
                    _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
                    *(pbuf++) = (byte) (_w & 0xFF);
                    *(pbuf++) = (byte) ((_w >> 8) & 0xFF);
                    *(pbuf++) = (byte) ((_w >> 16) & 0xFF);
                    *(pbuf++) = (byte) ((_w >> 24) & 0xFF);
                }
            }
        }
    }
}