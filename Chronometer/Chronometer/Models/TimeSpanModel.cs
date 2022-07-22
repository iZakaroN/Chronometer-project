using System;

namespace Chronometer.Models
{
    public class TimeSpanModel
    {
        public static readonly TimeSpanModel Zero = new TimeSpanModel(0, 0, 0);
        public static TimeSpanModel ToModel(TimeSpan timeSpan) => new TimeSpanModel(timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        public TimeSpanModel(int minutes, int seconds, int milliseconds)
        {
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }
        public TimeSpan ToTimeSpan()
            => new(0 , 0, Minutes, Seconds, Milliseconds);
        public override string ToString()
            => $"{Minutes}:{Seconds}:{Milliseconds}";
    }
}
