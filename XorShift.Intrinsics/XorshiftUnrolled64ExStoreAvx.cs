using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class XorshiftUnrolled64ExStoreAvx : Xorshift
    {
        public override int FillBufferMultipleRequired => 32;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            var vectorArray = stackalloc uint[8];
            var tVectorArray = stackalloc uint[8];

            uint* pX = vectorArray;
            uint* pY = vectorArray+1;
            uint* pZ = vectorArray+2;
            uint* pW = vectorArray+3;

            *(pX) = _x;
            *(pY) = _y;
            *(pZ) = _z;
            *(pW) = _w;

            uint* pTX = tVectorArray;
            uint* pTY = tVectorArray+1;
            uint* pTZ = tVectorArray+2;
            uint* pTW = tVectorArray+3;

            fixed (byte* pbytes = buf)
            {
                var pbuf = (uint*) (pbytes + offset);
                var pend = (uint*) (pbytes + offsetEnd);

                while (pbuf < pend)
                {
                    var  v = Avx.LoadVector256(vectorArray);

                    //var tXYXW = Avx2.Xor(v, Avx2.ShiftLeftLogical(v, 11));

                    //var tXYZWShifted = Avx2.ShiftRightLogical(tXYXW, 8);

                    //Avx2.Store(tVectorArray, Avx2.Xor(tXYXW, tXYZWShifted));
                    //Avx2.Store(tVectorArray, v);
                    Avx.Store(tVectorArray, v);

                    //*(pbuf++) = *(pX) = *(pW) ^ (*(pW) >> 19) ^ *(pTX);
                    //*(pbuf++) = *(pY) = *(pX) ^ (*(pX) >> 19) ^ *(pTY);
                    //*(pbuf++) = *(pZ) = *(pY) ^ (*(pY) >> 19) ^ *(pTZ);
                    //*(pbuf++) = *(pW) = *(pZ) ^ (*(pZ) >> 19) ^ *(pTW);
                    pbuf += 4;
                }
            }
            _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
        }
    }
}