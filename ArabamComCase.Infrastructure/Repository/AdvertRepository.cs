using ArabamComCase.Application.Interfaces;
using ArabamComCase.Core.DTOs;
using ArabamComCase.Core.Entities;
using ArabamComCase.Core.Models;
using ArabamComCase.Sql.Queries;
using Dapper;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

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

        public async Task<Advert> AddAsync(Advert entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertQueries.AddAdvert, entity);
                return entity;
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

        public async Task<AdvertGetAllDto> GetAllDtoAsync(AdvertGetAllParameterDto advertGetAllParameterDto)
        {
            AdvertGetAllDto advertGetAllDto = new AdvertGetAllDto();
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();

                DynamicParameters parameter2 = new DynamicParameters();

                parameter2.Add("@CategoryId", advertGetAllParameterDto.CategoryId);
                parameter2.Add("@PriceMin", advertGetAllParameterDto.PriceMin);
                parameter2.Add("@PriceMax", advertGetAllParameterDto.PriceMax);
                parameter2.Add("@Gear", advertGetAllParameterDto?.Gear);
                parameter2.Add("@Fuel", advertGetAllParameterDto?.Fuel);
                parameter2.Add("@Page", advertGetAllParameterDto.Page);
                parameter2.Add("@PageSize", advertGetAllParameterDto.PageSize);
                parameter2.Add("@SortingColumn", advertGetAllParameterDto?.SortingColumn);
                parameter2.Add("@SortingOrder", advertGetAllParameterDto?.SortingOrder);
                //parameter2.Add("@TotalCount", dbType: DbType.Int32, direction:ParameterDirection.Output);

                var result = await connection.QueryAsync<Advert>("dbo.GetFilteredAndSortedCarsWithPaging", parameter2, commandType: CommandType.StoredProcedure);
                if (result.Any())
                {
                    advertGetAllDto.Total = result.FirstOrDefault().TotalCount;
                    advertGetAllDto.Page = advertGetAllParameterDto.Page;
                    advertGetAllDto.Adverts = new List<AdvertDto>();
                    advertGetAllDto.Adverts = result.Select(source => source.Adapt<AdvertDto>()).ToList();
                }
            }
            return advertGetAllDto;
        }

        #endregion
    }
}
