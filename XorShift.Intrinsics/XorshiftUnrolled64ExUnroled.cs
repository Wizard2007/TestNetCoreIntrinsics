using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class XorshiftUnrolled64ExUnroled : Xorshift
    {
        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;
        public override int FillBufferMultipleRequired => 32;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            var vectorArray = stackalloc ulong[8];
            var tVectorArray = stackalloc ulong[8];

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

            //Console.WriteLine($"XorshiftUnrolled64Ex : {buf.Length}");
            fixed (byte* pbytes = buf)
            {
                var pbuf = (ulong*) (pbytes + offset);
                var pend = (ulong*) (pbytes + offsetEnd);

                while (pbuf < pend)
                {
                    var  v = Avx2.LoadVector256(vectorArray);

                    var tXYXW = Avx2.Xor(v, Avx2.ShiftLeftLogical(v, 11));

                    var tXYZWShifted = Avx2.ShiftRightLogical(tXYXW, 8);

                    var t =  Avx2.Xor(tXYXW, tXYZWShifted);

                    //Avx2.Store(tVectorArray, t);

                    //*(pbuf++) = *(pX) = *(pW) ^ (*(pW) >> 19) ^ *(pTX);
                    var tXYXW1 = Avx2.Xor(v, t);
                    var tXYXW2 = Avx2.ShiftRightLogical(v, 19);
                    var tXYXW3 = Avx2.Xor(tXYXW2, tXYXW1);
                    Avx2.Store(tVectorArray, tXYXW3);

                    //*(pbuf++) = *(pY) = *(pX) ^ (*(pX) >> 19) ^ *(pTY);
                    //*(pbuf++) = *(pZ) = *(pY) ^ (*(pY) >> 19) ^ *(pTZ);
                    //*(pbuf++) = *(pW) = *(pZ) ^ (*(pZ) >> 19) ^ *(pTW);
                    
                    var  v1 = Avx2.LoadVector256(vectorArray);

                    var tXYXW_1 = Avx2.Xor(v, Avx2.ShiftLeftLogical(v1, 11));

                    var tXYZWShifted1 = Avx2.ShiftRightLogical(tXYXW_1, 8);

                    var t1 =  Avx2.Xor(tXYXW_1, tXYZWShifted1);

                    
                    var tXYXW11 = Avx2.Xor(v1, t);
                    var tXYXW21 = Avx2.ShiftRightLogical(v1, 19);
                    var tXYXW31 = Avx2.Xor(tXYXW21, tXYXW11);
                    Avx2.Store(tVectorArray, tXYXW31);
                    
                    //----------------------
                    
                    var  v2 = Avx2.LoadVector256(vectorArray);

                    var tXYXW_2 = Avx2.Xor(v2, Avx2.ShiftLeftLogical(v2, 11));

                    var tXYZWShifted2 = Avx2.ShiftRightLogical(tXYXW_2, 8);

                    var t2 =  Avx2.Xor(tXYXW_2, tXYZWShifted2);

                    
                    var tXYXW12 = Avx2.Xor(v2, t);
                    var tXYXW22 = Avx2.ShiftRightLogical(v2, 19);
                    var tXYXW32 = Avx2.Xor(tXYXW22, tXYXW12);
                    Avx2.Store(tVectorArray, tXYXW32);
                    
                    //----------------------
                    var  v3 = Avx2.LoadVector256(vectorArray);

                    var tXYXW_3 = Avx2.Xor(v3, Avx2.ShiftLeftLogical(v3, 11));

                    var tXYZWShifted3 = Avx2.ShiftRightLogical(tXYXW_3, 8);

                    var t3 =  Avx2.Xor(tXYXW_3, tXYZWShifted3);

                    
                    var tXYXW13 = Avx2.Xor(v3, t);
                    var tXYXW23 = Avx2.ShiftRightLogical(v3, 19);
                    var tXYXW33 = Avx2.Xor(tXYXW23, tXYXW13);
                    Avx2.Store(tVectorArray, tXYXW33);
                    pbuf += 16;
                }
            }
            _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
        }
    }
}