using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    public sealed class XorshiftUnrolled64IntrinsicsSse3UnroledFluent : Xorshift
    {
        #region Private fields

        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        private ulong[] xyzwArray = new ulong[8];

        #endregion

        public XorshiftUnrolled64IntrinsicsSse3UnroledFluent()
        {
            xyzwArray[0] = xyzwArray[1] = _x;
            xyzwArray[2] = xyzwArray[3] = _y;
            xyzwArray[4] = xyzwArray[5] = _z;
            xyzwArray[6] = xyzwArray[7] = _w;            
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
                var x = Sse3.LoadVector128(pX);
                var y = Sse3.LoadVector128(pY);
                var z = Sse3.LoadVector128(pZ);
                var w = Sse3.LoadVector128(pW);
                
                fixed (byte* pbytes = buf)
                {
                    var pbuf = (ulong*) (pbytes + offset);
                    var pend = (ulong*) (pbytes + offsetEnd);

                    while (pbuf < pend)
                    {
                        // 1 -----------------------------------------------------------------------
                        //ulong tx = x ^ (x << 11);
                        var tx = Sse3.Xor(x, Sse3.ShiftLeftLogical(x, 11));

                        //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8))

                        x = Sse3.Xor(
                                Sse3.Xor(w, Sse3.Xor(tx, Sse3.ShiftRightLogical(tx, 8))),
                                Sse3.ShiftRightLogical(w, 19)
                            );

                        // save results
                        
                        Sse3.Store(pbuf, x);
                        pbuf = (ulong*)((byte*)pbuf+2*8);

                        // 2 -----------------------------------------------------------------------
                        
                        //ulong ty = y ^ (y << 11);
                        var ty = Sse3.Xor(y, Sse3.ShiftLeftLogical(y, 11));

                        //*(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));

                        y = Sse3.Xor(
                                Sse3.Xor(x, Sse3.Xor(ty, Sse3.ShiftRightLogical(ty, 8))),
                                Sse3.ShiftRightLogical(x, 19)
                            );
                            
                        // save results
                        
                        Sse3.Store(pbuf, y);
                        pbuf = (ulong*)((byte*)pbuf+2*8);
                        
                        // 3 -----------------------------------------------------------------------
                        //ulong tz = z ^ (z << 11);
                        var tz = Sse3.Xor(z, Sse3.ShiftLeftLogical(z, 11));

                        //*(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));

                        z = Sse3.Xor(
                                Sse3.Xor(y, Sse3.Xor(tz, Sse3.ShiftRightLogical(tz, 8))),
                                Sse3.ShiftRightLogical(y, 19)
                            );
                            
                        // save results
                        
                        Sse3.Store(pbuf, z);
                        pbuf = (ulong*)((byte*)pbuf+2*8);
                        
                        // 4 -----------------------------------------------------------------------
                        //ulong tw = w ^ (w << 11);
                        var tw = Sse3.Xor(w, Sse3.ShiftLeftLogical(w, 11));

                        //*(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));

                        w = Sse3.Xor(
                                Sse3.Xor(z, Sse3.Xor(tw, Sse3.ShiftRightLogical(tw, 8))),
                                Sse3.ShiftRightLogical(z, 19)
                            );
                            
                        // save results
                        
                        Sse3.Store(pbuf, w);
                        pbuf = (ulong*)((byte*)pbuf+2*8);
                    }
                    
                    Sse3.Store(pX, x);
                    Sse3.Store(pY, y);
                    Sse3.Store(pZ, z);
                    Sse3.Store(pW, w);
                }
            }
        }
    }
}