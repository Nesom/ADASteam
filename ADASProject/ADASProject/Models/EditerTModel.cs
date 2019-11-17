using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class EditerTModel
    {
        public Tuple<string, Type, bool>[] Types { get; set; }
        public object[] StandartValues { get; set; }
        public string[] Values { get; set; }
    }
}
