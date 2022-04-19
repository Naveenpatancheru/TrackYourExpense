using AutoMapper;

namespace TrackYourExpenseApi.Profiles
{
    public class ExpensesProfile : Profile
    {
        public ExpensesProfile()
        {
            CreateMap<Entities.Expense, Models.ExpenseDto>()
                .ForMember(
                 dest => dest.TransactionType, opt=> opt.MapFrom(src => 
                       $"{src.Transaction.Text}{src.Transaction.Description}"));
            //CreateMap<Entities.Transaction, Models.TransactionDto>();
        }

    }
}
