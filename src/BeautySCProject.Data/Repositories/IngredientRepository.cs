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
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private IMapper _mapper;

        public IngredientRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientViewModel>> GetIngredientsAsync()
        {
            return await Entities.ProjectTo<IngredientViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}