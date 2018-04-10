using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace RecordManagementAPI.Model
{
    public class Record
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Company { get; set; }
        public String Email { get; set; }
        public DateTime BirthDate { get; set; }
        public String PhoneNumberProfessional { get; set; }
        public String PhoneNumberPersonal { get; set; }
        public String Address { get; set; }
        public bool HasImage { get; set; }

        public static void PopulateDB()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<Record>("records");

//                var record = new Record
//                {
//                    Id = 1,
//                    Name = "John Doe",
//                    Company = "Chicken and co",
//                    Email = "john@chickenandco.com"
//                };
//
//                records.Insert(record);
            }
        }
    }

    
}
