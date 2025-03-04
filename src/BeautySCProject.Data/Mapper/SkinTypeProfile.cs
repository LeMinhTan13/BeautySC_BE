using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.SkinTypeModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    public class SkinTypeProfile:Profile
    {
        public SkinTypeProfile()
        {
            CreateMap<SkinType, SkinTypeViewModel>();
            CreateMap<SkinType, SkinTypeUpdateRequestModel>();
        }
        
    }
}
