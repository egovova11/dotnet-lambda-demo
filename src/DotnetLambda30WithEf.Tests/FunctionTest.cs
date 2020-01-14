using Amazon.Lambda.TestUtilities;
using NUnit.Framework;

namespace DotnetLambda30WithEf.Tests
{
    [TestFixture]
    public class FunctionTest
    {
        [Test]
        [Ignore("test is broken")]
        public void TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var context = new TestLambdaContext();
            var function = new Function();
            var upperCase = function.FunctionHandler("hello world", context);

            Assert.That(upperCase, Is.EqualTo("HELLO WORLD"));
        }
    }
}
