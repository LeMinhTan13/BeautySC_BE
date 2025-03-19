using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Models.BlogModel;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Mapper
{
    class BlogProfile : Profile
    {
        public BlogProfile()
        {            
            CreateMap<Blog, BlogViewModel>();
            CreateMap<Blog, BlogDetailViewModel>();

            CreateMap<BlogCreateRequest, Blog>();
            CreateMap<BlogDetailCreateRequest, BlogDetail>();

            CreateMap<BlogUpdateRequest, Blog>();
            CreateMap<BlogDetailUpdateRequest, BlogDetail>();
        }
    }
}
