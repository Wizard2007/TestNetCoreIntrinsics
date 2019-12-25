using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics.Store.Sse
{
    public class Sse3UnroledStoreStackAlloc : Xorshift
    {
        #region Private fields

        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        private ulong[] xyzwArray = new ulong[8];

        #endregion

        public Sse3UnroledStoreStackAlloc()
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
            var tmp = stackalloc ulong[8]; 

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

                var ptmp = tmp;
                var ptmp2 = tmp+2;
                var ptmp4 = tmp+4;
                var ptmp6 = tmp+6;
                
                fixed (byte* pbytes = buf)
                {
                    var pbuf = (ulong*) (pbytes + offset);
                    var pend = (ulong*) (pbytes + offsetEnd);

                    while (pbuf < pend)
                    {
                        // 1 -----------------------------------------------------------------------

                        Sse3.Store(ptmp, x);

                        // 2 -----------------------------------------------------------------------

                        Sse3.Store(ptmp2, y);

                        // 3 -----------------------------------------------------------------------

                        Sse3.Store(ptmp4, z);

                        // 4 -----------------------------------------------------------------------

                        Sse3.Store(ptmp6, w);

                        Buffer.MemoryCopy(tmp, pbuf, 64, 64);
                        pbuf += 8;
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