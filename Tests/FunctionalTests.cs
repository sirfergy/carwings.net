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
        public void Login()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
        }

        [TestMethod]
        public void GetBatteryStatus()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var batteryStatus = carwings.GetBatteryStatus(vehicle).Result;
        }

        [TestMethod]
        public void RefreshBatteryStatus()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var checkRequest = carwings.RefreshBatteryStatus(vehicle).Result;

            BatteryStatusCheckResultResponse batteryStatusCheckResult;
            do
            {
                batteryStatusCheckResult = carwings.CheckBatteryStatus(vehicle, checkRequest).Result;
            }
            while (batteryStatusCheckResult.ResponseFlag != 1);
        }

        [TestMethod]
        public void GetHvacStatus()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var hvacStatus = carwings.GetHvacStatus(vehicle).Result;
        }

        private Carwings GetCarwings()
        {
            var username = (string)TestContext.Properties["username"];
            var password = (string)TestContext.Properties["password"];

            return new Carwings(Region.USA, username, password);
        }
    }
}
