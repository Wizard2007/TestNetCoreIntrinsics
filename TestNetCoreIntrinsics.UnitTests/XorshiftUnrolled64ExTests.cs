using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp.UnitTests
{
    [TestClass]
    public class XorshiftUnrolled64ExTests
    {
        [TestMethod]
        public void GenerateNextBytes()
        {
            //Arrange
            var sample = new XorshiftUnrolled64();
            var samples = new byte[1024];
            sample.NextBytes(samples);

            var tested = new XorshiftUnrolled64Ex();

            //Act
            var results = new byte[1024];
            tested.NextBytes(results);
            //Assert
            CollectionAssert.AreEqual(samples, results);
        }
    }
}
