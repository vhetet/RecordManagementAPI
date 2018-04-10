using System;
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

            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<RecordManagementAPI.Model.Record>("records");
                records.Insert(testRecord);
            }
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var records = db.GetCollection<Record>("records");
                records.Delete(records.Max(r => r.Id));
            }
        }

        [TestMethod]
        public void GetByEMail_invalidNumber_shouldReturnNull()
        {
            var controller = new Controllers.ValuesController();
            var result = controller.GetByEMail("test");
            Assert.AreEqual("null", result);
        }

        [TestMethod]
        public void GetByEMail_validNumber_shouldReturnRecord()
        {
            var controller = new Controllers.ValuesController();
            var result = JsonConvert.DeserializeObject<Record>(controller.GetByEMail("test.test@test.test"));
            Assert.AreEqual("Test Tester", result.Name);
        }
    }
}
