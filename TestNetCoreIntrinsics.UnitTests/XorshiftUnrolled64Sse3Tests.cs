using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XorShift.Intrinsics;

namespace TestApp.UnitTests
{
    [TestClass]
    public class XorshiftUnrolled64Sse3Tests
    {
        [TestMethod]
        public void NextBytesUnrolled64IntrinsicsSse3Unroled()
        {
            //Arrange
            var sample = new XorshiftUnrolled64();
            var samples = new byte[512];
            sample.NextBytes(samples);
            sample.NextBytes(samples);
            var tested = new XorshiftUnrolled64IntrinsicsSse3Unroled();

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

        [TestMethod]
        public void NextBytesUnrolled64IntrinsicsSse3UnroledNoCopyStruct()
        {
            //Arrange
            var sample = new XorshiftUnrolled64();
            var samples = new byte[512];
            sample.NextBytes(samples);

            var tested = new XorshiftUnrolled64IntrinsicsSse3UnroledNoCopyStruct();

            //Act
            var results = new byte[1024];
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

        [TestMethod]
        public void NextBytesUnrolled64IntrinsicsSse3StaticBindingUnroled()
        {
            //Arrange
            var sample = new XorshiftUnrolled64();
            var samples = new byte[512];
            sample.NextBytes(samples);

            var tested = new XorshiftUnrolled64IntrinsicsSse3StaticBindingUnroled();

            //Act
            var results = new byte[1024];
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

