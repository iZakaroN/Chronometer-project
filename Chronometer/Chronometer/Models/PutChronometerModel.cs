using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronometer.Models
{
    public record PutChronometerModel(int ID, TimeSpanModel Timer);
}
