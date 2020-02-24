using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Registration.Interfaces;
using Registration.Models;
using Registration.Models.ReceivedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class ConsumeRabbitMQHostedService : IConsumeRabbitMQHostedService<UserAnswer>
    {
        #region fields
        private readonly ConnectionFactory connectionFactory;
        private const string queueName = "test-queue";
        #endregion

        public ConsumeRabbitMQHostedService(ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void SendMessage(UserAnswer message)
        {
            using(var connection = connectionFactory.CreateConnection())
            using(var model = connection.CreateModel())
            {
                model.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false);

                var jsonString = JsonConvert.SerializeObject(message);
                var arrayBytes = Encoding.UTF8.GetBytes(jsonString);

                model.BasicPublish(string.Empty, queueName, body: arrayBytes);
            }
        }
    }
}
