using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordManagementAPI.Model;
using Newtonsoft.Json;
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

        // PUT api/values
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

        // DELETE api/values/5
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

        //        // POST api/values
        //        [HttpPost]
        //        
        //        public Task addImage(IFormFile file, int id)
        //        {
        ////            if (file == null || file.Length == 0)
        ////                return Content("file not selected");
        //
        //            var pathTemp = Path.GetTempPath();
        //
        //            using (var stream = new FileStream(file.OpenReadStream(), FileMode.ReadOnly))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //
        //            using (var db = new LiteDatabase(@"MyData.db"))
        //            {
        //                var records = db.FileStorage.Upload(id.ToString(), file);
        //            }
        //
        //            return CreatedAtRoute("GetRecordById", new { id = item.Id }, item);
        //        }
        //
        //        [HttpPost]
        //        public async Task<IActionResult> UploadFile(IFormFile file)
        //        {
        //            if (file == null || file.Length == 0)
        //                return Content("file not selected");
        //
        //            var path = Path.Combine(
        //                Directory.GetCurrentDirectory(), "wwwroot",
        //                file.FileName);
        //
        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //
        //            return RedirectToAction("Files");
        //        }
    }
}
