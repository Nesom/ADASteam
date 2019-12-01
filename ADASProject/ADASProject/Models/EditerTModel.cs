using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace ADASProject.Models
{
    public class EditerTModel
    {
        public Tuple<string, PropertyInfo, bool>[] Types { get; set; }
        public object[] StandartValues { get; set; }
        public string[] Values { get; set; }
        public long TimeInTicks { get; set; }
    }
}
