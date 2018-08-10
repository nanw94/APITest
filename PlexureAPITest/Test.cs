using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PlexureAPITest
{
    [TestFixture]
    public class Test
    {
        Service service;

        [OneTimeSetUp]
        public void Setup()
        {
            service = new Service();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (service != null)
            {
                service.Dispose();
                service = null;
            }
        }

        [Test]
        public void TEST_001_Login_With_Valid_User()
        {
            var response = service.Login("Testar", "Plexure123");

            response.Expect(HttpStatusCode.OK);
           
        }

        [Test]
        public void TEST_002_Get_Points_For_Logged_In_User()
        {
            var points = service.GetPoints();
        }

        [Test]
        public void TEST_003_Purchase_Product()
        {
            int productId = 1;
            service.Purchase(productId);
        }
    }
}
