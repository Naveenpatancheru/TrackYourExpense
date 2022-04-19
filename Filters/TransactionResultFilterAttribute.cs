using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TrackYourExpenseApi.Filters
{
    public class TransactionResultFilterAttribute :ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if(resultFromAction?.Value == null 
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >=300)
            {
                await next();
                return;
            }
            //if (typeof(System.Collections.IEnumerable).IsAssignableFrom(resultFromAction.GetType())) { }
            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            try
            {
                resultFromAction.Value = mapper.Map<Models.TransactionDto>(resultFromAction.Value);
            }
            catch (Exception ex)
            {

            }
            await next();
        }
    }
}
