using System;
using System.Text.RegularExpressions;
using CommonMi;
using CommonMi.Structures;
using NUnit.Framework;


namespace UnitTests
{
    public class Tests
    {
        private MiHandler _handler;
        [SetUp]
        public void Setup()
        {
            this._handler = new MiHandler();
        }

        [Test]
        public void GetSerialNumber_ReturnString()
        {
            string result = this._handler.GetSerialNumber();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
        [Test]
        public void GetVideoControllers_ReturnListOfVideoControllers()
        {
            var result = _handler.GetVideoCards();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(VideoController),result[0]);
            Assert.IsNotNull(result[0].DriverVersion);
            Assert.IsNotEmpty(result[0].DriverVersion);
        }

        [Test]
        public void GetOperatingSystemInfo_ReturnOSInfo()
        {
            var result = _handler.GetOperatingSystemInfo();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(OsInfo), result);
            Assert.IsInstanceOf(typeof(Array), result.Languages);
            Assert.GreaterOrEqual(result.Languages.Length, 1);
            Assert.IsNotNull(result.SystemDrive);
            Assert.IsNotNull(result.Architecture);
            Assert.IsNotNull(result.Build);
        }

        [Test]
        public void GetModel_ReturnString()
        {
            var result = _handler.GetModel();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
        [Test]
        public void GetManufacturer_ReturnString()
        {
            var result = _handler.GetManufacturer();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
        [Test]
        public void GetProcessors_ReturnArrayOfStrings()
        {
            var result = _handler.GetProcessors();
            Assert.IsNotEmpty(result);
            Assert.IsNotNull(result[0]);
            Assert.IsInstanceOf(typeof(string), result[0]);
        }
        [Test]
        public void IsComputerOnACPower_ReturnBoolean()
        {
            var result = _handler.IsComputerOnACPower();
            Assert.IsTrue(result);
        }
        [Test]
        public void IsWindowsActivated_ReturnBoolean()
        {
            var result = _handler.IsWindowsActivated();
            Assert.IsInstanceOf(typeof(bool),result);
        }
        [Test]
        public void GetIP_ReturnProperString()
        {
            var result = _handler.GetIP();
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";    
            Regex check = new Regex(pattern); 
            Assert.IsTrue(check.IsMatch(result));
        }
        [Test]
        public void GetDiskDrives_ReturnListOfDisks()
        {
            var result = _handler.GetDiskDrives();
            Assert.Greater(result.Count, 0);
            
            //Assert.IsTrue(check.IsMatch(result));
        }
    }
}