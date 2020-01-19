using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using NUnit.Framework;

namespace DotnetLambda30WithEf.Tests.Integration
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task Given_ExistingClientName_ReturnsClientData()
        {
            // arrange
            const string query = "Adams";
            const string expected = "Customer search result: 491 Frances Adams";

            // act
            var function = new Function();
            var context = new TestLambdaContext();
            var actual = await function.FunctionHandler(query, context);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
