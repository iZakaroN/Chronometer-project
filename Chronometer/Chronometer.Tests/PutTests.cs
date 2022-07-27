using Chronometer.Controllers;
using Chronometer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chronometer.Tests
{
    class PutTests
    {
        private ChronometerController _controller;
        [SetUp]
        public void Setup()
        {
            _controller = new();
        }

        [Test]
        public void Changing_Chronometer_Check_IsRunning()
        {
            _controller.Post();
            _controller.Post();
            _controller.Post();

            _controller.Put(1, new ChronometerModel(1, new TimeSpanModel(0, 0, 0), true));

            Assert.AreEqual(true, _controller.Get(1).IsRunning);
        }

        [Test]
        public void Changing_Chronometer_Throws_Exception()
        {
            _controller.Post();
            _controller.Post();
            _controller.Post();

            Assert.Throws<Exception>(() => {
                _controller.Put(4, new ChronometerModel(1, new TimeSpanModel(0, 0, 0), true));
            });
        }
    }
}
