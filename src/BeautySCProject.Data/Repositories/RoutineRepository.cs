using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Repositories
{
    public class RoutineRepository:Repository<Routine>, IRoutineRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        private readonly IRepository<Customer> _customerRepository;
        public RoutineRepository(IMapper mapper, IUnitOfWork unitOfWork, BeautyscDbContext dbContext) : base(dbContext)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<RoutineViewModel> GetRoutinesBySkinTypeAsync(int skinTypeId)
        {
            var routine = await Entities
                .Where(r => r.SkinTypeId == skinTypeId)
                .Include(r => r.SkinType) 
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category) 
                .FirstOrDefaultAsync();

            return _mapper.Map<RoutineViewModel>(routine);
        }

    }
}
