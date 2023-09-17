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
using System.Text.Json;

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

        public async Task<AdvertVisit> AddAsync(AdvertVisit entity)
        {
            AdvertVisitProducer(entity);
            Thread.Sleep(1000);
            AdvertVisitConsumer();
            Thread.Sleep(1000);
            return entity;
        }

        public bool AdvertVisitProducer(AdvertVisit entity)
        {
            entity.IpAdress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            entity.VisitDate = DateTime.Now;
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    UserName = "guest",
                    Password = "guest",
                };

                using (var connection = connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "AdvertVisit",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string logMessageJson = JsonSerializer.Serialize(entity);

                    var body = Encoding.UTF8.GetBytes(logMessageJson);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "AdvertVisit",
                                         basicProperties: null,
                                         body: body);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }


        public bool AdvertVisitConsumer()
        {

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    UserName = "guest",
                    Password = "guest",
                };

                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "AdvertVisit",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                   consumer.Received += (model, mq) =>
                    {                      
                        var body = mq.Body.ToArray();
                        var logMessageString = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Log message received: {logMessageString}");

                        var logMessage = JsonSerializer.Deserialize<AdvertVisit>(logMessageString);

                        using (IDbConnection connection2 = new SqlConnection(configuration.GetConnectionString("DBConnection")))
                        {
                            connection2.Open();
                            connection2.ExecuteAsync(AdvertVisitQueries.AddAdvertVisit, logMessage).Wait();
                        }

                    };

                    channel.BasicConsume(queue: "AdvertVisit",
                                         autoAck: true,
                                         consumer: consumer);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
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
