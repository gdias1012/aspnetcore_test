using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using remember.Data;

namespace remember.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly Dictionary<int, string> d = new Dictionary<int, string>();

        private readonly IMongoDatabase db = null;

        public ValuesController(IConfiguration config) 
        {
            d.Add(1, "Value 1");
            d.Add(2, "Value 2");
            d.Add(3, "Value 3");
            d.Add(4, "Value 4");
            d.Add(5, "Value 5");

            db = new MongoClient(config.GetSection("MongoConnection:ConnectionString").Value).GetDatabase(config.GetSection("MongoConnection:Database").Value);


        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return d.Values;
        }

        [HttpGet("abcd")]
        public IEnumerable<string> GetItem()
        {
            return db.GetCollection<Test>("test").Find(_ => true).ToList().Select(x => x.name);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return from x in d
            //where x.Key.Equals(id)
            //select x.Value;
            var data = d.FirstOrDefault(x => x.Key.Equals(id)).Value;
            if (data != null)
                return new ObjectResult(data);
            else
                return NotFound();
        }
    }
}
