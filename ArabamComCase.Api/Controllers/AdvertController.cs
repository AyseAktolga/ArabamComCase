using ArabamComCase.Api.Models;
using ArabamComCase.Application.Interfaces;
using ArabamComCase.Core.Entities;
using ArabamComCase.Core.Models;
using ArabamComCase.Logging;
using Microsoft.AspNetCore.Mvc;

namespace ArabamComCase.Api.Controllers
{
    public class AdvertController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdvertController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<AdvertGetAllDto> All()
        {
            var result = await _unitOfWork.Adverts.GetAllDtoAsync();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<Advert> Get(long id)
        {
            var result = await _unitOfWork.Adverts.GetByIdAsync(id);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> Visit(AdvertVisitAddDto advertVisitAddDto )
        {
            AdvertVisit advertVisit = new AdvertVisit { AdvertId = advertVisitAddDto.AdvertId };
            var result = await _unitOfWork.AdvertVisits.AddAsync(advertVisit);
            var apiResponse = new ApiResponse<string>();
            apiResponse.Success = true;
            apiResponse.Result = result;
            return apiResponse;
        }
    }
}