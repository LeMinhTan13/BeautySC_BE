using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.FeedbackModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class FeedbackProfile :Profile
    {
        public FeedbackProfile() 
        {
            CreateMap<Feedback, FeedbackCreateRequestModel>();
            CreateMap<Feedback, FeedbackViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
            CreateMap<FeedbackCreateRequestModel, Feedback>();
            CreateMap<Customer, CustomerFeedbackViewModel>()
                 .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
        }
    }
}
