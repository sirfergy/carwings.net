using System.Threading.Tasks;
using carwings.net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FunctionalTests
    {
        public TestContext TestContext
        {
            get; set;
        }

        [TestMethod]
        public async Task Login()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            Assert.IsNotNull(vehicles);
            Assert.IsTrue(vehicles.Length > 0);
            Assert.IsNotNull(vehicles[0].VIN);
        }

        [TestMethod]
        [Ignore]
        public async Task RefreshBatteryStatus()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            var vin = vehicles[0].VIN;
            var response = await carwings.RefreshBatteryStatus(vin);
            Assert.IsNotNull(response.BatteryRecord);
        }

        [TestMethod]
        [Ignore]
        public async Task HvacOff()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            var vin = vehicles[0].VIN;
            var response = await carwings.HvacOff(vin);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task HvacOn()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            var vin = vehicles[0].VIN;
            var response = await carwings.HvacOn(vin);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        
        public async Task ChargeOff()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            var vin = vehicles[0].VIN;
            var response = await carwings.ChargeOff(vin);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task ChageOn()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            var vin = vehicles[0].VIN;
            var response = await carwings.ChargeOn(vin);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task FindVehicle()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"));
            var vin = vehicles[0].VIN;
            var location = await carwings.FindVehicle(vin);
            Assert.IsNotNull(location);
            Assert.IsTrue(location.Latitude > 0);
            Assert.IsTrue(location.Longitude > 0);
        }
    }
}
