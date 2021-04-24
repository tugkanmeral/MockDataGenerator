using Common.EventBus.RabbitMQBus.Core;
using Common.EventBus.RabbitMQBus.Events.Log;
using Common.EventBus.RabbitMQBus.Producer.Log;
using Microsoft.AspNetCore.Mvc;
using Services.PersonGenerator.Interfaces;
using Services.PersonGenerator.Models;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Services.PersonGenerator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IMockDataGenerator _mockDataGenerator;
        private readonly LogBusProducer _logBusProducer;
        public PersonController(LogBusProducer logBusProducer, IMockDataGenerator mockDataGenerator = null)
        {
            _mockDataGenerator = mockDataGenerator;
            _logBusProducer = logBusProducer;
        }

        [HttpGet]
        public IMockData Get()
        {
            IMockData mockData = _mockDataGenerator.GetMockData();
            string logMessage = ((Person)mockData).Name + " ismine sahip mock person data üretildi.";
            LogEvent logEvent = new LogEvent()
            {
                Message = logMessage
            };

            try
            {
                _logBusProducer.Publish(QueueNames.LOG_QUEUE, logEvent);
            }
            catch (Exception ex)
            {
                throw;
            }

            return mockData;
        }

        [HttpGet("{count}")]
        public IEnumerable<IMockData> Get(int count)
        {
            IEnumerable<IMockData> mockDatas = _mockDataGenerator.GetMockDatas(count);
            string logMessage = mockDatas != null ? $"{count} adet mock data oluşturuldu." : "MockData oluşturulmadı!";

            LogEvent logEvent = new LogEvent()
            {
                Message = logMessage
            };

            try
            {
                _logBusProducer.Publish(QueueNames.LOG_QUEUE, logEvent);
            }
            catch (Exception ex)
            {
                throw;
            }

            return mockDatas;
        }

    }}
