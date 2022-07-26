/*using System;

namespace Chronometer.Models
{
    public class ChronometerService
    {
        private ChronometerModel Model { get; }
        private DateTime StartTime { get; set; }

        private TimeSpan RunningTimer => (DateTime.Now - StartTime);
        private TimeSpan Timer => Model.IsRunning ? RunningTimer : Model.Timer.ToTimeSpan();
        public ChronometerService(ChronometerModel model)
        {
            Model = model;
            StartTime = DateTime.Now;
        }

        public void Update(ChronometerModel model)
        {
            StartTime = DateTime.Now - model.Timer.ToTimeSpan();
            Model.Update(model);
        }

        internal ChronometerModel GetModel()
            => new ChronometerModel(Model.ID, TimeSpanModel.ToModel(Timer), Model.IsRunning);
    }
}
*/