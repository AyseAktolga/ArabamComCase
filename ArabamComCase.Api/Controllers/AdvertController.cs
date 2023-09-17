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
        public async Task<IActionResult> All(AdvertGetAllParameterDto advertGetAllParameterDto)
        {

            var apiResponse = new ApiResponse<AdvertGetAllDto>();
            try
            {
                var result = await _unitOfWork.Adverts.GetAllDtoAsync(advertGetAllParameterDto);
                if (result == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No adverts found";
                    return StatusCode(204, apiResponse);
                }
                apiResponse.Success = true;
                apiResponse.Message = "Successful operation";
                apiResponse.Result = result;

                return StatusCode(200, apiResponse);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception:",ex);
                apiResponse.Success = false;
                apiResponse.Message = "Internal error occurred";
                return StatusCode(500, apiResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var apiResponse = new ApiResponse<Advert>();
            try
            {
                var result = await _unitOfWork.Adverts.GetByIdAsync(id);
                if (result == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No advert found";
                    return StatusCode(204, apiResponse);
                }
                apiResponse.Success = true;
                apiResponse.Message = "Successful operation";
                apiResponse.Result = result;

                return StatusCode(200, apiResponse);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception:", ex);
                apiResponse.Success = false;
                apiResponse.Message = "Internal error occurred";
                return StatusCode(500, apiResponse);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Visit(AdvertVisitAddDto advertVisitAddDto )
        {
            var apiResponse = new ApiResponse<AdvertVisit>();
            try
            {
                if(advertVisitAddDto.AdvertId == 0)
                {
                    throw new Exception();
                }
                AdvertVisit advertVisit = new AdvertVisit { AdvertId = advertVisitAddDto.AdvertId };
                var result = await _unitOfWork.AdvertVisits.AddAsync(advertVisit);

                apiResponse.Success = true;
                apiResponse.Message = "visit created";
                apiResponse.Result = result;

                return StatusCode(201, apiResponse);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception:", ex);
                apiResponse.Success = false;
                apiResponse.Message = "Internal error occurred";
                return StatusCode(500, apiResponse);
            }

        }
    }
}