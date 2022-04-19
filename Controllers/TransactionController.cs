using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackYourExpenseApi.Filters;
using TrackYourExpenseApi.Models;
using TrackYourExpenseApi.Services;

namespace TrackYourExpenseApi.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class TransactionController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private IMapper _mapper;

        public TransactionController(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [TransactionResultFilter]
        public async Task<IActionResult> GetTransactions()
        {
            var transactionEntities = await _expenseRepository.GetTransactionsAsync();
            //   return Ok(_mapper.Map<IEnumerable<TransactionDto>>(transactionEntities));
            return Ok(transactionEntities);

        }

        [HttpGet("{transactionId}")]
        [TransactionResultFilter]
        public async Task<IActionResult> GetTransactions(int transactionId)
        {
            var transactionFromRepo = await _expenseRepository.GetTransactionAsync(Guid.NewGuid());
            if(transactionFromRepo == null)
            {
                return NotFound();
            } 
            //return Ok(_mapper.Map<TransactionDto> (transactionFromRepo)); we used filter attribute instead of mapping explicitly
            return Ok(transactionFromRepo);

        }
    }
}
