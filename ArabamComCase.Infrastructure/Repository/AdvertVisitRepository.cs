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
using Microsoft.AspNetCore.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections;


namespace ArabamComCase.Infrastructure.Repository
{
    public class AdvertVisitRepository : IAdvertVisitRepository
    {
        #region ===[ Private Members ]=============================================================

        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region ===[ Constructor ]=================================================================

        public AdvertVisitRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
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
            int result = 0;

            new Thread(async () =>
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    UserName = "guest",
                    Password = "guest",
                };
                using (var connection = connectionFactory.CreateConnection())
                {
                    var channel = connection.CreateModel();

                    channel.QueueDeclare(queue: "AdvertVisit", durable: false, exclusive: false, autoDelete: false);

                    var consumer = new EventingBasicConsumer(channel);

                    channel.BasicConsume(queue: "AdvertVisit", autoAck: false, consumer: consumer);
                }


                entity.IpAdress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
                entity.VisitDate = DateTime.Now;

                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    result = await connection.ExecuteAsync(AdvertVisitQueries.AddAdvertVisit, entity);
                }

            }).Start();

            return result.ToString();
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
