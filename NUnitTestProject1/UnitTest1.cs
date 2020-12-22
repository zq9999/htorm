using Ht.AuthenticationCenter.Utility.RSA;
using NUnit.Framework;
using System.IO;

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
            //int i = 9;
            //object obj = i;
            //var aa= obj.GetType();
            //Assert.Pass();

            var dir=Directory.GetCurrentDirectory();
            var mutipleK=RSAHelper.GenerateAndSaveKey(dir);
        }
    }
}