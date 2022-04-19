using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackYourExpenseApi.Filters
{
    public class TransactionsResultFilterAttribute: ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }
            //if (typeof(System.Collections.IEnumerable).IsAssignableFrom(resultFromAction.GetType())) { }
            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            resultFromAction.Value = mapper.Map<IEnumerable<Models.TransactionDto>>(resultFromAction.Value);
            await next();
        }
    }
}
