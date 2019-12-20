using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using A = System.Runtime.Intrinsics.X86.A;

namespace XorShift.Intrinsics
{
    public class XorshiftUnrolled64IntrinsicsUnroled : Xorshift
    {
        #region Private fields

        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        private ulong[] xyzwArray = new ulong[16];

        private ulong[] txyzwArray = new ulong[16];

        #endregion



/*
delegate string StringToString (string s);
static void Main()
{
    MethodInfo trimMethod = typeof (string).GetMethod ("Trim", new Type[0]);
    var trim = (StringToString) Delegate.CreateDelegate(typeof (StringToString), trimMethod);

    for (int i = 0; i < 1000000; i++)
        trim ("test");
}
*/

        public override int FillBufferMultipleRequired => 128;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {

            fixed(ulong* pxyzwArray = xyzwArray, ptxyzwArray = txyzwArray)
            {
                ulong* pX = pxyzwArray;
                ulong* pY = pxyzwArray+4;
                ulong* pZ = pxyzwArray+8;
                ulong* pW = pxyzwArray+12;

                *(pX) = _x;
                *(pY) = _y;
                *(pZ) = _z;
                *(pW) = _w;

                ulong* pTX = ptxyzwArray;
                ulong* pTY = ptxyzwArray+4;
                ulong* pTZ = ptxyzwArray+8;
                ulong* pTW = ptxyzwArray+12;

                fixed (byte* pbytes = buf)
                {
                    var pbuf = (ulong*) (pbytes + offset);
                    var pend = (ulong*) (pbytes + offsetEnd);
                    var x = A.LoadVector256(pxyzwArray);
                    var y = A.LoadVector256(pxyzwArray+4);
                    var z = A.LoadVector256(pxyzwArray+8);
                    var w = A.LoadVector256(pxyzwArray+12);
                    while (pbuf < pend)
                    {
                        // 1 -----------------------------------------------------------------------
                        //ulong tx = x ^ (x << 11);
                        var tx = A.Xor(x, A.ShiftLeftLogical(x, 11));

                        //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                        var ttx =  A.Xor(tx, A.ShiftRightLogical(tx, 8));

                        x = A.Xor(
                                A.Xor(w, ttx),
                                A.ShiftRightLogical(w, 19)
                            );

                        x = A.Xor(
                                A.Xor(
                                    w, A.ShiftRightLogical(w, 19)
                                ),
                                ttx
                            );
                        A.Store(pbuf, x);
                        

                        pbuf += 4;
                        // 2 -----------------------------------------------------------------------
                        var v1 = A.LoadVector256(pxyzwArray);

                        var tXYXW_1 = A.Xor(tx, A.ShiftLeftLogical(v1, 11));

                        var tXYZWShifted1 = A.ShiftRightLogical(tXYXW_1, 8);

                        var t1 =  A.Xor(tXYXW_1, tXYZWShifted1);

                        
                        var tXYXW11 = A.Xor(v1, ttx);
                        var tXYXW21 = A.ShiftRightLogical(v1, 19);
                        var tXYXW31 = A.Xor(tXYXW21, tXYXW11);
                        A.Store(pbuf, tXYXW31);
                        A.Store(pxyzwArray + 4, tXYXW31);
                        pbuf += 4;
                        // 3 -----------------------------------------------------------------------
                        
                        var  v2 = A.LoadVector256(pxyzwArray+4);

                        var tXYXW_2 = A.Xor(v2, A.ShiftLeftLogical(v2, 11));

                        var tXYZWShifted2 = A.ShiftRightLogical(tXYXW_2, 8);

                        var t2 =  A.Xor(tXYXW_2, tXYZWShifted2);

                        
                        var tXYXW12 = A.Xor(v2, ttx);
                        var tXYXW22 = A.ShiftRightLogical(v2, 19);
                        var tXYXW32 = A.Xor(tXYXW22, tXYXW12);
                        A.Store(pbuf, tXYXW32);
                        A.Store(pxyzwArray+8, tXYXW3);
                        
                        pbuf += 4;
                        // 4 -----------------------------------------------------------------------
                        var  v3 = A.LoadVector256(pxyzwArray+8);

                        var tXYXW_3 = A.Xor(v3, A.ShiftLeftLogical(v3, 11));

                        var tXYZWShifted3 = A.ShiftRightLogical(tXYXW_3, 8);

                        var t3 =  A.Xor(tXYXW_3, tXYZWShifted3);
                        
                        var tXYXW13 = A.Xor(v3, ttx);
                        var tXYXW23 = A.ShiftRightLogical(v3, 19);
                        var tXYXW33 = A.Xor(tXYXW23, tXYXW13);
                        A.Store(pbuf, tXYXW33);
                        A.Store(pxyzwArray+12, tXYXW3);

                        pbuf += 4;
                    }
                }
                
                _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
            }
        }
    }
}