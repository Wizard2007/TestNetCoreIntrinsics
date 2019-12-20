using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
//using A = System.Runtime.Intrinsics.X86.Avx2;

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

        public XorshiftUnrolled64IntrinsicsUnroled()
        {
            xyzwArray[0] = _x;
            xyzwArray[1] = _x;
            xyzwArray[2] = _x;
            xyzwArray[3] = _x;
        }

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
                    var x = Avx2.LoadVector256(pxyzwArray);
                    var y = Avx2.LoadVector256(pxyzwArray+4);
                    var z = Avx2.LoadVector256(pxyzwArray+8);
                    var w = Avx2.LoadVector256(pxyzwArray+12);
                    while (pbuf < pend)
                    {
                        // 1 -----------------------------------------------------------------------
                        //ulong tx = x ^ (x << 11);
                        var tx = Avx2.Xor(x, Avx2.ShiftLeftLogical(x, 11));

                        //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                        var ttx =  Avx2.Xor(tx, Avx2.ShiftRightLogical(tx, 8));

                        x = Avx2.Xor(
                                Avx2.Xor(w, ttx),
                                Avx2.ShiftRightLogical(w, 19)
                            );

                        // save results
                        Avx2.Store(pbuf, x);
                        pbuf += 4;

                        // 2 -----------------------------------------------------------------------

                        //ulong ty = y ^ (y << 11);
                        var ty = Avx2.Xor(y, Avx2.ShiftLeftLogical(y, 11));

                        //*(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                        var tty =  Avx2.Xor(ty, Avx2.ShiftRightLogical(ty, 8));

                        y = Avx2.Xor(
                                Avx2.Xor(x, tty),
                                Avx2.ShiftRightLogical(x, 19)
                            );
                            
                        // save results
                        Avx2.Store(pbuf, y);
                        pbuf += 4;

                        // 3 -----------------------------------------------------------------------
                        //ulong tz = z ^ (z << 11);
                        var tz = Avx2.Xor(z, Avx2.ShiftLeftLogical(z, 11));

                        //*(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                        var ttz =  Avx2.Xor(tz, Avx2.ShiftRightLogical(tz, 8));

                        z = Avx2.Xor(
                                Avx2.Xor(y, ttz),
                                Avx2.ShiftRightLogical(y, 19)
                            );
                            
                        // save results
                        Avx2.Store(pbuf, z);
                        pbuf += 4;

                        // 4 -----------------------------------------------------------------------
                        //ulong tw = w ^ (w << 11);
                        var tw = Avx2.Xor(w, Avx2.ShiftLeftLogical(w, 11));

                        //*(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                        var ttw =  Avx2.Xor(tz, Avx2.ShiftRightLogical(tz, 8));

                        w = Avx2.Xor(
                                Avx2.Xor(z, ttw),
                                Avx2.ShiftRightLogical(z, 19)
                            );
                            
                        // save results
                        Avx2.Store(pbuf, w);
                        pbuf += 4;
                    }
                }
                
                _x = *(pX); _y = *(pY); _z = *(pZ); _w = *(pW);
            }
        }
    }
}