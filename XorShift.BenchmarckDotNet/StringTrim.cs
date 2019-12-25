using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace XorShift.BenchmarckDotNet
{
    public class StringTest
    {
        delegate string StringToString (string s);
        StringToString trim;
        public StringTest()
        {
            MethodInfo trimMethod = typeof (string).GetMethod ("Trim", new Type[0]);
            trim = (StringToString) Delegate.CreateDelegate(typeof (StringToString), trimMethod);

        }

        [Benchmark]
        public void EmptyLoop()
        {
           
            for (int i = 0; i < 10000000; i++)
            {
                
            }
        }

        [Benchmark]
        public void StringTrimBinded()
        {
           
            for (int i = 0; i < 10000000; i++)
                trim ("test");
        }

        [Benchmark]
        public void StringTrim()
        {
            //MethodInfo trimMethod = typeof (string).GetMethod ("Trim", new Type[0]);
            //var trim = (StringToString) Delegate.CreateDelegate(typeof (StringToString), trimMethod);

            for (int i = 0; i < 10000000; i++)
                "test".Trim();
        }
    }
}