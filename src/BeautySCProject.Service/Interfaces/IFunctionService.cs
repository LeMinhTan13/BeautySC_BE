﻿using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Interfaces
{
    public interface IFunctionService
    {
        Task<MethodResult<IEnumerable<FunctionViewModel>>> GetFunctionsAsync();
    }
}
