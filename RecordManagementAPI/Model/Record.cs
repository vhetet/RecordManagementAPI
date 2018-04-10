using LiteDB;
using System;

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

                Record testRecord1 = new Record()
                {
                    Id = 1,
                    Name = "Test Tester1",
                    Company = "testCompany",
                    Email = "test.test@test.test",
                    BirthDate = new DateTime(1980, 12, 5),
                    PhoneNumberPersonal = "3123123123",
                    PhoneNumberProfessional = "3123123120",
                    Address = "test"
                };
                Record testRecord2 = new Record()
                {
                    Id = 2,
                    Name = "Test Tester2",
                    Company = "testCompany",
                    Email = "test.test@test.test",
                    BirthDate = new DateTime(1980, 12, 5),
                    PhoneNumberPersonal = "3123123123",
                    PhoneNumberProfessional = "3123123120",
                    Address = "test"
                };
                Record testRecord3 = new Record()
                {
                    Id = 3,
                    Name = "Test Tester3",
                    Company = "testCompany",
                    Email = "test.test@test.test",
                    BirthDate = new DateTime(1980, 12, 5),
                    PhoneNumberPersonal = "3123123123",
                    PhoneNumberProfessional = "3123123120",
                    Address = "test"
                };
                Record testRecord4 = new Record()
                {
                    Id = 4,
                    Name = "Test Tester4",
                    Company = "testCompany",
                    Email = "test.test@test.test",
                    BirthDate = new DateTime(1980, 12, 5),
                    PhoneNumberPersonal = "3123123123",
                    PhoneNumberProfessional = "3123123120",
                    Address = "test"
                };
                Record testRecord5 = new Record()
                {
                    Id = 5,
                    Name = "Test Tester5",
                    Company = "testCompany",
                    Email = "test.test@test.test",
                    BirthDate = new DateTime(1980, 12, 5),
                    PhoneNumberPersonal = "3123123123",
                    PhoneNumberProfessional = "3123123120",
                    Address = "test"
                };
                if(!records.Exists(r => r.Id == 1))
                    records.Insert(testRecord1);
                if (!records.Exists(r => r.Id == 2))
                    records.Insert(testRecord2);
                if (!records.Exists(r => r.Id == 3))
                    records.Insert(testRecord3);
                if (!records.Exists(r => r.Id == 4))
                    records.Insert(testRecord4);
                if (!records.Exists(r => r.Id == 5))
                    records.Insert(testRecord5);
            }
        }
    }

    
}
