using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using RecordManagementAPI.Model;
using Newtonsoft.Json;

namespace RecordManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                return  JsonConvert.SerializeObject(records.FindAll());
            }
        }
                                                                                                                          
        

        // GET api/values/5
        [HttpGet("{id}", Name = "GetRecordById")]
        public string Get(int id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                return JsonConvert.SerializeObject(records.FindOne(x => x.Id == id));
            }
        }

        // GET api/values/3123123123
        [HttpGet]
        [Route("phonenumber/{phoneNumber}")]
        public string GetByPhoneNumber(string phoneNumber)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                return JsonConvert.SerializeObject(records.FindOne(x => x.PhoneNumberPersonal == phoneNumber || x.PhoneNumberProfessional == phoneNumber));
            }
        }

        // GET api/values/john.doe@johndoe.com
        [HttpGet]
        [Route("email/{email}")]
        public string GetByEMail(string email)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                return JsonConvert.SerializeObject(records.FindOne(x => x.Email == email));
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Record item)
        {
            if (item == null)
                return BadRequest();
            
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
//                if (records.Count() == 0)
//                    item.Id = 0;
//                else
//                    item.Id = records.Max(x => x.Id) + 1;
                records.Insert(item);
            }

            return CreatedAtRoute("GetRecordById", new { id = item.Id }, item);
        }

        // PUT api/values
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Record item)
        {
            if (item == null)
                return BadRequest();

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                records.Update(item);
            }

            return CreatedAtRoute("GetRecordById", new { id = item.Id }, item);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                records.Delete(id);
            }
        }
    }
}
