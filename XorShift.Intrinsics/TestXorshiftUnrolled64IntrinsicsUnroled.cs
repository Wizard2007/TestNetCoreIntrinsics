using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class TestXorshiftUnrolled64IntrinsicsUnroled : Xorshift
    {
        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;
        public override int FillBufferMultipleRequired => 128;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            var xyzwArray = stackalloc ulong[16];
            var txyzwArray = stackalloc ulong[16];

            ulong* pX = xyzwArray;
            ulong* pY = xyzwArray+4;
            ulong* pZ = xyzwArray+8;
            ulong* pW = xyzwArray+12;

            *(pX) = _x;
            *(pY) = _y;
            *(pZ) = _z;
            *(pW) = _w;

            ulong* pTX = txyzwArray;
            ulong* pTY = txyzwArray+4;
            ulong* pTZ = txyzwArray+8;
            ulong* pTW = txyzwArray+12;

            fixed (byte* pbytes = buf)
            {
                var pbuf = (ulong*) (pbytes + offset);
                var pend = (ulong*) (pbytes + offsetEnd);

                while (pbuf < pend)
                {
                    // 1 -----------------------------------------------------------------------
                    //ulong tx = x ^ (x << 11);
                    var v = Avx2.LoadVector256(xyzwArray+12);
                    var tXYXW = Avx2.Xor(v, Avx2.ShiftLeftLogical(v, 11));

                    //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                    var tXYZWShifted = Avx2.ShiftRightLogical(tXYXW, 8);
                    var t =  Avx2.Xor(tXYXW, tXYZWShifted);
                    var tXYXW1 = Avx2.Xor(v, t);
                    var tXYXW2 = Avx2.ShiftRightLogical(v, 19);
                    var tXYXW3 = Avx2.Xor(tXYXW2, tXYXW1);
                    Avx2.Store(pbuf, tXYXW3);
                    Avx2.Store(xyzwArray, tXYXW3);

                    pbuf += 4;
                    // 2 -----------------------------------------------------------------------
                    var v1 = Avx2.LoadVector256(xyzwArray);

                    var tXYXW_1 = Avx2.Xor(v, Avx2.ShiftLeftLogical(v1, 11));

                    var tXYZWShifted1 = Avx2.ShiftRightLogical(tXYXW_1, 8);

                    var t1 =  Avx2.Xor(tXYXW_1, tXYZWShifted1);

                    
                    var tXYXW11 = Avx2.Xor(v1, t);
                    var tXYXW21 = Avx2.ShiftRightLogical(v1, 19);
                    var tXYXW31 = Avx2.Xor(tXYXW21, tXYXW11);
                    Avx2.Store(pbuf, tXYXW31);
                    Avx2.Store(xyzwArray + 4, tXYXW3);
                    pbuf += 4;
                    // 3 -----------------------------------------------------------------------
                    
                    var  v2 = Avx2.LoadVector256(xyzwArray+4);

                    var tXYXW_2 = Avx2.Xor(v2, Avx2.ShiftLeftLogical(v2, 11));

                    var tXYZWShifted2 = Avx2.ShiftRightLogical(tXYXW_2, 8);

                    var t2 =  Avx2.Xor(tXYXW_2, tXYZWShifted2);

                    
                    var tXYXW12 = Avx2.Xor(v2, t);
                    var tXYXW22 = Avx2.ShiftRightLogical(v2, 19);
                    var tXYXW32 = Avx2.Xor(tXYXW22, tXYXW12);
                    Avx2.Store(pbuf, tXYXW32);
                    Avx2.Store(xyzwArray+8, tXYXW3);
                    
                    pbuf += 4;
                    // 4 -----------------------------------------------------------------------
                    var  v3 = Avx2.LoadVector256(xyzwArray+8);

                    var tXYXW_3 = Avx2.Xor(v3, Avx2.ShiftLeftLogical(v3, 11));

                    var tXYZWShifted3 = Avx2.ShiftRightLogical(tXYXW_3, 8);

                    var t3 =  Avx2.Xor(tXYXW_3, tXYZWShifted3);
                    
                    var tXYXW13 = Avx2.Xor(v3, t);
                    var tXYXW23 = Avx2.ShiftRightLogical(v3, 19);
                    var tXYXW33 = Avx2.Xor(tXYXW23, tXYXW13);
                    Avx2.Store(pbuf, tXYXW33);
                    Avx2.Store(xyzwArray+12, tXYXW3);

                    pbuf += 4;
                }
            }
            _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
        }
    
    }
}