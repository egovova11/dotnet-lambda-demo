using Amazon.Lambda.TestUtilities;
using NUnit.Framework;

namespace DotnetLambda21.Tests
{
    [TestFixture]
    
    public class FunctionTest
    {
        [Test]
        public void TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = function.FunctionHandler("hello world", context);

            Assert.That(upperCase, Is.EqualTo("HELLO WORLD"));
        }
    }
}
