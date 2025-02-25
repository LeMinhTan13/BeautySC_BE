using BeautySCProject.Data.Entities;
using BeautySCProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IRoutineRepository
    {
        Task<RoutineViewModel?> GetRoutinesBySkinTypeAsync(int skinTypeId);
        Task<Routine?> GetRoutinesByRoutineIdAsync(int routineId);
        Task<bool> CreateRoutineAsync(Routine routine);
        /*Task<bool> DeleteRoutineAsync(int routineId);*/
        Task<bool> UpdateRoutineAsync(Routine routine);
        Task<IEnumerable<RoutineGetAllViewModel>> GetAllRoutineAsync();
    }
}
