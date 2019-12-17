using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class XorshiftUnrolled64Ex : Xorshift
    {
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
                ulong* pbuf = (ulong*) (pbytes + offset);
                ulong* pend = (ulong*) (pbytes + offsetEnd);
                while (pbuf < pend)
                {
                    var  v = Avx2.LoadVector256(vectorArray);
                    var shifted = Avx2.ShiftLeftLogical(v, 11);
                    var tVector = Avx2.Xor(v, shifted);
                    var tVectorR = Avx2.ShiftRightLogical(tVector, 8);
                    var tResult = Avx2.Xor(tVector, tVectorR);
                    Avx2.Store(tVectorArray, tResult);

                    *(pbuf++) = *(pX) = *(pW) ^ (*(pW) >> 19) ^ *(pTX);
                    *(pbuf++) = *(pY) = *(pX) ^ (*(pX) >> 19) ^ *(pTY);
                    *(pbuf++) = *(pZ) = *(pY) ^ (*(pY) >> 19) ^ *(pTZ);
                    *(pbuf++) = *(pW) = *(pZ) ^ (*(pZ) >> 19) ^ *(pTW);
                }
            }
            _x = (uint)*(pX); _y = (uint)*(pY); _z = (uint)*(pZ); _w = (uint)*(pW);
        }
    }
}