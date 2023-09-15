using ArabamComCase.Core.Entities;
using ArabamComCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabamComCase.Application.Interfaces
{
    public interface IAdvertRepository : IRepository<Advert>
    {
        Task<AdvertGetAllDto> GetAllDtoAsync(AdvertGetAllParameterDto advertGetAllParameterDto);
    }
}
