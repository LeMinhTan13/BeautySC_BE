using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.SkinTypeQuestionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class SkinTypeQuestionProfile:Profile
    {
        public SkinTypeQuestionProfile() 
        {
            CreateMap<SkinTypeQuestionModel, SkinTypeQuestion>()
                .ForMember(dest => dest.SkinTypeQuestionId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));
            /*
                    CreateMap<SkinTypeAnswerModel, SkinTypeAnswer>()
                        .ForMember(dest => dest.SkinTypeAnswerId, opt => opt.Ignore()) // Bỏ qua ID (AutoIncrement)
                        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                        .ForMember(dest => dest.SkinTypeId, opt => opt.MapFrom(src => src.SkinTypeId));*/
        }

    }
}

