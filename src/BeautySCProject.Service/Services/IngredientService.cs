using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<MethodResult<IEnumerable<IngredientViewModel>>> GetIngredientsAsync()
        {
            var result = await _ingredientRepository.GetIngredientsAsync();
            return new MethodResult<IEnumerable<IngredientViewModel>>.Success(result);
        }
    }
}
