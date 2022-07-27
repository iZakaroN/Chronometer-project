using Chronometer.Controllers;
using Chronometer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chronometer.Tests
{
    public class GetTests
    {
        private ChronometerController _controller;
        [SetUp]
        public void Setup()
        {
            _controller = new();
        }

        [Test]
        public void Getting_Chronometer_Passes()
        {
            // ARRANGE
            ChronometerModel testModel = _controller.Post();
            _controller.Post();
            _controller.Post();
            ChronometerModel result;

            // ACT
            result = _controller.Get(testModel.ID);

            // ASSERT
            Assert.AreEqual(testModel.ID, result.ID);
        }

        [Test]
        public void Getting_Chronometer_Throws()
        {
            // ARRANGE
            ChronometerModel testModel = _controller.Post();
            _controller.Post();
            _controller.Post();

            // ASSERT + ACT
            Assert.Throws<Exception>(() => _controller.Get(4));
        }

        [Test]
        public void Getting_All_Chronometers_Passes()
        {
            // ARRANGE
            _controller.Post();
            _controller.Post();
            _controller.Post();
            IEnumerable<ChronometerModel> result;

            // ACT
            result = _controller.Get();

            // ASSERT
            Assert.AreEqual(3, Enumerable.Count(result));
        }

        [Test]
        public void Getting_All_Chronometers_GivesEmptyList()
        {
            // ARRANGE - we do not add any chronometers
            IEnumerable<ChronometerModel> result;

            // ACT
            result = _controller.Get();

            // ASSERT
            Assert.IsEmpty(result);
        }
    }
}