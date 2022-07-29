using Chronometer.Hubs;
using Chronometer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronometer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChronometerController : ControllerBase
    {
        private static readonly Dictionary<int, ChronometerService> _chronometerServices = new();
        private readonly IHubContext<ChatHub> _chronometerHubContext;

        public ChronometerController(IHubContext<ChatHub> chronometerHubContext)
        {
            _chronometerHubContext = chronometerHubContext;
        }

        // GET: api/<ChronometerController>
        [HttpGet]
        public IEnumerable<ChronometerModel> Get()
        {
            return _chronometerServices.Values.Select(cs => cs.GetModel());
        }

        // GET api/<ChronometerController>/5
        [HttpGet("{id}")]
        public ChronometerModel Get(int id)
        {
            if (!_chronometerServices.TryGetValue(id, out var result))
            {
                throw new Exception($"Timer with id {id} does not exists");
            }
            return result.GetModel();
        }

        // POST api/<ChronometerController>
        [HttpPost]
        public async Task<ChronometerModel> Post()
        {
            var model = ChronometerModelFactory.Create();
            if (!_chronometerServices.TryAdd(model.ID, new ChronometerService(model)))
            {
                throw new Exception($"Timer with {model.ID} already exists");
            }
            await _chronometerHubContext.Clients.All.SendAsync("Add", model.ID);
            return model;
        }

        // PUT api/<ChronometerController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ChronometerModel value)
        {
            if (!_chronometerServices.ContainsKey(id))
            {
                throw new Exception($"Chronometer with id {id} does not exist");
            }
            _chronometerServices[id].Update(
                new ChronometerModel(
                    id, 
                    new TimeSpanModel(value.Timer.Minutes, value.Timer.Seconds, value.Timer.Milliseconds), 
                    value.IsRunning
                )
            );
            await _chronometerHubContext.Clients.All.SendAsync("Update", id);
        }

        // DELETE api/<ChronometerController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (!_chronometerServices.ContainsKey(id))
            {
                throw new Exception($"Chronometer with id {id} does not exist");
            }
            _chronometerServices.Remove(id);
            await _chronometerHubContext.Clients.All.SendAsync("Delete", id);
        }
    }
}
