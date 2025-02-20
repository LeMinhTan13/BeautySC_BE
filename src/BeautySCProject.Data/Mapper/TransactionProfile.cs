using BeautySCProject.Data.Entities;
using BeautySCProject.Data.ViewModels;
using AutoMapper;

namespace BeautySCProject.Data.Mapper
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.Customer.FullName));
        }
    }
}
