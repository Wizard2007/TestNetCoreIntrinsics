using Microsoft.VisualStudio.TestTools.UnitTesting;
using XorShift.Intrinsics;

namespace TestApp.UnitTests
{
    [TestClass]
    public class XorshiftUnrolled64IntrinsicsSse3UnroledFluentWithOutLocalVarTests
    {
        [TestMethod]
        public void NextBytesUnrolled64IntrinsicsSse3UnroledFluentWithOutLocalVar()
        {
            //Arrange
            var sample = new XorshiftUnrolled64();
            var samples = new byte[512];
            var samples_ = new byte[512];
            sample.NextBytes(samples_);
            sample.NextBytes(samples);
            var tested = new XorshiftUnrolled64IntrinsicsSse3UnroledFluentWithOutLocalVar();

            //Act
            var results = new byte[1024];
            var results_ = new byte[1024];
            tested.NextBytes(results_);
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

