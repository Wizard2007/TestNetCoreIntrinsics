using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XorShift.Intrinsics;

namespace TestApp.UnitTests
{
    [TestClass]
    public class XorshiftUnrolled64Avx2Tests
    {
        [TestMethod]
        public void GenerateNextBytes()
        {
            if(System.Runtime.Intrinsics.X86.Avx2.IsSupported)
            {
                //Arrange
                var sample = new XorshiftUnrolled64();
                var samples = new byte[256];
                sample.NextBytes(samples);
                sample.NextBytes(samples);
                var tested = new XorshiftUnrolled64IntrinsicsAvx2Unroled();

                //Act
                var results = new byte[1024];
                tested.NextBytes(results);
                tested.NextBytes(results);
                //Assert
                for(int i = 0; i< samples.Length; i+=8)
                {
                    
                    for(int k = 0; k< 2;k++)
                    {
                        for(int j = 0; j<8; j++)
                        {
                            Assert.AreEqual(samples[i+j], results[i*2+k*8+j]);
                        }
                    }
                }
            }
        }
    }
}

