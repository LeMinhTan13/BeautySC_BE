﻿using AutoMapper;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
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

        public async Task<Routine> GetRoutinesBySkinTypeAsync(int skinTypeId)
        {
            var routine = await Entities
                .Where(r => r.SkinTypeId == skinTypeId && r.Status == true)
                .Include(r => r.SkinType) 
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category)
                            .ThenInclude(c => c.Products)
                                .ThenInclude(p => p.ProductImages)
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category)
                            .ThenInclude(c => c.Products)
                                .ThenInclude(p => p.ProductIngredients)
                                    .ThenInclude(pi => pi.Ingredient)
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category)
                            .ThenInclude(c => c.Products)
                                .ThenInclude(p => p.ProductSkinTypes)
                .FirstOrDefaultAsync();

            return routine;
        }
        public async Task<bool> CreateRoutineAsync(Routine routine)
        {
            try
            {
                var existingRoutines = await Entities
                    .Where(x => x.SkinTypeId == routine.SkinTypeId && x.Status == true)
                    .ToListAsync();

                foreach (var item in existingRoutines)
                {
                    item.Status = false;
                }
                Entities.Add(routine);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateRoutineAsync(Routine routine)
        {
            try
            {
                var existingRoutines = await Entities
                    .Where(x => x.SkinTypeId == routine.SkinTypeId && x.Status == true)
                    .ToListAsync();

                foreach (var item in existingRoutines)
                {
                    item.Status = false;
                }
                Entities.Update(routine);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<IEnumerable<RoutineGetAllViewModel>> GetAllRoutineAsync()
        {
            return await Entities
                .Include(r => r.SkinType)  
                .Select(r => new RoutineGetAllViewModel
                {
                    RoutineId = r.RoutineId,
                    RoutineName = r.RoutineName,
                    SkinTypeName = r.SkinType.SkinTypeName,
                    SkinTypeId = r.SkinType.SkinTypeId,
                    Status = r.Status
                })
                .ToListAsync();
        }

        public async Task<Routine> GetRoutinesByRoutineIdAsync(int routineId)
        {
            return await Entities
              .Where(r => r.RoutineId == routineId)
              .Include(r => r.SkinType)
              .Include(r => r.RoutineDetails)
                  .ThenInclude(rd => rd.RoutineSteps)
                      .ThenInclude(rs => rs.Category)
                          .ThenInclude(c => c.Products)
                                      .ThenInclude(p => p.ProductImages)
              .Include(r => r.RoutineDetails).ThenInclude(rd => rd.RoutineSteps)
                         .ThenInclude(rs => rs.Category)
                             .ThenInclude(c => c.Products)
                .ThenInclude(p => p.ProductIngredients)
                                     .ThenInclude(pi => pi.Ingredient)
             .FirstOrDefaultAsync();

        }

        public async Task<Routine> GetRoutinesByRoutineIdForAminAsync(int routineId)
        {
            return await Entities
            .Where(r => r.RoutineId == routineId)
            .Include(r => r.SkinType)
            .Include(r => r.RoutineDetails)
                .ThenInclude(rd => rd.RoutineSteps)
                    .ThenInclude(rs => rs.Category)
            .FirstOrDefaultAsync();
        }
    }
}
