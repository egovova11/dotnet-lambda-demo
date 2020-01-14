using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DotnetLambda30WithEf.Tests.Integration
{
    [TestFixture()]
    public class FullTest
    {
        public async Task T()
        {
            var task = Function.Main(Array.Empty<string>());


        }
    }
}
