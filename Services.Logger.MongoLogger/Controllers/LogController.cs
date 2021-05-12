using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Services.Logger.MongoLogger.Entities;
using Services.Logger.MongoLogger.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logger.MongoLogger.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ILogRepository _logRepository;
        public LogController(ILogRepository logRepository) //LogRepository logRepository
        {
            _logRepository = logRepository;
        }

        [HttpGet]
        public async Task<string> Insert(string message)
        {
            Log log = new();
            log.Message = message;
            _logRepository.InsertOneAsync(log);

            var sortFilter = Builders<Log>.Sort.Descending(l => l._id);
            var lastAddedMessage = await _logRepository.Collection.Find(Builders<Log>.Filter.Empty).Sort(sortFilter).Limit(1).FirstOrDefaultAsync();

            return $"{lastAddedMessage._id} {lastAddedMessage.Message}";
        }
    }
}
