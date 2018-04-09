using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecordManagementAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new Controllers.ValuesController();
            var result = controller.GetByEMail("test");
            Assert.AreEqual("", result);
        }
    }
}
