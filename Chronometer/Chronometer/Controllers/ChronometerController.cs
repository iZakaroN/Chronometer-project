using Chronometer.Models;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chronometer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChronometerController : ControllerBase
    {
        private static readonly Dictionary<int, ChronometerModel> Timers = new();
        private IEnumerable<ChronometerModel> GetChronometersWithChangedTime()
        {
            TimeSpan timeDifference;
            TimeSpan currentTimerValues;
            foreach (var chronometer in Timers.Values)
            {
                if (chronometer.IsRunning)
                {
                    currentTimerValues = chronometer.Timer.ToTimeSpan();
                    timeDifference = DateTime.Now.Subtract(chronometer.StartTime);
                    chronometer.Timer = TimeSpanModel.ToModel(currentTimerValues.Add(timeDifference));
                    chronometer.StartTime = DateTime.Now;
                }
            }
            return Timers.Values;
        }
        // GET: api/<ChronometerController>
        [HttpGet]
        public IEnumerable<ChronometerModel> Get()
        {
            return GetChronometersWithChangedTime();
        }

        // GET api/<ChronometerController>/5
        [HttpGet("{id}")]
        public ChronometerModel Get(int id)
        {
            if (!Timers.TryGetValue(id, out var result))
            {
                throw new Exception($"Timer with id {id} does not exists");
            }
            return result;
        }

        // POST api/<ChronometerController>
        [HttpPost]
        public ChronometerModel Post()
        {
            var result = ChronometerModelFactory.Create();
            if (!Timers.TryAdd(result.ID, result))
            {
                throw new Exception($"Timer with {result.ID} already exists");
            }
            return result;
        }

        // PUT api/<ChronometerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ChronometerModel value)
        {
            if (!Timers.ContainsKey(id))
            {
                throw new Exception($"Chronometer with id {id} does not exist");
            }
            Timers[id].Update(new ChronometerModel(id, new TimeSpanModel(value.Timer.Minutes, value.Timer.Seconds, value.Timer.Milliseconds), DateTime.Now, value.IsRunning));
        }

        // DELETE api/<ChronometerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (!Timers.ContainsKey(id))
            {
                throw new Exception($"Chronometer with id {id} does not exist");
            }
            Timers.Remove(id);
        }
    }
}
