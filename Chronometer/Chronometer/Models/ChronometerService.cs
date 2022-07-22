using System;

namespace Chronometer.Models
{
    public class ChronometerService
    {
        private int _id;
        private DateTime _startTime;
        private DateTime _endTime;
        private bool _isRunning;
        public ChronometerService(ChronometerModel model)
        {
            _id = model.ID;
            _startTime = DateTime.Now;
            _isRunning = model.IsRunning;
        }

        /*public ChronometerModel GetModel() 
            => new ChronometerModel(_id, TimeSpanModel.FromTimeSpan(_endTime - _startTime), _isRunning);
*/
        public void Update(ChronometerModel model)
        {
            
        }
    }
}
