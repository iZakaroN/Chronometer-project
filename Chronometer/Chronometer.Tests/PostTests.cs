using Chronometer.Controllers;
using Chronometer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chronometer.Tests
{
    class PostTests
    {
        private ChronometerController _controller;
        [SetUp]
        public void Setup()
        {
            _controller = new();
        }

        [Test]
        public void Adding_Chronometer_Passes()
        {
            // ARRANGE
            ChronometerModel result;

            // ACT
            result = _controller.Post();

            // ASSERT
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsRunning);
        }
        
        [Test]
        public void Adding_Multiple_Chronometers_Passes()
        {
            // ARRANGE
            _controller.Post();
            _controller.Post();
            _controller.Post();

            // ASSERT + ACT
            Assert.AreEqual(3, _controller.Get().Count());
        }
    }
}
