using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecordManagementAPI.Model;
using System.IO;
using System.Threading.Tasks;
using FileMode = System.IO.FileMode;

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
                var records = db.GetCollection<Record>("records");
                return  JsonConvert.SerializeObject(records.FindAll());
            }
        }
                                                                                                                          
        

        // GET api/values/5
        [HttpGet("{id}", Name = "GetRecordById")]
        public string Get(int id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<Record>("records");
                return JsonConvert.SerializeObject(records.FindById(id));
            }
        }

        // GET api/values/3123123123
        [HttpGet]
        [Route("phonenumber/{phoneNumber}")]
        public string GetByPhoneNumber(string phoneNumber)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<Record>("records");
                return JsonConvert.SerializeObject(records.Find(x => x.PhoneNumberPersonal == phoneNumber || x.PhoneNumberProfessional == phoneNumber));
            }
        }

        // GET api/values/john.doe@johndoe.com
        [HttpGet]
        [Route("email/{email}")]
        public string GetByEMail(string email)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<Record>("records");
                return JsonConvert.SerializeObject(records.Find(x => x.Email == email));
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
                var records = db.GetCollection<Record>("records");
                records.Insert(item);
            }

            return CreatedAtRoute("GetRecordById", new { id = item.Id }, item);
        }

        // PUT api/values/id
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Record item)
        {
            if (item == null)
                return BadRequest();

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<Record>("records");
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
                var records = db.GetCollection<Record>("records");
                db.FileStorage.Delete(id.ToString());
                records.Delete(id);
            }
        }

        // GET api/values/image/5
        [HttpGet]
        [Route("image/{id}")]
        public IActionResult GetImage(string id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                if (!db.FileStorage.Exists(id))
                    return NotFound();
                var stream = db.FileStorage.OpenRead(id);
                return File(stream, "image/" + Path.GetExtension(stream.FileInfo.Filename));
            }
        }

        // POST api/values/image/5
        [HttpPost]
        [Route("image/{id}")]
        public async Task<IActionResult> Post(IFormFile file, int id)
        {
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    using (var db = new LiteDatabase(@"MyData.db"))
                    {
                        db.FileStorage.Upload(id.ToString(), "image" + id, stream);
                        var records = db.GetCollection<Record>("records");
                        records.FindById(id).HasImage = true;

                    }
                }
            }

            return Ok("Upload succeed");
        }

        // DELETE api/values/image/5
        [HttpDelete]
        [Route("image/{id}")]
        public void DeleteImage(int id)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                db.FileStorage.Delete(id.ToString());
                var records = db.GetCollection<Record>("records");
                records.FindOne(r => r.Id == id).HasImage = false;
            }
        }
    }
}
