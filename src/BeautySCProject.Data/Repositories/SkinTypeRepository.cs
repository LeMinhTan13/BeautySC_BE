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
    public class SkinTypeRepository: Repository<SkinType>, ISkinTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BeautyscDbContext _dbContext;
        public SkinTypeRepository(IUnitOfWork unitOfWork, BeautyscDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateSkinTypeAsync(SkinType skinType)
        {
            try
            {
                Entities.Add(skinType);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<SkinTypeViewModel>> GetAllSkinTypeAsync()
        {
            return await Entities
            .Select(st => new SkinTypeViewModel
            {
                SkinTypeId = st.SkinTypeId,
                SkinTypeName = st.SkinTypeName
            })
            .ToListAsync();
        }

        public async Task<SkinType?> GetSkinTypeByIdAsync(int skinTypeId)
        {
            var skinType = await Entities.FindAsync(skinTypeId);
            await _unitOfWork.SaveChangesAsync();
            return skinType;
        }

        public async Task<bool> UpdateSkinTypeAsync(SkinType skinType)
        {
            try
            {
                Entities.Update(skinType);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
