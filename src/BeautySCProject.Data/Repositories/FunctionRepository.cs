using AutoMapper.QueryableExtensions;
using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BeautySCProject.Data.Repositories
{
    public class FunctionRepository : Repository<Function>, IFunctionRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private IMapper _mapper;

        public FunctionRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FunctionViewModel>> GetFunctionsAsync()
        {
            return await Entities.ProjectTo<FunctionViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}