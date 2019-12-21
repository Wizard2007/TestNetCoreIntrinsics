using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public class XorshiftUnrolled64IntrinsicsSse41Unroled : Xorshift
    {
        #region Private fields

        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        private ulong[] xyzwArray = new ulong[8];

        #endregion

        public XorshiftUnrolled64IntrinsicsSse41Unroled()
        {
            xyzwArray[0] = xyzwArray[1] = _x;
            xyzwArray[2] = xyzwArray[3] = _y;
            xyzwArray[4] = xyzwArray[5] = _z;
            xyzwArray[6] = xyzwArray[7] = _w;            
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

        public override int FillBufferMultipleRequired => 64;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed(ulong* pxyzwArray = xyzwArray)
            {
                var pX = pxyzwArray;
                var pY = pxyzwArray+2;
                var pZ = pxyzwArray+4;
                var pW = pxyzwArray+6;
                var x = Sse41.LoadVector128(pX);
                var y = Sse41.LoadVector128(pY);
                var z = Sse41.LoadVector128(pZ);
                var w = Sse41.LoadVector128(pW);

                fixed (byte* pbytes = buf)
                {
                    var pbuf = (ulong*) (pbytes + offset);
                    var pend = (ulong*) (pbytes + offsetEnd);

                    while (pbuf < pend)
                    {
                        //VectroToString(x, 16);
                        // 1 -----------------------------------------------------------------------
                        //ulong tx = x ^ (x << 11);
                        var tx = Sse41.Xor(x, Sse41.ShiftLeftLogical(x, 11));

                        //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                        var ttx =  Sse41.Xor(tx, Sse41.ShiftRightLogical(tx, 8));

                        x = Sse41.Xor(
                                Sse41.Xor(w, ttx),
                                Sse41.ShiftRightLogical(w, 19)
                            );

                        // save results
                        Sse41.Store(pbuf, x);
                        pbuf += 2;

                        // 2 -----------------------------------------------------------------------

                        //ulong ty = y ^ (y << 11);
                        var ty = Sse41.Xor(y, Sse41.ShiftLeftLogical(y, 11));

                        //*(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                        var tty =  Sse41.Xor(ty, Sse41.ShiftRightLogical(ty, 8));

                        y = Sse41.Xor(
                                Sse41.Xor(x, tty),
                                Sse41.ShiftRightLogical(x, 19)
                            );
                            
                        // save results
                        Sse41.Store(pbuf, y);
                        pbuf += 2;

                        // 3 -----------------------------------------------------------------------
                        //ulong tz = z ^ (z << 11);
                        var tz = Sse41.Xor(z, Sse41.ShiftLeftLogical(z, 11));

                        //*(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                        var ttz =  Sse41.Xor(tz, Sse41.ShiftRightLogical(tz, 8));

                        z = Sse41.Xor(
                                Sse41.Xor(y, ttz),
                                Sse41.ShiftRightLogical(y, 19)
                            );
                            
                        // save results
                        Sse41.Store(pbuf, z);
                        pbuf += 2;

                        // 4 -----------------------------------------------------------------------
                        //ulong tw = w ^ (w << 11);
                        var tw = Sse41.Xor(w, Sse41.ShiftLeftLogical(w, 11));

                        //*(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                        var ttw =  Sse41.Xor(tw, Sse41.ShiftRightLogical(tw, 8));

                        w = Sse41.Xor(
                                Sse41.Xor(z, ttw),
                                Sse41.ShiftRightLogical(z, 19)
                            );
                            
                        // save results
                        Sse41.Store(pbuf, w);
                        pbuf += 2;
                    }
                    
                    Sse41.Store(pX, x);
                    Sse41.Store(pY, y);
                    Sse41.Store(pZ, z);
                    Sse41.Store(pW, w);
                }
            }
        }
    }
}