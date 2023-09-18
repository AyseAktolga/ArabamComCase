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
        public async Task<IActionResult> All([FromQuery] AdvertGetAllParameterDto advertGetAllParameterDto)
        {

            try
            {
                var result = await _unitOfWork.Adverts.GetAllDtoAsync(advertGetAllParameterDto);
                if (result.Adverts == null)
                {
                    return StatusCode(204, result);
                }

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception:", ex);
                return StatusCode(500, "Internal error occurred");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {

            try
            {
                var result = await _unitOfWork.Adverts.GetByIdAsync(id);
                if (result == null)
                {
                    return StatusCode(204, result);
                }

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception:", ex);
                return StatusCode(500, "Internal error occurred");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Visit(AdvertVisitAddDto advertVisitAddDto)
        {
            try
            {
                if (advertVisitAddDto.AdvertId == 0)
                {
                    throw new Exception();
                }
                AdvertVisit advertVisit = new AdvertVisit { AdvertId = advertVisitAddDto.AdvertId };
                var result = await _unitOfWork.AdvertVisits.AddAsync(advertVisit);

                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception:", ex);

                return StatusCode(500, "Internal error occurred");
            }

        }
    }
}