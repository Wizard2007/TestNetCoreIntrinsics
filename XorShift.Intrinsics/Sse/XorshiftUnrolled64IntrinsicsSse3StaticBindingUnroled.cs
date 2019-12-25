using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace XorShift.Intrinsics
{
    unsafe public sealed class XorshiftUnrolled64IntrinsicsSse3StaticBindingUnroled : Xorshift
    {
        #region Private fieldsi

        private new ulong _x = 123456789;
        private new ulong _y = 362436069;
        private new ulong _z = 521288629;
        private new ulong _w = 88675123;

        private ulong[] _xyzwArray = new ulong[8];


            LoadVector128  _loadVector128 ;
            Xor128  _xor128 ;
            ShiftLeftLogical128  _shiftLeftLogical128 ;                                
            ShiftRightLogical128 _shiftRightLogical128 ;

        #endregion

        public XorshiftUnrolled64IntrinsicsSse3StaticBindingUnroled()
        {
            _xyzwArray[0] = _xyzwArray[1] = _x;
            _xyzwArray[2] = _xyzwArray[3] = _y;
            _xyzwArray[4] = _xyzwArray[5] = _z;
            _xyzwArray[6] = _xyzwArray[7] = _w;         

            var methods = typeof(Sse42).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
            //var methods = typeof(Sse3).get();
            _loadVector128 = (LoadVector128) Delegate.CreateDelegate(typeof(LoadVector128), 
                            methods.Where(x => x.Name == "LoadVector128"  && x.GetParameters()?[0].ParameterType ==  typeof(ulong*)
                            ).FirstOrDefault());
            _xor128 = (Xor128) Delegate.CreateDelegate(typeof(Xor128), 
                            methods.Where(x => x.Name == "Xor"  && x.GetParameters()?[0].ParameterType ==  typeof(Vector128<ulong>)
                             && x.GetParameters()?[1].ParameterType ==  typeof(Vector128<ulong>)
                            ).FirstOrDefault());
            _shiftLeftLogical128 = (ShiftLeftLogical128) Delegate.CreateDelegate(typeof(ShiftLeftLogical128), 
                            methods.Where(x => x.Name == "ShiftLeftLogical"  && x.GetParameters()?[0].ParameterType ==  typeof(Vector128<ulong>)
                             && x.GetParameters()?[1].ParameterType ==  typeof(byte)
                            ).FirstOrDefault());                                
            _shiftRightLogical128 = (ShiftRightLogical128) Delegate.CreateDelegate(typeof(ShiftRightLogical128), 
                            methods.Where(x => x.Name == "ShiftRightLogical"  && x.GetParameters()?[0].ParameterType ==  typeof(Vector128<ulong>)
                             && x.GetParameters()?[1].ParameterType ==  typeof(byte)
                            ).FirstOrDefault());               
        }

/*

static void Main()
{
    MethodInfo trimMethod = typeof (string).GetMethod ("Trim", new Type[0]);
    var trim = (StringToString) Delegate.CreateDelegate(typeof (StringToString), trimMethod);

    for (int i = 0; i < 1000000; i++)
        trim ("test");
}
*/

        delegate string StringToString (string s);
        delegate Vector128<ulong> LoadVector128 (ulong* address);
        delegate Vector128<ulong> Xor128(Vector128<ulong> left, Vector128<ulong> right);
        delegate Vector128<ulong> ShiftLeftLogical128(Vector128<ulong> left, byte count);
        delegate Vector128<ulong> ShiftRightLogical128(Vector128<ulong> left, byte count);
        
        delegate Vector128<ulong> Store128(ulong* address,Vector128<ulong> left);
        public override int FillBufferMultipleRequired => 64;

        protected unsafe override void FillBuffer(byte[] buf, int offset, int offsetEnd)
        {

/*
            Store128 store128 = (Store128) Delegate.CreateDelegate(typeof(Store128), 
                            methods.Where(x => x.Name == "Store"  && x.GetParameters()?[0].ParameterType ==  typeof(ulong*)
                             && x.GetParameters()?[1].ParameterType ==  typeof(Vector128<ulong>)
                            ).FirstOrDefault());
            */
            /*foreach(var method in methods)
            {                
                if(method.Name == "LoadVector128")
                {
                    Console.WriteLine(method.Name);
                    var mparams = method.GetParameters();
                    foreach(var param in mparams)
                    {
                        Console.WriteLine( $"    {param.ParameterType.ToString()} {param.Name}");                       
                    }
                    if(methods.Where(x => x.Name == "LoadVector128"  && x.GetParameters()?[0].ParameterType ==  typeof(System.UInt64*)).FirstOrDefault()  == null)
                    {
                        Console.WriteLine("    method null");
                    }
                    else
                    {
                        
                        try{
                            Console.WriteLine(methods.Where(x => x.Name == "LoadVector128"  && x.GetParameters()?[0].ParameterType ==  typeof(System.UInt64*)).FirstOrDefault().Name);
                            loadVector128 = (LoadVector128) Delegate.CreateDelegate(typeof(LoadVector128), 
                            methods.Where(x => x.Name == "LoadVector128"  && x.GetParameters()?[0].ParameterType ==  typeof(System.UInt64*)
                            ).FirstOrDefault());
                            if(loadVector128 == null)
                            {

                                Console.WriteLine("   loadVector_128 null");
                            }
                            else
                            {
                                Console.WriteLine("   loadVector_128 good");
                                break;
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            };
*/

    //MethodInfo m = typeof (string).GetMethod ("LoadVector128", new Type[1] {typeof(System.UInt64*)});
    //var t = (LoadVector128) Delegate.CreateDelegate(typeof (LoadVector128), m);

//        public static Vector128<double> AddSubtract(Vector128<double> left, Vector128<double> right);

            LoadVector128  loadVector128 =  _loadVector128;
            Xor128  xor128 = _xor128;
            ShiftLeftLogical128  shiftLeftLogical128 = _shiftLeftLogical128;                                
            ShiftRightLogical128 shiftRightLogical128 = _shiftRightLogical128;


            fixed(ulong* pxyzwArray = _xyzwArray)
            {
                var pX = pxyzwArray;
                var pY = pxyzwArray+2;
                var pZ = pxyzwArray+4;
                var pW = pxyzwArray+6;
                var x = loadVector128(pX);
                var y = loadVector128(pY);
                var z = loadVector128(pZ);
                var w = loadVector128(pW);
                
                fixed (byte* pbytes = buf)
                {
                    var pbuf = (ulong*) (pbytes + offset);
                    var pend = (ulong*) (pbytes + offsetEnd);

                    while (pbuf < pend)
                    {
                        // 1 -----------------------------------------------------------------------
                        //ulong tx = x ^ (x << 11);
                        var tx = xor128(x, shiftLeftLogical128(x, 11));

                        //*(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                        var ttx =  xor128(tx, shiftRightLogical128(tx, 8));

                        x = xor128(
                                xor128(w, ttx),
                                shiftRightLogical128(w, 19)
                            );

                        // save results
                        
                        Sse3.Store(pbuf, x);
                        pbuf += 2;

                        // 2 -----------------------------------------------------------------------
                        
                        //ulong ty = y ^ (y << 11);
                        var ty = xor128(y, shiftLeftLogical128(y, 11));

                        //*(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                        var tty =  xor128(ty, shiftRightLogical128(ty, 8));

                        y = xor128(
                                xor128(x, tty),
                                shiftRightLogical128(x, 19)
                            );
                            
                        // save results

                        Sse3.Store(pbuf, y);
                        pbuf += 2;
                        
                        // 3 -----------------------------------------------------------------------
                        //ulong tz = z ^ (z << 11);
                        var tz = xor128(z, shiftLeftLogical128(z, 11));

                        //*(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                        var ttz =  xor128(tz, shiftRightLogical128(tz, 8));

                        z = xor128(
                                xor128(y, ttz),
                                shiftRightLogical128(y, 19)
                            );
                            
                        // save results
                        
                        Sse3.Store(pbuf, z);
                        pbuf += 2;
                        
                        // 4 -----------------------------------------------------------------------
                        //ulong tw = w ^ (w << 11);
                        var tw = xor128(w, shiftLeftLogical128(w, 11));

                        //*(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
                        var ttw =  xor128(tw, shiftRightLogical128(tw, 8));

                        w = xor128(
                                xor128(z, ttw),
                                shiftRightLogical128(z, 19)
                            );
                            
                        // save results
                        
                        Sse3.Store(pbuf, w);
                        pbuf += 2;
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