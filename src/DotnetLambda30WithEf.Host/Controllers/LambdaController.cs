using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Mvc;

namespace DotnetLambda30WithEf.Host.Controllers
{
    [ApiController]
    [Route("/")]
    public class LambdaController : LambdaController<string, Task<string>>
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

    public abstract class LambdaController<TInput, TOutput>: ControllerBase
    {
        private readonly Func<TInput, ILambdaContext, TOutput> _function;
        private readonly ILambdaContext _context;

        protected LambdaController(
            Func<TInput, ILambdaContext, TOutput> function, 
            ILambdaContext context)
        {
            _function = function;
            _context = context;
        }

        public TOutput InvokeImpl(TInput input)
        {
            return _function.Invoke(input, _context);
        }
    }
}
