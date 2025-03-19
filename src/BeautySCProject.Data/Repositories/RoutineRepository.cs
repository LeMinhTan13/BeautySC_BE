using AutoMapper;
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
                .Where(r => r.SkinTypeId == skinTypeId)
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
                if (routine.Status == true)
                {
                    var routines = await Entities.Where(x => x.Status == true).ToListAsync();
                    foreach (var item in routines)
                    {
                        item.Status = false;
                    }
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
                if (routine.Status == true)
                {
                    var routines = await Entities.Where(x => x.Status == true).ToListAsync();
                    foreach (var item in routines)
                    {
                        item.Status = false;
                    }
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
            var routine = await Entities
                .Where(r => r.RoutineId == routineId)
                .Include(r => r.SkinType)
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category)
                            .ThenInclude(c => c.Products)
                                .ThenInclude(p => p.ProductSkinTypes) // Include danh sách ProductSkinTypes
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category)
                            .ThenInclude(c => c.Products)
                                .ThenInclude(p => p.ProductImages) // Include ảnh sản phẩm
                .Include(r => r.RoutineDetails)
                    .ThenInclude(rd => rd.RoutineSteps)
                        .ThenInclude(rs => rs.Category)
                            .ThenInclude(c => c.Products)
                                .ThenInclude(p => p.ProductIngredients) // Include danh sách ProductIngredients
                                    .ThenInclude(pi => pi.Ingredient) // Include thông tin Ingredient của sản phẩm
                .FirstOrDefaultAsync();


            // Lọc danh sách sản phẩm sau khi lấy từ DB
            if (routine != null)
            {
                foreach (var routineDetail in routine.RoutineDetails)
                {
                    foreach (var routineStep in routineDetail.RoutineSteps)
                    {
                        routineStep.Category.Products = routineStep.Category.Products
                            .Where(p => p.ProductSkinTypes.Any(pst => pst.SkinTypeId == routine.SkinType.SkinTypeId))
                            .ToList();
                    }
                }
            }

            return routine;
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
