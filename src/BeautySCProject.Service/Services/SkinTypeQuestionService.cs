using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.SkinTypeQuestionModel;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class SkinTypeQuestionService : ISkinTypeQuestionService
    {
        private readonly IMapper _mapper;
        private readonly ISkinTypeQuestionRepository _skinTypeQuestionRepository;
        public SkinTypeQuestionService(IMapper mapper, ISkinTypeQuestionRepository skinTypeQuestionRepository)
        {
            _mapper = mapper;
            _skinTypeQuestionRepository = skinTypeQuestionRepository;
        }

        public async Task<MethodResult<string>> CreateSkinTypeQuestionAsync(int skinTestId, List<SkinTypeQuestionModel> questions)
        {
            var skinTypeQuestions = _mapper.Map<List<SkinTypeQuestion>>(questions);
            foreach (var question in skinTypeQuestions)
            {
                question.SkinTestId = skinTestId;
            }
            var checkCreQuestions = await _skinTypeQuestionRepository.CreateSkinTypeQuestionAsync(skinTypeQuestions);
            if (!checkCreQuestions)
            {
                return new MethodResult<string>.Failure("Fail while create skin-test", StatusCodes.Status500InternalServerError);
            }
            return new MethodResult<string>.Success("Create Skin Test succesfully");
        }

        public Task<bool> DeleteSkinTypeQuestionBySkinTypeIdAsync(int skinTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
