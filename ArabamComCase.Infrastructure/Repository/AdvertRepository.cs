using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArabamComCase.Application.Interfaces;
using ArabamComCase.Sql.Queries;
using Dapper;
using ArabamComCase.Core.Entities;
using ArabamComCase.Core.Models;
using ArabamComCase.Core.DTOs;
using ArabamComCase.Core.Enums;

namespace ArabamComCase.Infrastructure.Repository
{
    public class AdvertRepository : IAdvertRepository
    {
        #region ===[ Private Members ]=============================================================

        private readonly IConfiguration configuration;

        #endregion

        #region ===[ Constructor ]=================================================================

        public AdvertRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #endregion

        #region ===[ IAdvertRepository Methods ]==================================================

        public async Task<IReadOnlyList<Advert>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Advert>(AdvertQueries.AllAdvert);
                return result.ToList();
            }
        }

        public async Task<Advert> GetByIdAsync(long id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Advert>(AdvertQueries.AdvertById, new { Id = id });
                return result;
            }
        }

        public async Task<string> AddAsync(Advert entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertQueries.AddAdvert, entity);
                return result.ToString();
            }
        }

        public async Task<string> UpdateAsync(Advert entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertQueries.UpdateAdvert, entity);
                return result.ToString();
            }
        }

        public async Task<string> DeleteAsync(long id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertQueries.DeleteAdvert, new { Id = id });
                return result.ToString();
            }
        }

        public async Task<AdvertGetAllDto> GetAllDtoAsync(AdvertGetAllParameterDto parameter)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AdvertDto>(AdvertQueries.AllAdvertDto);
                var advertGetAllDto = new AdvertGetAllDto();
                advertGetAllDto.Adverts = result.ToList();
                advertGetAllDto.Total = advertGetAllDto.Adverts.Count();
                advertGetAllDto.Page = parameter.PageNumber;
                var adverts = advertGetAllDto.Adverts;

                if (parameter.CategoryId != 0)
                {
                    adverts = adverts.Where(x => x.CategoryId == parameter.CategoryId).ToList();
                }
                if (parameter.PriceMin != 0 && parameter.PriceMax != 0)
                {
                    adverts = adverts.Where(x => x.Price >= parameter.PriceMin && x.Price <= parameter.PriceMax).ToList();
                }
                else if (parameter.PriceMin != 0)
                {
                    adverts = adverts.Where(x => x.Price >= parameter.PriceMin).ToList();
                }
                else if (parameter.PriceMax != 0)
                {
                    adverts = adverts.Where(x => x.Price <= parameter.PriceMax).ToList();
                }
                if (parameter.GearEnum != GearEnum.All)
                {
                    adverts = adverts.Where(x => x.Gear == parameter.GearEnum.ToString()).ToList();
                }
                if (parameter.GearEnum != GearEnum.All)
                {
                    adverts = adverts.Where(x => x.Gear == parameter.GearEnum.ToString()).ToList();
                }
                if (parameter.AdvertSorting != AdvertSorting.NoSorting)
                {
                    if (parameter.AdvertSorting == AdvertSorting.KmLowToHigh)
                    {
                        adverts = adverts.OrderBy(x => x.Km).ToList();
                    }
                    if (parameter.AdvertSorting == AdvertSorting.KmHighToLow)
                    {
                        adverts = adverts.OrderByDescending(x => x.Km).ToList() ;
                    }
                    if (parameter.AdvertSorting == AdvertSorting.PriceLowToHigh)
                    {
                        adverts = adverts.OrderBy(x => x.Price).ToList()    ;
                    }
                    if (parameter.AdvertSorting == AdvertSorting.PriceHighToLow)
                    {
                        adverts = adverts.OrderByDescending(x => x.Price).ToList()  ;
                    }
                    if (parameter.AdvertSorting == AdvertSorting.YearLowToHigh)
                    {
                        adverts = adverts.OrderBy(x => x.Year).ToList();
                    }
                    if (parameter.AdvertSorting == AdvertSorting.YearHighToLow)
                    {
                        adverts = adverts.OrderByDescending(x => x.Year).ToList();
                    }
                }

                if(parameter.PageNumber == 0 && parameter.PageSize == 0)
                {
                    advertGetAllDto.Adverts = adverts;
                }
                else
                {
                    advertGetAllDto.Adverts = adverts.Skip((parameter.PageNumber - 1) * parameter.PageSize).Take(parameter.PageSize).ToList();
                }

                return advertGetAllDto;
            }
        }

        #endregion
    }
}
