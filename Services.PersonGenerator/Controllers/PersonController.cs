using Microsoft.AspNetCore.Mvc;
using Services.PersonGenerator.Interfaces;
using Services.PersonGenerator.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Services.PersonGenerator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IMockDataGenerator _mockDataGenerator;
        public PersonController(IMockDataGenerator mockDataGenerator = null)
        {
            _mockDataGenerator = mockDataGenerator;
        }

        [HttpGet]
        public IMockData Get()
        {
            return _mockDataGenerator.GetMockData();
        }

        [HttpGet("{count}")]
        public IEnumerable<IMockData> Get(int count)
        {
            return _mockDataGenerator.GetMockDatas(count);
        }

    }
}
