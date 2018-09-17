using System;
using System.IO;
using System.Threading.Tasks;
using carwings.net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            Assert.IsNotNull(vehicles[0].BatteryRecord);
            Assert.IsTrue(vehicles[0].BatteryRecord.CruisingRangeAcOff > 0);
            Assert.IsTrue(vehicles[0].BatteryRecord.CruisingRangeAcOn > 0);
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
        [Ignore]
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
        public async Task ChargeOn()
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


        #region Serialization Tests

        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private class TimeSpanFieldTest
        {
            public TimeSpan Time;
            public TimeSpan? NullableTimeSpan;
            public TimeSpan? NullTimeSpan;
        }

        [TestMethod]
        public void TimeSpanSerializationTest()
        {
            var testObj = new TimeSpanFieldTest();
            testObj.Time = TimeSpan.FromHours(1);
            testObj.NullableTimeSpan = TimeSpan.FromHours(2);
            testObj.NullTimeSpan = null;

            var json = JsonConvert.SerializeObject(testObj, serializerSettings);
            var deserialized = JsonConvert.DeserializeObject<TimeSpanFieldTest>(json);

            Assert.IsTrue(deserialized.Time == TimeSpan.FromHours(1));
            Assert.IsTrue(deserialized.NullableTimeSpan == TimeSpan.FromHours(2));
            Assert.IsNull(deserialized.NullTimeSpan);
        }

        [TestMethod]
        public void DeserializeAuthenticateResult()
        {
            var json = File.ReadAllText("TestLeafData.txt");
            var deserialized = JsonConvert.DeserializeObject<AuthenticateResponse>(json);

            Assert.IsNotNull(deserialized.Vehicles);
            Assert.IsTrue(deserialized.Vehicles.Length == 1);
        }

        #endregion Serialization Tests
    }
}
