using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class TestEmptyLoop : Xorshift
    {
        public override int FillBufferMultipleRequired => 32;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            var vectorArray = stackalloc uint[4];
            var tVectorArray = stackalloc uint[4];

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

            //Console.WriteLine($"XorshiftUnrolled64Ex : {buf.Length}");
            fixed (byte* pbytes = buf)
            {
                var pbuf = (uint*) (pbytes + offset);
                var pend = (uint*) (pbytes + offsetEnd);

                while (pbuf < pend)
                {
                    pbuf += 16;
                }
            }
            _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
        }
    }
}