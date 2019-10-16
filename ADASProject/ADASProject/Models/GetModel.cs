using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASProject.Models
{
    public class GetModel
    {
        public int Id { get; set; }
        public Dictionary<string, Dictionary<string, string>> Values { get; set; }
        public Dictionary<string, Dictionary<string, string>> StandartValues { get; set; }
        public byte[] Image { get; set; }
    }
}
