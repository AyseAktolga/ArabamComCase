using ArabamComCase.Core.Entities;
using ArabamComCase.Core.Models;

namespace ArabamComCase.Application.Interfaces
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<AdvertGetAllDto> GetAllDtoAsync(AdvertGetAllParameterDto advertGetAllParameterDto);
    }
}
