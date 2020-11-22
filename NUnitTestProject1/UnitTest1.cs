using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            int i = 9;
            object obj = i;
            var aa= obj.GetType();
            Assert.Pass();
        }
    }
}