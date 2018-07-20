using System;
using System.Collections.Generic;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RecordManagementAPI.Model;

namespace RecordManagementAPI.Tests
{
    [TestClass]
    public class ValuesControllerTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Record testRecord = new Record()
            {
                Name = "Test Tester",
                Company = "testCompany",
                Email = "test.test@test.test",
                BirthDate = new DateTime(1980, 12, 5),
                PhoneNumberPersonal = "3123123123",
                PhoneNumberProfessional = "3123123120",
                Address = "test"
            };

            ConnectionString connectionString = new ConnectionString("MyData.db");
            connectionString.Mode = FileMode.Exclusive;

            using (var db = new LiteDatabase(connectionString))
            {
                
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                records.Insert(testRecord);
            }
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            ConnectionString connectionString = new ConnectionString("MyData.db");
            connectionString.Mode = FileMode.Exclusive;

            using (var db = new LiteDatabase(connectionString))            {
                var records = db.GetCollection<Record>("records");
                records.Delete(records.Max(r => r.Id));
            }
        }

        [TestMethod]
        public void GetByEMail_invalidMail_shouldReturnNull()
        {
            var controller = new Controllers.ValuesController();
            var result = controller.GetByEMail("test");
            Assert.AreEqual("[]", result);
        }

        [TestMethod]
        public void GetByEMail_validMail_shouldReturnRecord()
        {
            var controller = new Controllers.ValuesController();
            var result = JsonConvert.DeserializeObject<List<Record>>(controller.GetByEMail("test.test@test.test"));
            Assert.AreEqual("Test Tester", result[0].Name);
        }

        [TestMethod]
        public void GetByPhoneNumber_invalidNumber_shouldReturnNull()
        {
            var controller = new Controllers.ValuesController();
            var result = controller.GetByPhoneNumber("3212");
            Assert.AreEqual("[]", result);
        }

        [TestMethod]
        public void GetByPhoneNumber_validPersonalNumber_shouldReturnRecord()
        {
            var controller = new Controllers.ValuesController();
            var result = JsonConvert.DeserializeObject<List<Record>>(controller.GetByPhoneNumber("3123123123"));
            Assert.AreEqual("Test Tester", result[0].Name);
        }

        [TestMethod]
        public void GetByPhoneNumber_validProfessionalNumber_shouldReturnRecord()
        {
            var controller = new Controllers.ValuesController();
            var result = JsonConvert.DeserializeObject<List<Record>>(controller.GetByPhoneNumber("3123123120"));
            Assert.AreEqual("Test Tester", result[0].Name);
        }
    }
}
