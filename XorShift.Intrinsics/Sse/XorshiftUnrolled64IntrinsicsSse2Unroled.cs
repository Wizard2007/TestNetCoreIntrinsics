using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnrolled64IntrinsicsSse2Unroled : Xorshift
    {
        #region Private fields

        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        private ulong[] xyzwArray = new ulong[8];

        #endregion

        public XorshiftUnrolled64IntrinsicsSse2Unroled()
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

        string VectroToString(Vector128<ulong> vector, int vectorSize) 
        {
            var line = string.Empty;

            for(int i = 0; i < vectorSize; i++)
            {
                 line+=$"{Convert.ToString( vector.AsByte().GetElement(i), toBase : 2), 16}_".Replace(" ", "x");
            }

            return line;
        }
        public override int FillBufferMultipleRequired => 64;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {
            fixed(ulong* pxyzwArray = xyzwArray)
            {
                var pX = pxyzwArray;
                var pY = pxyzwArray+2;
                var pZ = pxyzwArray+4;
                var pW = pxyzwArray+6;
                var x = Sse2.LoadVector128(pX);
                var y = Sse2.LoadVector128(pY);
                var z = Sse2.LoadVector128(pZ);
                var w = Sse2.LoadVector128(pW);

                fixed (byte* pbytes = buf)
                {
                    var pbuf = (ulong*) (pbytes + offset);
                    var pend = (ulong*) (pbytes + offsetEnd);

                    while (pbuf < pend)
                    {
                        //VectroToString(x, 16);
                        // 1 -----------------------------------------------------------------------
                        //ulong tx = x ^ (x << 11);
                        var tx = Sse2.Xor(x, Sse2.ShiftLeftLogical(x, 11));

                        //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                        var ttx =  Sse2.Xor(tx, Sse2.ShiftRightLogical(tx, 8));

                        x = Sse2.Xor(
                                Sse2.Xor(w, ttx),
                                Sse2.ShiftRightLogical(w, 19)
                            );

                        // save results
                        Sse2.Store(pbuf, x);
                        pbuf += 2;

                        // 2 -----------------------------------------------------------------------

                        //ulong ty = y ^ (y << 11);
                        var ty = Sse2.Xor(y, Sse2.ShiftLeftLogical(y, 11));

                        //*(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                        var tty =  Sse2.Xor(ty, Sse2.ShiftRightLogical(ty, 8));

                        y = Sse2.Xor(
                                Sse2.Xor(x, tty),
                                Sse2.ShiftRightLogical(x, 19)
                            );
                            
                        // save results
                        Sse2.Store(pbuf, y);
                        pbuf += 2;

                        // 3 -----------------------------------------------------------------------
                        //ulong tz = z ^ (z << 11);
                        var tz = Sse2.Xor(z, Sse2.ShiftLeftLogical(z, 11));

                        //*(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                        var ttz =  Sse2.Xor(tz, Sse2.ShiftRightLogical(tz, 8));

                        z = Sse2.Xor(
                                Sse2.Xor(y, ttz),
                                Sse2.ShiftRightLogical(y, 19)
                            );
                            
                        // save results
                        Sse2.Store(pbuf, z);
                        pbuf += 2;

                        // 4 -----------------------------------------------------------------------
                        //ulong tw = w ^ (w << 11);
                        var tw = Sse2.Xor(w, Sse2.ShiftLeftLogical(w, 11));

                        //*(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                        var ttw =  Sse2.Xor(tw, Sse2.ShiftRightLogical(tw, 8));

                        w = Sse2.Xor(
                                Sse2.Xor(z, ttw),
                                Sse2.ShiftRightLogical(z, 19)
                            );
                            
                        // save results
                        Sse2.Store(pbuf, w);
                        pbuf += 2;
                    }
                    
                    Sse2.Store(pX, x);
                    Sse2.Store(pY, y);
                    Sse2.Store(pZ, z);
                    Sse2.Store(pW, w);
                }
            }
        }
    }
}