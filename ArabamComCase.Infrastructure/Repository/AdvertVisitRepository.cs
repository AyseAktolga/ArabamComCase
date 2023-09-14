using ArabamComCase.Core.Entities;
using ArabamComCase.Sql.Queries;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArabamComCase.Application.Interfaces;
using Dapper;

namespace ArabamComCase.Infrastructure.Repository
{
    public class AdvertVisitRepository : IAdvertVisitRepository
    {
        #region ===[ Private Members ]=============================================================

        private readonly IConfiguration configuration;

        #endregion

        #region ===[ Constructor ]=================================================================

        public AdvertVisitRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #endregion

        #region ===[ IAdvertVisitRepository Methods ]==================================================

        public async Task<IReadOnlyList<AdvertVisit>> GetAllAsync()
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<AdvertVisit>(AdvertVisitQueries.AllAdvertVisit);
                return result.ToList();
            }
        }

        public async Task<AdvertVisit> GetByIdAsync(long id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<AdvertVisit>(AdvertVisitQueries.AdvertVisitById, new { Id = id });
                return result;
            }
        }

        public async Task<string> AddAsync(AdvertVisit entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertVisitQueries.AddAdvertVisit, entity);
                return result.ToString();
            }
        }

        public async Task<string> UpdateAsync(AdvertVisit entity)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertVisitQueries.UpdateAdvertVisit, entity);
                return result.ToString();
            }
        }

        public async Task<string> DeleteAsync(long id)
        {
            using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(AdvertVisitQueries.DeleteAdvertVisit, new { Id = id });
                return result.ToString();
            }
        }

        #endregion
    }
}
