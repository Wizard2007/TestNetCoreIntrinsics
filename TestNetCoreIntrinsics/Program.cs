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
            //---------------------------------------
            var n = 32;
            var doubleArray = stackalloc double[n]; 
            
            for(var i = 0; i < n ; i++)
            {
                doubleArray[i] = i;
            }

            var current = doubleArray;
            for(var i = 0; i < n; i+=4)
            {
                var v = Avx.LoadAlignedVector256(current);
                Avx.StoreAligned(current, v);
                current+=4;
            }

            //---------------------
            var broadcastScalar = stackalloc double[8] {1d, 2, 3, 4,5,6,7,8};            
            var broadcastScalarToVector = Avx.BroadcastScalarToVector256(broadcastScalar);

            //---------------------
            Vector256<double> HorizontalAdd1 = Vector256.Create(1d,2d,3d,4d);
            Vector256<double> HorizontalAdd2 = Vector256.Create(10d,20d,30d,40d);

            var h3 = Avx.HorizontalAdd(HorizontalAdd1, HorizontalAdd2);

            //---------------------------
            Vector256<ulong> firstV256L = Vector256.Create(1ul,2ul,3ul,4ul);
            Vector256<ulong> secondV256L = Vector256.Create(10ul,20ul, 30ul, 40ul);

            firstV256L = Avx.Add(firstV256L.AsDouble(), secondV256L.AsDouble()).AsUInt64();

            //------------------------------------
            Vector256<double> firstV256 = Vector256.Create(1d,2d,3d,4d);
            Vector256<double> secondV256 = Vector256.Create(10d,20d,30d,40d);

            firstV256 = Avx2.Add(firstV256, secondV256);
            
            var sum = Avx2.Add(firstV256, secondV256);
            var x = sum.GetElement(0);
            var y = sum.GetElement(1);
            var z = sum.GetElement(2);
            var w = sum.GetElement(3);


            //-----------------------------------------
            CheckSuppertedIntrinsicsFeature();

            Console.WriteLine(string.Empty);
            //Vector128Bit();
            Console.WriteLine(string.Empty);
            //Vector256Bit();
            Console.WriteLine(string.Empty);
            Vector256BitEditSource();
            Console.WriteLine("-- Completed ... ");
        }

        private static unsafe void CheckSuppertedIntrinsicsFeature()
        {
            Console.WriteLine(Bmi1.IsSupported ? "Bmi1 supported" : "Bmi1 : not supported");
            Console.WriteLine(Aes.IsSupported ? "Aes supported" : "Aes : not supported");
            Console.WriteLine(Avx.IsSupported ? "Avx supported" : "Avx : not supported");
            Console.WriteLine(Avx2.IsSupported ? "Avx2 supported" : "Avx2 : not supported");
            Console.WriteLine(Sse.IsSupported ? "Sse supported" : "Sse : not supported");
            Console.WriteLine(Sse2.IsSupported ? "Sse2 supported" : "Sse2 : not supported");
            Console.WriteLine(Sse3.IsSupported ? "Sse3 supported" : "Sse3 : not supported");
            Console.WriteLine(Sse41.IsSupported ? "Sse41 supported" : "Sse41 : not supported");
            Console.WriteLine(Sse42.IsSupported ? "Sse42 supported" : "Sse42 : not supported");
        }

        unsafe static void Vector256BitEditSource()
        {
            Console.WriteLine("-- Edit 256 Bit vector --");

            var vectroSize256 = 256 / 8 / sizeof(uint);
            var utemp256 = stackalloc uint[vectroSize256];
            for (int j = 0; j < vectroSize256; j++) {
                utemp256[j] = 1;
            }


            var v1 = Avx2.LoadVector256(utemp256);

            Console.WriteLine("-- Original vector --");
            Console.WriteLine(VectroTo32String(v1, vectroSize256));
            utemp256[0] = uint.MaxValue;
            Console.WriteLine("-- After change array --");
            Console.WriteLine(VectroTo32String(v1, vectroSize256));
            var v2 = Avx2.LoadVector256(utemp256);
            Console.WriteLine("-- After reload vetor --");
            Console.WriteLine(VectroTo32String(v2, vectroSize256));

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
            Console.WriteLine(VectroTo32String(v1, vectroSize256));

            var v2 = Avx2.ShiftRightLogical(v1, 9);

            Console.WriteLine("-- ShiftRightLogical 9 bit --");
            Console.WriteLine(VectroTo32String(v2, vectroSize256));

            var v3 = Avx2.LoadVector256(temp256);
            var v4 = Avx2.ShiftRightArithmetic(v3, 9);

            Console.WriteLine("-- ShiftRightArithmetic 9 bit --");
            Console.WriteLine(VectroTo32String(v4, vectroSize256));
        }

        unsafe static void Vector128Bit()
        {
            Console.WriteLine("-- 128 Bit --");
            var vectorSize128 = 128 / 8 / sizeof(short);
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

        unsafe static string VectroTo32String<T>(Vector256<T> vector, int vectorSize) where T : struct
        {
            var line = string.Empty;
            var v = vector.AsUInt32();

            for(int i = 0; i < vectorSize; i++)
            {
                 line+=$"{Convert.ToString( v.GetElement(i), toBase : 2), 32}_".Replace(" ", "x");
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
