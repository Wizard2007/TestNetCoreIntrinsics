using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using XorShift.Intrinsics;

namespace TestNetCoreIntrinsics
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            var arrayByte = new byte[1024*1024];
            var t1 = new XorshiftUnrolled64IntrinsicsSse2Unroled();
            t1.NextBytes(arrayByte);
            var t = new XorshiftUnrolled64();
            arrayByte = new byte[1024*1024];          
            t.NextBytes(arrayByte);

            Console.WriteLine( Avx2.IsSupported ? "Avx2 supported" : "Avx2 : not supported" );
            Console.WriteLine( Sse.IsSupported ? "Sse supported" : "Sse : not supported");
            Console.WriteLine( Sse2.IsSupported ? "Sse2 supported" : "Sse2 : not supported");
            Console.WriteLine( Sse3.IsSupported ? "Sse3 supported" : "Sse3 : not supported");
            Console.WriteLine( Sse41.IsSupported ? "Sse41 supported" : "Sse41 : not supported");
            Console.WriteLine( Sse42.IsSupported ? "Sse42 supported" : "Sse42 : not supported");

            Console.WriteLine(string.Empty);
            //Vector128Bit();
            Console.WriteLine(string.Empty);
            //Vector256Bit();
            Console.WriteLine(string.Empty);
            Vector256BitEditSource();
            Console.WriteLine("-- Completed ... ");
        }

        unsafe static void Vector256BitEditSource()
        {
            Console.WriteLine("-- Edit 256 Bit vector --");

            var vectroSize256 = 256 / 8 / 4;
            var utemp256 = stackalloc uint[16];
            for (int j = 0; j < vectroSize256; j++) {
                utemp256[j] = 1;
            }


            var v1 = Avx2.LoadVector256(utemp256);

            Console.WriteLine("-- Original vector --");
            Console.WriteLine(VectroToString(v1, vectroSize256));
            utemp256[0] = uint.MaxValue;
            Console.WriteLine("-- After change array --");
            Console.WriteLine(VectroToString(v1, vectroSize256));
            var v2 = Avx2.LoadVector256(utemp256);
            Console.WriteLine("-- After reload vetor --");
            Console.WriteLine(VectroToString(v2, vectroSize256));

            //Avx2.Shuffle
        }

        unsafe static void Vector256Bit()
        {
            Console.WriteLine("-- 256 Bit --");

            var vectroSize256 = 256 / 8 / 4;
            var utemp256 = stackalloc uint[16];
            for (int j = 0; j < vectroSize256; j++) {
                utemp256[j] = uint.MaxValue;
            }

            var temp256 = stackalloc int[16];
            for (int j = 0; j < vectroSize256; j++) {
                temp256[j] = int.MinValue;
            }

            var v1 = Avx2.LoadVector256(utemp256);

            Console.WriteLine("-- Original vector --");
            Console.WriteLine(VectroToString(v1, vectroSize256));

            var v2 = Avx2.ShiftRightLogical(v1, 9);

            Console.WriteLine("-- ShiftRightLogical 9 bit --");
            Console.WriteLine(VectroToString(v2, vectroSize256));

            var v3 = Avx2.LoadVector256(temp256);
            var v4 = Avx2.ShiftRightArithmetic(v3, 9);

            Console.WriteLine("-- ShiftRightArithmetic 9 bit --");
            Console.WriteLine(VectroToString(v4, vectroSize256));
        }

        unsafe static void Vector128Bit()
        {
            Console.WriteLine("-- 128 Bit --");
            var vectorSize128 = 128 / 8 / 4;
            var temp128 = stackalloc short[vectorSize128];

            for (int j = 0; j < vectorSize128; j++) {
                temp128[j] = short.MaxValue;
            }

            var v = Sse42.LoadVector128(temp128);

            Console.WriteLine("-- Original vector --");          
            Console.WriteLine(VectroToString(v, vectorSize128));
            Console.WriteLine("-- Shift ShiftRightArithmetic : 2 bit");
            Console.WriteLine(VectroToString(Sse42.ShiftRightArithmetic(v, 2), vectorSize128));
            Console.WriteLine("-- Shift ShiftRightLogical : 2 bit");
            Console.WriteLine(VectroToString(Sse42.ShiftRightLogical(v, 2), vectorSize128));

            Console.WriteLine("-- Shift ShiftLeftLogical : 2 bit");            
            Console.WriteLine(VectroToString(Sse42.ShiftLeftLogical(v, 2), vectorSize128));

            Console.WriteLine("-- ShiftLeftLogical128BitLane --");
            Console.WriteLine(VectroToString(v, vectorSize128));
            
            Console.WriteLine(VectroToString(Sse42.ShiftLeftLogical128BitLane(v, 1), vectorSize128));
            Console.WriteLine(VectroToString(Sse42.ShiftLeftLogical128BitLane(v, 2), vectorSize128));
            Console.WriteLine(VectroToString(Sse42.ShiftLeftLogical128BitLane(v, 3), vectorSize128));
            Console.WriteLine(VectroToString(Sse42.ShiftLeftLogical128BitLane(v, 4), vectorSize128));
            Console.WriteLine(VectroToString(Sse42.ShiftLeftLogical128BitLane(v, 5), vectorSize128));
        }
        unsafe static string VectroToString(Vector128<short> vector, int vectorSize) 
        {
            var line = string.Empty;

            for(int i = 0; i < vectorSize; i++)
            {
                 line+=$"{Convert.ToString( vector.GetElement(i), toBase : 2), 16}_".Replace(" ", "x");
            }

            return line;
        }

        unsafe static string VectroToString(Vector256<uint> vector, int vectorSize) 
        {
            var line = string.Empty;

            for(int i = 0; i < vectorSize; i++)
            {
                 line+=$"{Convert.ToString( vector.GetElement(i), toBase : 2), 32}_".Replace(" ", "x");
            }

            return line;
        }

        unsafe static string VectroToString(Vector256<int> vector, int vectorSize) 
        {
            var line = string.Empty;

            for(int i = 0; i < vectorSize; i++)
            {
                 line+=$"{Convert.ToString( vector.GetElement(i), toBase : 2), 32}_".Replace(" ", "x");
            }

            return line;
        }
    }
}
