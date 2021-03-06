using System;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DotnetLambda21WithEf.Tests
{
    [TestFixture]
    public class IntegrationTests : IntegrationFixtureBase
    {
        [Test]
        public async Task Given_ExistingClientName_ReturnsClientData()
        {
            // arrange
            const string query = "Adams";
            const string expected = "Customer search result: 491 Frances Adams";
            var configuration = new Lazy<IConfiguration>(Configuration);

            // act
            var function = new Function(configuration);
            var context = new TestLambdaContext();
            var actual = await function.FunctionHandler(query, context);

            // assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
