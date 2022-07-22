using System;

namespace Chronometer.Models
{
    public static class ChronometerModelFactory
    {
        private static int _identityCounter = 0;
        private static object _lock = new object();

        public static ChronometerModel Create()
        {
            lock (_lock)
            {
                var id = ++_identityCounter;
                return new ChronometerModel(id, TimeSpanModel.Zero, DateTime.MinValue, false);
            }
        }
    }

    public class ChronometerModel
    {
        public ChronometerModel(int id, TimeSpanModel timer, DateTime startTime, bool isRunning)
        {
            ID = id;
            Timer = timer;
            StartTime = startTime;
            IsRunning = isRunning;
        }

        public int ID { get; private set; }
        public TimeSpanModel Timer { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsRunning { get; private set; }

        public void Update(ChronometerModel model)
        {
            ID = model.ID;
            Timer = model.Timer;
            StartTime = model.StartTime;
            IsRunning = model.IsRunning;
        }
    }
}
