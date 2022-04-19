using System;
using AutoMapper;


namespace TrackYourExpenseApi.Profiles
{
    public class TransactionsProfile : Profile
    {
        public TransactionsProfile()
        {
            CreateMap<Entities.Transaction, Models.TransactionDto>();
        }
    }
}
