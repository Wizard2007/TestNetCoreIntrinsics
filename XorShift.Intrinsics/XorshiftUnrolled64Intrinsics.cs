using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class XorshiftUnrolled64Intrinsics : Xorshift
    {
        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;
        public override int FillBufferMultipleRequired => 32;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            var vectorArray = stackalloc ulong[4];
            var tVectorArray = stackalloc ulong[4];

            ulong* pX = vectorArray;
            ulong* pY = vectorArray+1;
            ulong* pZ = vectorArray+2;
            ulong* pW = vectorArray+3;

            *(pX) = _x;
            *(pY) = _y;
            *(pZ) = _z;
            *(pW) = _w;

            ulong* pTX = tVectorArray;
            ulong* pTY = tVectorArray+1;
            ulong* pTZ = tVectorArray+2;
            ulong* pTW = tVectorArray+3;

            fixed (byte* pbytes = buf)
            {
                var pbuf = (ulong*) (pbytes + offset);
                var pend = (ulong*) (pbytes + offsetEnd);

                while (pbuf < pend)
                {
                    //ulong tx = x ^ (x << 11);
                    //ulong ty = y ^ (y << 11);
                    //ulong tz = z ^ (z << 11);
                    //ulong tw = w ^ (w << 11);                    
                    var  v = Avx2.LoadVector256(vectorArray);
                    var tXYXW = Avx2.Xor(v, Avx2.ShiftLeftLogical(v, 11));
                    var tXYZWShifted = Avx2.ShiftRightLogical(tXYXW, 8);
                    Avx2.Store(tVectorArray, Avx2.Xor(tXYXW, tXYZWShifted));

                    //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    //*(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                    //*(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                    //*(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));                    
                    *(pbuf++) = *(pX) = *(pW) ^ (*(pW) >> 19) ^ *(pTX);
                    *(pbuf++) = *(pY) = *(pX) ^ (*(pX) >> 19) ^ *(pTY);
                    *(pbuf++) = *(pZ) = *(pY) ^ (*(pY) >> 19) ^ *(pTZ);
                    *(pbuf++) = *(pW) = *(pZ) ^ (*(pZ) >> 19) ^ *(pTW);
                    //pbuf += 4;
                }
            }
            _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
        }
    }
}