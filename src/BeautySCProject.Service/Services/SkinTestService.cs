using AutoMapper;
using BeautySCProject.Common.Helpers;
using BeautySCProject.Data.Entities;
using BeautySCProject.Data.Interfaces;
using BeautySCProject.Data.Models.SkinTestModel;
using BeautySCProject.Data.Repositories;
using BeautySCProject.Data.ViewModels;
using BeautySCProject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySCProject.Service.Services
{
    public class SkinTestService : ISkinTestService
    {
        private readonly IMapper _mapper;
        private readonly ISkinTestRepository _skinTestRepository;
        private readonly ISkinTypeQuestionRepository _skinTypeQuestionRepository;
        private readonly ISkinTypeAnswerRepository _skinTypeAnswerRepository;
        private readonly ICustomerRepository _customerRepository;
        public SkinTestService(IMapper mapper,ISkinTestRepository skinTestRepository, ISkinTypeQuestionRepository skinTypeQuestionRepository,ISkinTypeAnswerRepository skinTypeAnswerRepository, ICustomerRepository customerRepository) 
        { 
            _skinTestRepository = skinTestRepository;
            _skinTypeQuestionRepository = skinTypeQuestionRepository;
            _skinTypeAnswerRepository = skinTypeAnswerRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<MethodResult<string>> CreateSkinTestAsync(SkinTestCreateRequest request)
        {
            try
            {
                var skinTest = _mapper.Map<SkinTest>(request);
                var checkCreSkinTest = await _skinTestRepository.CreateSkinTestAsync(skinTest);
                if (!checkCreSkinTest)
                {
                    return new MethodResult<string>.Failure("Fail while create skin-test", StatusCodes.Status500InternalServerError);
                }
                
                return new MethodResult<string>.Success("Create Skin Test succesfully");
            }

            catch (Exception ex)
            {
                return new MethodResult<string>.Failure($"Error: {ex.Message}", StatusCodes.Status500InternalServerError);

            }
        }

        public async Task<MethodResult<SkinTestViewModel>> GetSkinTestAsync(int skinTestId)
        {
            
            var result = await _skinTestRepository.GetSkinTestAsync(skinTestId);
            if (result == null)
            {
                return new MethodResult<SkinTestViewModel>.Failure("Skin test not found", StatusCodes.Status404NotFound);
            }    
            return new MethodResult<SkinTestViewModel>.Success(result);
            
        }
        public async Task<SkinTest> GetSkinTestByIdAsync(int skinTestId)
        {
            return await _skinTestRepository.GetSkinTestByIdAsync(skinTestId);
        }
        public async Task<MethodResult<IEnumerable<SkinTestGetAllViewModel>>> GetAllSkinTestAsync()
        {
            var result = await _skinTestRepository.GetAllSkinTestAsync();
            return new MethodResult<IEnumerable<SkinTestGetAllViewModel>>.Success(result);
        }
        public async Task<MethodResult<string>> UpdateSkinTestAsync(SkinTestUpdateRequest request)
        {
            try
            {
                var existingSkinTest = await _skinTestRepository.GetSkinTestByIdAsync(request.SkinTestId);
                if (existingSkinTest == null)
                {
                    return new MethodResult<string>.Failure("Skin test not found", StatusCodes.Status404NotFound);
                }

                existingSkinTest.SkinTestName = request.SkinTestName;
                existingSkinTest.Status = request.Status;

                foreach (var skinTypeQuestion in request.SkinTypeQuestions)
                {
                    var existingQuestion = existingSkinTest.SkinTypeQuestions
                        .FirstOrDefault(q => q.SkinTypeQuestionId == skinTypeQuestion.SkinTypeQuestionId);

                    if (existingQuestion != null)
                    {
                        existingQuestion.Description = skinTypeQuestion.Description;
                        existingQuestion.Type = skinTypeQuestion.Type;

                        foreach (var skinTypeAnswer in skinTypeQuestion.SkinTypeAnswers)
                        {
                            var existingAnswer = existingQuestion.SkinTypeAnswers
                                .FirstOrDefault(a => a.SkinTypeAnswerId == skinTypeAnswer.SkinTypeAnswerId);

                            if (existingAnswer != null)
                            {
                                existingAnswer.Description = skinTypeAnswer.Description;
                                existingAnswer.SkinTypeId = skinTypeAnswer.SkinTypeId;
                            }
                            else
                            {
                                existingQuestion.SkinTypeAnswers.Add(new SkinTypeAnswer
                                {
                                    SkinTypeId = skinTypeAnswer.SkinTypeId,
                                    Description = skinTypeAnswer.Description
                                });
                            }
                        }
                    }
                    else
                    {
                        var newQuestion = new SkinTypeQuestion
                        {
                            Description = skinTypeQuestion.Description,
                            Type = skinTypeQuestion.Type,
                            SkinTypeAnswers = skinTypeQuestion.SkinTypeAnswers.Select(a => new SkinTypeAnswer
                            {
                                SkinTypeId = a.SkinTypeId,
                                Description = a.Description
                            }).ToList()
                        };
                        existingSkinTest.SkinTypeQuestions.Add(newQuestion);
                    }
                }

                var updateResult = await _skinTestRepository.UpdateSkinTestAsync(existingSkinTest);
                if (!updateResult)
                {
                    return new MethodResult<string>.Failure("Fail while update skin test!", StatusCodes.Status500InternalServerError);
                }

                return new MethodResult<string>.Success("Skin test updated successfully");
            }
            catch (Exception ex)
            {
                return new MethodResult<string>.Failure($"Error: {ex.Message}", StatusCodes.Status500InternalServerError);
            }
        }


        //xác định loại da 
        public async Task<MethodResult<object>> DetermineSkinTypeAsync(int customerId,ListUserAnswerModel model)
        {
            var skinTypeScores = new Dictionary<int, int>(); 

            var allAnswers = new List<int>();
            allAnswers.AddRange(model.ListUserAnswer);

            var skinTypeAnswers = await _skinTestRepository.GetSkinTypeAnswersAsync(allAnswers);
            if (skinTypeAnswers == null || !skinTypeAnswers.Any())
            {
                return new MethodResult<object>.Failure("No skin type answers found.", 404);
            }

            // Tính điểm cho từng loại da
            foreach (var answer in skinTypeAnswers)
            {
                if (!skinTypeScores.ContainsKey(answer.SkinTypeId))
                {
                    skinTypeScores[answer.SkinTypeId] = 0;
                }
                skinTypeScores[answer.SkinTypeId]++;
            }

            // Lấy danh sách các loại da với điểm số của chúng
            var skinTypeIds = skinTypeScores.Keys.ToList();
            var skinTypes = new List<SkinType>();
            foreach (var skinTypeId in skinTypeIds)
            {
                var skinType = await _skinTestRepository.GetSkinTypeByIdAsync(skinTypeId);
                if (skinType != null)
                {
                    skinTypes.Add(skinType);
                }
            }

            if (!skinTypes.Any())
            {
                return new MethodResult<object>.Failure("Skin type not found.", 404);
            }
            
            var bestSkinType = skinTypes
                .OrderByDescending(st => skinTypeScores[st.SkinTypeId]) // Ưu tiên điểm số cao nhất
                .ThenBy(st => st.Priority) // Nếu điểm số bằng nhau, ưu tiên priority nhỏ hơn
                .First();
            var skinResult = _mapper.Map<SkinTestResultViewModel>(bestSkinType);
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return new MethodResult<object>.Failure("Fail to get customer!", StatusCodes.Status500InternalServerError);
            } 
            
            customer.SkinTypeId = skinResult.SkinTypeId;
            var checkSetSkinType = await _customerRepository.UpdateCustomerAsync(customer);
            if (!checkSetSkinType)
            {
                return new MethodResult<object>.Failure("Fail while set skin type!", StatusCodes.Status500InternalServerError);
            }
            // Trả về kết quả
            return new MethodResult<object>.Success(skinResult);
        }

        
    }
}
