﻿using BeautySCProject.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Data.Interfaces
{
    public interface IFunctionRepository
    {
        Task<IEnumerable<FunctionViewModel>> GetFunctionsAsync();
    }
}
