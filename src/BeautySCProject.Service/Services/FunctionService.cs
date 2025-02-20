using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;

        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        public async Task<MethodResult<IEnumerable<FunctionViewModel>>> GetFunctionsAsync()
        {
            var result = await _functionRepository.GetFunctionsAsync();
            return new MethodResult<IEnumerable<FunctionViewModel>>.Success(result);
        }
    }
}
