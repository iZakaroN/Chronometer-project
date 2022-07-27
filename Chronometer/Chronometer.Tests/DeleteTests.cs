using Chronometer.Controllers;
using Chronometer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chronometer.Tests
{
    class DeleteTests
    {
        private ChronometerController _controller;
        [SetUp]
        public void Setup()
        {
            _controller = new();
        }

        [Test]
        public void Deleting_Chronometer_Validate()
        {
            var test = _controller.Post();
            _controller.Post();
            _controller.Post();

            _controller.Delete(test.ID);

            Assert.AreEqual(2, _controller.Get().Count());
        }

        [Test]
        public void Deleting_Chronometer_Throws_Exception()
        {
            _controller.Post();
            _controller.Post();
            _controller.Post();

            Assert.Throws<Exception>(() => _controller.Delete(4));
        }
    }
}
