using Microsoft.VisualStudio.TestTools.UnitTesting;
using XorShift.Intrinsics;

namespace TestApp.UnitTests
{
    //[TestClass]
    public class XorshiftUnrolled64IntrinsicsTests
    {
        [TestMethod]
        public void GenerateNextBytes()
        {
            if(System.Runtime.Intrinsics.X86.Avx2.IsSupported)
            {
                //Arrange
                var sample = new XorshiftUnrolled64();
                var samples = new byte[1024];
                sample.NextBytes(samples);
                sample.NextBytes(samples);
                var tested = new XorshiftUnrolled64Intrinsics();

                //Act
                var results = new byte[1024];
                tested.NextBytes(results);
                tested.NextBytes(results);
                //Assert
                CollectionAssert.AreEqual(samples, results);
            }
        }
    }
}

