using System;
using System.Diagnostics;

namespace Chronometer.Models
{
    public class ChronometerService
    {
        private ChronometerModel Model { get; }
        private DateTime UpdateTime { get; set; }

        private TimeSpan UpdateTimer => (DateTime.Now - UpdateTime);
        private TimeSpan Timer => Model.Timer.ToTimeSpan() + UpdateTimer;
        public ChronometerService(ChronometerModel model)
        {
            Model = model;
            UpdateTime = DateTime.Now;
        }

        public void Update(ChronometerModel model)
        {
            UpdateTime = DateTime.Now;
            Model.Update(model);
        }

        //internal ChronometerModel GetModel()
        //{
        //    if (Model.IsRunning)
        //    {
        //        var currentTimerValues = Model.Timer.ToTimeSpan();
        //        var timeDifference = DateTime.Now.Subtract(StartTime);
        //        Model.Timer = TimeSpanModel.ToModel(currentTimerValues.Add(timeDifference));
        //        if (Model.Timer.Milliseconds.ToString().Length == 3)
        //            Model.Timer.Milliseconds /= 100;
        //        else
        //            Model.Timer.Milliseconds = 0;

        //        StartTime = DateTime.Now;
        //    }
        //    return Model;
        //}

        public ChronometerModel GetModel()
        {
            if (Model.IsRunning)
            {
                var activeTimerModel = TimeSpanModel.ToModel(Timer);
                activeTimerModel.Milliseconds /= 100;
                return new ChronometerModel(Model.ID, activeTimerModel, Model.IsRunning);
            }
            return Model;
        }
    }
}
