using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private LogRepository _logRepository;
        public LogController(LogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet]
        public async Task<string> Insert(string message)
        {
            _logRepository.Insert(message);
            var addedMessage = await _logRepository.Get(message);
            return addedMessage;
        }
    }
}
