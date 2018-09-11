using System.Net;
using System.Threading;
using carwings.net;
using carwings.net.login.bouncycastle;
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

            Assert.IsNotNull(userLoginResponse);
            Assert.AreEqual(200, userLoginResponse.Status);

            Assert.IsNotNull(userLoginResponse.Vehicles);
            Assert.AreEqual(1, userLoginResponse.Vehicles.Count);
            Assert.IsNotNull(userLoginResponse.Vehicles[0]);
            Assert.IsNotNull(userLoginResponse.Vehicles[0].CustomSessionId);

            Assert.IsNotNull(userLoginResponse.Profile);
            Assert.IsNotNull(userLoginResponse.Profile.Vin);
        }

        [TestMethod]
        public void GetBatteryStatus()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var batteryStatus = carwings.GetBatteryStatus(userLoginResponse.Vehicles[0], vehicle).Result;

            Assert.IsNotNull(batteryStatus);
        }

        [TestMethod]
        public void RefreshBatteryStatus()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var checkRequest = carwings.RefreshBatteryStatus(userLoginResponse.Vehicles[0], vehicle).Result;

            BatteryStatusCheckResultResponse batteryStatusCheckResult;
            do
            {
                Thread.Sleep(5000);
                batteryStatusCheckResult = carwings.CheckBatteryStatus(userLoginResponse.Vehicles[0], vehicle, checkRequest).Result;
                Assert.IsNotNull(batteryStatusCheckResult);
            }
            while (batteryStatusCheckResult.ResponseFlag != 1);
        }

        [TestMethod]
        public void GetHvacStatus()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var hvacStatus = carwings.GetHvacStatus(userLoginResponse.Vehicles[0], vehicle).Result;

            Assert.IsNotNull(hvacStatus);
        }

        [TestMethod]
        public void TurnHvacOn()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var checkRequest = carwings.HvacOn(userLoginResponse.Vehicles[0], vehicle).Result;

            HvacStatusCheckResultResponse hvacStatusCheckResultResponse;
            do
            {
                Thread.Sleep(5000);
                hvacStatusCheckResultResponse = carwings.CheckHvacOnStatus(userLoginResponse.Vehicles[0], vehicle, checkRequest).Result;
                Assert.IsNotNull(hvacStatusCheckResultResponse);
            }
            while (hvacStatusCheckResultResponse.ResponseFlag != 1);
        }

        [TestMethod]
        public void TurnHvacOff()
        {
            var carwings = GetCarwings();
            var userLoginResponse = carwings.Login().Result;
            var vehicle = userLoginResponse.Profile;
            var checkRequest = carwings.HvacOff(userLoginResponse.Vehicles[0], vehicle).Result;

            HvacStatusCheckResultResponse hvacStatusCheckResultResponse;
            do
            {
                Thread.Sleep(5000);
                hvacStatusCheckResultResponse = carwings.CheckHvacOffStatus(userLoginResponse.Vehicles[0], vehicle, checkRequest).Result;
                Assert.IsNotNull(hvacStatusCheckResultResponse);
            }
            while (hvacStatusCheckResultResponse.ResponseFlag != 1);
        }

        private Carwings GetCarwings()
        {
            // Disable HTTPS verification, to allow using Fiddler
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var username = (string)TestContext.Properties["username"];
            var password = (string)TestContext.Properties["password"];
            // Use BouncyCastle implementation of password provider
            var loginProvider = new LoginProvider(username, password);

            return new Carwings(Region.USA, loginProvider);
        }
    }
}
