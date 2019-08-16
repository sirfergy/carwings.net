using System;
using System.IO;
using System.Threading;
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
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
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
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
            var vin = vehicles[0].VIN;
            var response = await carwings.RefreshBatteryStatus(vin, CancellationToken.None);
            Assert.IsNotNull(response.BatteryRecord);
        }

        [TestMethod]
        [Ignore]
        public async Task HvacOff()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
            var vin = vehicles[0].VIN;
            var response = await carwings.HvacOff(vin, CancellationToken.None);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task HvacOn()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
            var vin = vehicles[0].VIN;
            var response = await carwings.HvacOn(vin, CancellationToken.None);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task ChargeOff()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
            var vin = vehicles[0].VIN;
            var response = await carwings.ChargeOff(vin, CancellationToken.None);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task ChargeOn()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
            var vin = vehicles[0].VIN;
            var response = await carwings.ChargeOn(vin, CancellationToken.None);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [Ignore]
        public async Task FindVehicle()
        {
            var carwings = new Carwings();
            var vehicles = await carwings.Login(TestContext.GetTestRunsetting<string>("username"), TestContext.GetTestRunsetting<string>("password"), TestContext.GetTestRunsetting<string>("country"), CancellationToken.None);
            var vin = vehicles[0].VIN;
            var location = await carwings.FindVehicle(vin, CancellationToken.None);
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
            // Deserialize one Leaf.  Data from ~September 2018
            var json = File.ReadAllText("TestLeafAuthData1.txt");
            var deserialized = JsonConvert.DeserializeObject<AuthenticateResponse>(json);

            Assert.IsNotNull(deserialized.Vehicles);
            Assert.IsTrue(deserialized.Vehicles.Length == 1);
            Assert.IsTrue(deserialized.Vehicles[0].Nickname == "LEA2018");
            Assert.AreEqual(2018, deserialized.Vehicles[0].ModelYear);
            Assert.AreEqual(100, deserialized.Vehicles[0].BatteryRecord.Status.Capacity);
            Assert.AreEqual(27.5f, deserialized.Vehicles[0].InteriorTemperatureRecord.Inc_Temp);
            Assert.AreEqual("YES", deserialized.Vehicles[0].BatteryRecord.Status.Charging);
            Assert.AreEqual(74, deserialized.Vehicles[0].BatteryRecord.Status.Remaining);
            Assert.AreEqual(74, deserialized.Vehicles[0].BatteryRecord.Status.StateOfCharge.Value);
            Assert.IsFalse(deserialized.Vehicles[0].BatteryRecord.TimeRequired.HasValue);
            Assert.IsFalse(deserialized.Vehicles[0].BatteryRecord.TimeRequired200.HasValue);
            Assert.IsTrue(deserialized.Vehicles[0].BatteryRecord.TimeRequired200_6kw.HasValue);
            Assert.AreEqual(TimeSpan.FromHours(3), deserialized.Vehicles[0].BatteryRecord.TimeRequired200_6kw.Value);

            // Deserialize a second Leaf.  Data from August 2019.
            json = File.ReadAllText("TestLeafAuthData2.txt");
            deserialized = JsonConvert.DeserializeObject<AuthenticateResponse>(json);

            Assert.IsNotNull(deserialized.Vehicles);
            Assert.IsTrue(deserialized.Vehicles.Length == 1);
            var grayLeaf = deserialized.Vehicles[0];
            Assert.AreEqual("Gray LEAF II", grayLeaf.Nickname);
            Assert.AreEqual(2018, grayLeaf.ModelYear);
            Assert.AreEqual(100, grayLeaf.BatteryRecord.Status.Capacity);
            Assert.AreEqual(28.0f, grayLeaf.InteriorTemperatureRecord.Inc_Temp);
            Assert.AreEqual("NO", grayLeaf.BatteryRecord.Status.Charging);
            Assert.AreEqual(74, grayLeaf.BatteryRecord.Status.Remaining);
            Assert.AreEqual(74, grayLeaf.BatteryRecord.Status.StateOfCharge.Value);
            Assert.AreEqual("NOT_CONNECTED", grayLeaf.BatteryRecord.PluginState);
            Assert.AreEqual(TimeSpan.FromHours(13), grayLeaf.BatteryRecord.TimeRequired.Value);
            Assert.AreEqual(TimeSpan.FromHours(4.5), grayLeaf.BatteryRecord.TimeRequired200.Value);
            Assert.AreEqual(TimeSpan.FromHours(3), grayLeaf.BatteryRecord.TimeRequired200_6kw.Value);
        }

        [TestMethod]
        public void DeserializeBatteryStatusResult()
        {
            // Deserialize a real Leaf battery record.  Data from August 2019.
            var json = File.ReadAllText("TestLeafBatteryRecord.txt");
            var deserialized = JsonConvert.DeserializeObject<BatteryStatusResponse>(json);

            Assert.IsNotNull(deserialized.BatteryRecord);
            Assert.AreEqual("NOT_CONNECTED", deserialized.BatteryRecord.PluginState);
            var batteryStatus = deserialized.BatteryRecord.Status;
            Assert.AreEqual(187000.0, deserialized.BatteryRecord.CruisingRangeAcOn);
            Assert.AreEqual(195000.0, deserialized.BatteryRecord.CruisingRangeAcOff);
            Assert.AreEqual(27.5f, deserialized.TemperatureRecord.Inc_Temp);
            Assert.AreEqual("NO", batteryStatus.Charging);
            Assert.AreEqual(TimeSpan.FromHours(13), deserialized.BatteryRecord.TimeRequired.Value);
        }

        [TestMethod]
        public void DeserializeLocationResponse()
        {
            // Deserialize a Leaf VehicleLocatorResponse.  Data from August 2019.
            var json = File.ReadAllText("TestLocationData.txt");
            var deserialized = JsonConvert.DeserializeObject<VehicleLocatorResponse>(json);

            Assert.IsNotNull(deserialized.Location);
            Assert.AreEqual(47.61712222222222, deserialized.Location.Latitude);
            Assert.AreEqual(-122.15977777777778, deserialized.Location.Longitude);
            Assert.AreEqual("AVAILABLE", deserialized.Location.Position);
        }

        #endregion Serialization Tests
    }
}
