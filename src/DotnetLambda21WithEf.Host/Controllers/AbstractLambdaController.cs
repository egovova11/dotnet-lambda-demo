using System;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Mvc;

namespace DotnetLambda21WithEf.Host.Controllers
{
    public abstract class AbstractLambdaController<TInput, TOutput>: ControllerBase
    {
        private readonly Func<TInput, ILambdaContext, TOutput> _function;
        private readonly ILambdaContext _context;

        protected AbstractLambdaController(
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