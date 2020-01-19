using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Mvc;

namespace DotnetLambda30WithEf.Host.Controllers
{
    [ApiController]
    [Route("/")]
    public class LambdaController : AbstractLambdaController<string, Task<string>>
    {
        public LambdaController(Func<string, ILambdaContext, Task<string>> function, ILambdaContext context) : base(function, context)
        {
        }

        [HttpGet("invoke")]
        public Task<string> Invoke([FromQuery(Name = "input")] string input)
        {
            return InvokeImpl(input);
        }
    }
}
