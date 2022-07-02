using System;
using Moq;
using NUnit.Framework;
using TrackYourPresenceAPI.Controllers;
using TrackYourPresenceAPI.Models;
using TrackYourPresenceAPI.Data;
using TrackYourPresenceAPI.DataObjects;

namespace UnitTesting.Api.Services
{
    public class WorkDayServiceTests
    {
        private WorkDayController _controller;
        private Mock<DataContext> _context;

        [SetUp]
        public void OnSetup()
        {
            _context = new Mock<DataContext>();

            _controller = new WorkDayController(_context.Object);
        }

        [Test]
        public void TestFindAsync()
        {
            const string deviceId = "UnitTestDeviceId";
            var uuid = Guid.NewGuid();
            var workday = new WorkDay {
                id = 1,
                Uuid = uuid,
                Date = DateTime.Today,
                PauseTime = DateTime.Today,
                StartTime = DateTime.Today,
                StopTime = DateTime.Today
            };
            Data<WorkDay> data = new() {
                DeviceId = deviceId,
                Uuid = uuid,
                Entity = null
            };
            
            var result = _controller.Find(data);

            Assert.NotNull(result);
        }
        
        [Test]
        public void TestAllAsync()
        {
            const string deviceId = "UnitTestDeviceId";
            var uuid = Guid.NewGuid();
            var workday = new WorkDay {
                id = 1,
                Uuid = uuid,
                Date = DateTime.Today,
                PauseTime = DateTime.Today,
                StartTime = DateTime.Today,
                StopTime = DateTime.Today
            };
            Data<WorkDay> data = new() {
                DeviceId = deviceId,
                Uuid = uuid,
                Entity = null
            };
            
            var result = _controller.All(data);

            Assert.NotNull(result);
        }
    }
}