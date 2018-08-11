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
        int Points;

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

        //Login: Successful Response
        [Test]
        public void TEST_001_Login_With_Valid_User()
        {
            var response = service.Login("Tester", "Plexure123");
            response.Expect(HttpStatusCode.OK);
           
        }

        //Points: Successful Response Response return 202
        [Test]
        public void TEST_002_Get_Points_For_Logged_In_User()
        {
            var response = service.GetPoints();
            response.Expect(HttpStatusCode.Accepted);
            Points = response.Entity.Value;
        }

        //Purchase: Unsuccessful Response 400 Error: ‘Invalid product id’
        [Test]
        public void TEST_003_Purchase_Invalid_Product()
        {
            int productId = 9999999;
            var response = service.Purchase(productId);
            response.Expect(HttpStatusCode.BadRequest);
        }

        //Purchase: Successful Response return 202
        [Test]
        public void TEST_004_Purchase_Product()
        {
            int productId = 1;
            var response = service.Purchase(productId);
            response.Expect(HttpStatusCode.Accepted);
        }

        //User can earn 100 credit points when transaction complete
        [Test]
        public void TEST_005_Points_Increased_After_Purchase()
        {
            var newPoints = service.GetPoints();
            int newPointValue = newPoints.Entity.Value;
            Assert.AreEqual(Points + 100, newPointValue);
        }

        //Login: Unsuccessful Response if Unsuccessful Response if username and password isn’t matched return 401 { Error: ‘Unauthorized’}
        [Test]
        public void TEST_006_Login_With_Invalid_User()
        {
            var response = service.Login("Tester", "plexure123");
            response.Expect(HttpStatusCode.Unauthorized);
        }

        //Login: Unsuccessful Response if username or password missed Response return 400 { Error: ‘Username and password are required’}
        [Test]
        public void TEST_007_Login_Without_Username_And_Password()
        {
            var response = service.Login(null, null);
            response.Expect(HttpStatusCode.BadRequest);
            Assert.AreEqual("\"Error: Username and password required.\"", response.Error);
        }
    }
}
